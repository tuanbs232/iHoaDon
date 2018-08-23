using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;
using iHoaDon.Resources.Client;
using iHoaDon.Util;

namespace iHoaDon.Business
{
    public class ProfileService : Service
    {
        // logger for this class
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IRepository<Profile> _profile;
        private readonly LogService _actionLogSvc;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ProfileService(IUnitOfWork context)
            : base(context)
        {

            _profile = Context.GetRepository<Profile>();
            _actionLogSvc = new LogService(context);
        }

        public Profile GetById(int id)
        {
            return _profile.One(ProfileQuery.WithById(id));
        }
        public IEnumerable<Profile> GelAll()
        {
            return _profile.Find();
        }

        public void Create(Profile profile)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var actionContent = profile.Id == 0
                                           ? String.Format(RegisterModelResource.CreateProfile)
                                           : String.Format(RegisterModelResource.EditProfile, profile.Id);
                    var actionType = profile.Id == 0
                                         ? (byte)LogActionType.CreateAccountProfileLog
                                         : (byte)LogActionType.UpdateAccountProfileLog;
                    var dataAfterChange = profile.StringifyJs();

                    if (profile.Id == 0)
                        _profile.Create(profile);
                    else
                        _profile.Update(profile);
                    Context.SaveChanges();

                    var loginNameChanges = User.GetUserName();
                    if (!string.IsNullOrEmpty(loginNameChanges))
                    {
                        _actionLogSvc.CreateActionLog(loginNameChanges, actionContent, actionType,
                                                      dataAfterChange: dataAfterChange);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public bool Update(Profile profile)
        {
            try
            {
                _profile.Update(profile);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Cannot update account profile. " + ex.Message, ex);
                return false;
            }
        }

        //public IEnumerable<Profile> GetAllProfile(out int totalRecords,
        //                                                            int currentPage = 1,
        //                                                            int pageSize = 25,
        //                                                            string sortBy = "Id",
        //                                                            bool descending = true,
        //                                                            string name = null)
        //{
        //    var spec = ProfileQuery.WithAll();
        //    spec = name != null
        //            ? spec.And(ProfileQuery.WithByName(name))
        //            : spec;
        //    totalRecords = _profile.Count(spec);
        //    var sort = Context.Filters.Sort<Profile, int>(ti => ti.Id, true);
        //    switch (sortBy)
        //    {
        //        case "Id":
        //            sort = Context.Filters.Sort<Profile, int>(ti => ti.Id, descending);
        //            break;
        //        case "Name":
        //            sort = Context.Filters.Sort<Profile, string>(ti => ti.Name, descending);
        //            break;
        //        case "Order":
        //            sort = Context.Filters.Sort<Profile, int>(ti => ti.Order, descending);
        //            break;
        //        default:
        //            break;
        //    }
        //    var pager = Context.Filters.Page<Profile>(currentPage, pageSize);
        //    return _profile.Find(spec, sort, pager);
        //}
        public void Delete(Profile Profile)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var dataBeforeChange = Profile.StringifyJs();
                    _profile.Delete(Profile);
                    Context.SaveChanges();
                    // Ghi log tao khach hang
                    var loginNameChanges = User.GetUserName();
                    var actionContent = String.Format(RegisterModelResource.DeleteProfile, Profile.Id);
                    const byte actionType = (byte)LogActionType.DeleteAccountProfileLog;
                    _actionLogSvc.CreateActionLog(!string.IsNullOrEmpty(loginNameChanges) ? loginNameChanges : "admin",
                                                  actionContent, actionType, dataBeforeChange);

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
