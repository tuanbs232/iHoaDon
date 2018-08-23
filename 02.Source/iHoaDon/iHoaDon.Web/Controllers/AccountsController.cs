using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using iHoaDon.Business;
using iHoaDon.Entities;
using iHoaDon.Util;
using WebDemoSigning.Filter;

namespace iHoaDon.Web.Controllers
{

    public class AccountsController : Controller
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Account/
        private static AccountService _acc;
        private static ProfileService _pro;
        public AccountsController(AccountService acc, ProfileService pro)
        {
            _acc = acc;
            _pro = pro;

        }
        public ActionResult Index(string message, string messageType)
        {
            int id = User.GetAccountId();
            Account account = _acc.GetById(id);
            Profile profile = _pro.GetById(account.ProfileId);
            ViewBag.Account = account;
            ViewBag.Profile = profile;

            ViewBag.Message = message;
            ViewBag.MessageType = messageType;

            return View();
        }

        public ActionResult LogOnUser()
        {
            return View();
        }
        [HttpPost]

        public ActionResult LogOnUser(LogOnUseModel model, string returnUrl)
        {
            ActionResult result;
            if (ModelState.IsValid)
            {
                try
                {
                    var identity = _acc.DoAuthenticate(model.CompanyCode, model.Password, Request.ServerVariables["REMOTE_ADDR"]);
                    var userDataString = identity.ToCookieString();
                    FormsAuthentication.SetAuthCookie(identity.Name, model.RememberMe);
                    var authCookie = FormsAuthentication.GetAuthCookie(identity.Name, model.RememberMe);
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    var newTicket = new FormsAuthenticationTicket(
                        ticket.Version,
                        ticket.Name,
                        ticket.IssueDate,
                        ticket.Expiration,
                        ticket.IsPersistent,
                        userDataString
                    );
                    authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                    Response.Cookies.Add(authCookie);
                    if (Request.Cookies["checkLogin"] != null)
                    {
                        Response.Cookies["checkLogin"].Value = identity.Name;
                    }
                    else
                    {
                        Response.Cookies.Add(new HttpCookie("checkLogin", identity.Name));
                    }
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        result = RedirectToAction("Dashboard", "Home", new { area = "" });

                    }
                    else
                    {
                        if (Url.IsLocalUrl(returnUrl)
                            && returnUrl.Length > 1
                            && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//")
                            && !returnUrl.StartsWith("/\\"))
                        {
                            result = Redirect(returnUrl);
                        }
                        else
                        {
                            result = RedirectToAction("Index", "Home", new { area = "" });
                        }
                    }
                }
                catch (Exception exception)
                {

                    ModelState.AddModelError(String.Empty, exception.Message);
                    result = View(model);
                }
            }
            else
            {
                result = View(model);
            }
            return result;
        }

        public ActionResult LogOffUser()
        {
            Session.Clear();
            if (Request.Cookies["checkLogin"] != null)
            {
                Response.Cookies["checkLogin"].Value = string.Empty;
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult Register()
        {
            ViewBag.District = "";

            var registerMode = new RegisterModel()
            {
                ListProvince = ListTaxAgency()
            };
            return View(registerMode);

        }

        [HttpPost]
        public ActionResult Register(RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                register.Subject = "";
                register.Serial = "";
                register.ValidFrom = "10/01/2018";
                register.ValidTo = "10/01/2020";
                var profile = new Profile
                {
                    CompanyName = register.CompanyName,
                    Email = register.Email,
                    Phone = register.Phone,
                    Fax = null,
                    CompanyCode = register.CompanyCode,
                    Address = register.Address,
                    Province = register.Province,
                    StartOfFinancialYear = null,
                    TaxAgencyCode = register.TaxAgencyCode,
                    TaxAgencyName = register.TaxAgencyName,
                    Issuer = register.Issuer,
                    Subject = register.Subject,
                    Serial = register.Serial,
                    ValidFrom = DateTime.Parse(register.ValidFrom,
                                                                       System.Globalization.CultureInfo.
                                                                               GetCultureInfo("vi-VN").
                                                                               DateTimeFormat),
                    ValidTo = DateTime.Parse(register.ValidTo,
                                                                       System.Globalization.CultureInfo.
                                                                               GetCultureInfo("vi-VN").
                                                                               DateTimeFormat),
                };

                var password = register.NewPassword;
                var salt = EntityUtils.GenerateRandomBytes(Constants.PasswordSaltLength);
                var pwdHash = EntityUtils.GetInputPasswordHash(password, salt);
                var accounts = new Account()
                {
                    ProfileId = profile.Id,
                    CompanyCode = register.CompanyCode,
                    PasswordHash = pwdHash,
                    PasswordSalt = salt,
                    CompanyName = register.CompanyName,
                    Representative = register.Representative,
                    Address = register.Address,
                    Phone = register.Phone,
                    AccountType = null,
                    BankAccount = register.BankAccount,
                    BankName = null,
                    ActivationTime = DateTime.Now,
                    DateExpired = DateTime.Now,
                    DeactivationTime = DateTime.Now,
                    // LastLoginDate = DateTime.Now
                };

                using (var scope = new TransactionScope())
                {
                    _pro.Create(profile);

                    accounts.ProfileId = profile.Id;
                    _acc.Create(accounts);
                    scope.Complete();
                }
            }
            else
            {
                return View();
            }

            return null;
        }

        [HttpGet]
        [PublicWebAuthentication]
        public ActionResult EditAccounts(int id)
        {
            var acc = _acc.GetById(id);
            var pro = _pro.GetById(acc.ProfileId);
            var registerMode = new EditAccountsModel()
            {
                ListProvince = ListTaxAgency(),
                CompanyCode = acc.CompanyCode,
                CompanyName = acc.CompanyName,

                Representative = acc.Representative,
                Address = acc.Address,
                Phone = acc.Phone,
                BankAccount = acc.BankAccount,
                Province = pro.Province,
                TaxAgencyCode = pro.TaxAgencyCode,
                Email = pro.Email
            };
            var list = ListTaxAgency();
            foreach (var p in list)
            {
                if (p != null && pro.Province.Equals(p.Text))
                {
                    ViewBag.Province = p.Value;
                    break;
                }
            }
            ViewBag.District = pro.TaxAgencyCode;
            return View(registerMode);

        }

        [HttpPost]
        [PublicWebAuthentication]
        public ActionResult EditAccounts(EditAccountsModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _acc.GetById(model.Id);
                if (account == null)
                {
                    return RedirectToAction("Index", new { message = "Thông tin đăng nhập không chính xác", messageType = "error" });
                }

                var pro = _pro.GetById(account.ProfileId);
                if (pro == null)
                {
                    return RedirectToAction("Index", new { message = "Không tìm thấy thông tin tài khoản", messageType = "error" });
                }
                pro.Address = model.Address;
                pro.CompanyCode = model.CompanyCode;
                pro.CompanyName = model.CompanyName;
                pro.Email = model.Email;
                pro.Phone = model.Phone;
                pro.Province = model.Province;
                pro.TaxAgencyCode = model.TaxAgencyCode;
                pro.TaxAgencyName = model.TaxAgencyName;
                if (!_pro.Update(pro))
                {
                    return RedirectToAction("Index",
                        new
                        {
                            message = "Có lỗi khi cập nhật thông tin người dùng. Vui lòng thử lại sau",
                            messageType = "error"
                        });
                }

                account.CompanyCode = model.CompanyCode;
                account.CompanyName = model.CompanyName;
                account.Address = model.Address;
                account.BankAccount = model.BankAccount;
                account.Phone = model.Phone;
                account.Representative = model.Representative;

                if (!_acc.Add(account))
                {
                    return RedirectToAction("Index",
                        new
                        {
                            message = "Có lỗi khi cập nhật thông tin tài khoản. Vui lòng thử lại sau",
                            messageType = "error"
                        });
                }

                return RedirectToAction("Index", new { message = "Cập nhật thông tin tài khoản thành công", messageType = "info" });
            }
            else
            {
                ViewBag.Province = model.ProvinceId;
                ViewBag.District = model.TaxAgencyCode;
                model.ListProvince = ListTaxAgency();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ChangeCertificate(string CertBase64)
        {
            int id = User.GetAccountId();
            Account account = _acc.GetById(id);
            if(account == null)
            {
                return RedirectToAction("Index", new { message = "Không tìm thấy thông tin tài khoản", messageType = "error" });
            }
            Profile profile = _pro.GetById(account.ProfileId);
            if(profile == null)
            {
                return RedirectToAction("Index", new { message = "Không tìm thấy thông tin người dùng", messageType = "error" });
            }

            byte[] certData = null;
            try
            {
                certData = Convert.FromBase64String(CertBase64);
            }
            catch(Exception ex)
            {
                _log.Error("Cannot parse account certificate. " + ex.Message, ex);
                return RedirectToAction("Index", new { message = "Dữ liệu certificate không hợp lệ", messageType = "error" });
            }

            X509Certificate2 cert = null;
            try
            {
                cert = new X509Certificate2(certData);
            }
            catch(Exception ex)
            {
                _log.Error("Cannot parse account certificate. " + ex.Message, ex);
                return RedirectToAction("Index", new { message = "Dữ liệu certificate không hợp lệ. ", messageType = "error" });
            }

            profile.Subject = cert.GetNameInfo(X509NameType.DnsName, false);
            profile.Issuer = cert.GetNameInfo(X509NameType.DnsName, true);
            profile.Serial = cert.SerialNumber;
            profile.ValidFrom = cert.NotBefore;
            profile.ValidTo = cert.NotAfter;

            if (!_pro.Update(profile))
            {
                return RedirectToAction("Index", new { message = "Lỗi khi cập nhật chữ ký số. ", messageType = "error" });
            }

            return RedirectToAction("Index", new { message = "Cập nhật chữ ký số thành công", messageType = "info" });
        }

        private IEnumerable<SelectListItem> ListTaxAgency()
        {
            string paths = Server.MapPath("~") + "Content\\Json\\taxBureauCode.json";
            var listTaxBureau = ProvinceTaxAgency.TaxBureaus(paths);
            var lstItem = new List<SelectListItem> { new SelectListItem { Value = "", Text = @"- Chọn Tỉnh/Thành Phố -" } };
            foreach (var taxBureau in listTaxBureau)
            {
                lstItem.Add(new SelectListItem { Text = taxBureau.Text, Value = taxBureau.Value });

            }
            return lstItem;
        }


        [HttpGet]
        public ActionResult ListProvince(string newsId)
        {

            string paths = Server.MapPath("~") + "Content\\Json\\taxBureauCode.json";
            var listChist = new List<ProvinceNew>();
            var listTaxBureau = ProvinceTaxAgency.TaxBureaus(paths);
            foreach (var taxBureau in listTaxBureau)
            {
                if (taxBureau.Value.Equals(newsId))
                {
                    listChist = taxBureau.Childs;
                    break;
                }
            }

            var lstItem = new List<SelectListItem> { new SelectListItem { Value = "", Text = @"- Chọn cơ quan BHXH quản lý -" } };
            foreach (var item in listChist)
            {
                lstItem.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }
    }
}
