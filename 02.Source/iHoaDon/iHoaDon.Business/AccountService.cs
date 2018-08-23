using System.Transactions;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;
using iHoaDon.Resources.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace iHoaDon.Business
{
    public class AccountService : Service
    {
        // logger for this class
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IRepository<Account> _account;
        private readonly IRepository<AccountLogin> _accountLog;
        private readonly LogService _logService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AccountService(IUnitOfWork context)
            : base(context)
        {
            _account = Context.GetRepository<Account>();
            _accountLog = Context.GetRepository<AccountLogin>();
            _logService = new LogService(context);
        }

        public Account GetById(int id)
        {
            return _account.One(AccountQuery.WithById(id));
        }

        public Account GetByLoginName(string loginName)
        {
            return _account.One(AccountQuery.WithByLoginName(loginName));
        }

        public IEnumerable<Account> GelAll()
        {
            return _account.Find();
        }
        public void ChangePassword(string loginName, string oldPassword, string newPassword)
        {
            var spec = AccountQuery.WithByLoginName(loginName);
            var admin = _account.One(spec);
            if (admin == null)
            {
                throw new Exception(AccountServiceResource.AccountNullException.FormatWith("LoginName", loginName));
            }

            var dbPwdHash = admin.PasswordHash;
            var dbsalt = admin.PasswordSalt;

            if (dbPwdHash == null || dbsalt == null)
            {
                throw new Exception(AccountServiceResource.PasswordAndSaltNullException);
            }

            var inputPwdHash = EntityUtils.GetInputPasswordHash(oldPassword, dbsalt);

            if (!dbPwdHash.SequenceEqual(inputPwdHash))
            {
                throw new Exception(AccountServiceResource.PasswordInvalidException);
            }

            var salt = EntityUtils.GenerateRandomBytes(Constants.PasswordSaltLength);
            var pwdHash = EntityUtils.GetInputPasswordHash(newPassword, salt);

            admin.PasswordSalt = salt;
            admin.PasswordHash = pwdHash;

            Context.SaveChanges();
        }
        public IEnumerable<Account> GetAllAccount(out int totalRecords,
                                                                    int currentPage = 1,
                                                                    int pageSize = 25,
                                                                    string sortBy = "Id",
                                                                    bool descending = true,
                                                                    string loginName = null)
        {
            var spec = AccountQuery.WithAll();
            spec = loginName != null
                    ? spec.And(AccountQuery.WithByLoginName(loginName))
                    : spec;
            totalRecords = _account.Count(spec);
            var sort = Context.Filters.Sort<Account, int>(ti => ti.Id, true);
            switch (sortBy)
            {
                case "Id":
                    sort = Context.Filters.Sort<Account, int>(ti => ti.Id, descending);
                    break;
                case "Title":
                    sort = Context.Filters.Sort<Account, string>(ti => ti.CompanyCode, descending);
                    break;
                default:
                    break;
            }
            var pager = Context.Filters.Page<Account>(currentPage, pageSize);
            return _account.Find(spec, sort, pager);
        }

        public bool Add(Account account)
        {
            try
            {
                if (account.Id == 0)
                    _account.Create(account);
                else
                    _account.Update(account);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Cannot update account information. " + ex.Message, ex);
                return false;
            }
        }

        public iHoaDonIdentity DoAuthenticate(string loginName, string pwd, string ip)
        {
            var expireDate = DateTime.MaxValue;

            var spec = AccountQuery.WithLoginName(loginName);
            int t = _account.Count(spec);
            var m = _account.Find(spec);
            var ts = _account.One(AccountQuery.WithLoginName(loginName));
            var account = _account.One(spec);
            if (account == null)
            {
                CreateAccountLog(loginName, ip, DateTime.Now, false);
                throw new Exception(AccountServiceResource.AccountNullExceptionLogin.FormatWith(loginName));
            }

            //var inputPwdHash = EntityUtils.GetInputPasswordHash(pwd, account.PasswordSalt);
            ////kiểm tra password có đúng không
            //if (!account.PasswordHash.SequenceEqual(inputPwdHash))
            //{
            //    CreateAccountLog(loginName, ip, DateTime.Now, false);
            //    throw new Exception(AccountServiceResource.PasswordInvalidException);
            //}

            // Kiểm tra ngày hết hạn của khách hàng

            expireDate = DateTime.Now.AddDays(1);

            CreateAccountLog(loginName, ip, DateTime.Now, true);
            return new iHoaDonIdentity(
                 account.CompanyCode,
                 account.Id,
                 account.RoleCode,
                 account.PermissionFlags,
                 !account.MasterAccountId.HasValue,
                 expireDate
                 );
        }
        private void CreateAccountLog(string loginName, string loginIP, DateTime loginTime, bool status)
        {
            var accountLog = new AccountLogin
            {
                LoginName = loginName,
                LoginIP = loginIP,
                LoginTime = loginTime,
                Status = status
            };
            _accountLog.Create(accountLog);
            Context.SaveChanges();
        }

        public void Create(Account account)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (account.Id == 0)
                        _account.Create(account);
                    else
                        _account.Update(account);
                    Context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }
    }
}
