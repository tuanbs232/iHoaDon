using iHoaDon.Business;
using iHoaDon.Entities;
using iHoaDon.Resources.Admin;
using iHoaDon.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iHoaDon.Web.Areas.Admin.Controllers
{
   // [AccessDeniedAuthorize(Roles = Roles.Admin)]
    public class UserController : Controller
    {
        //
        // GET: /Admin/User/
        private readonly AccountService _accountSvc;
        public UserController(AccountService accountSvc)
        {
            _accountSvc = accountSvc;

        }
        public ActionResult Me()
        {
            var userName = User.GetUserName().ToString();
            var account = _accountSvc.GetByLoginName(userName);
            var accountModel = new AccountModel
            {
                Phone = account.Phone,
                CompanyName = account.CompanyName,
                CompanyCode = account.CompanyCode,
                RoleName = account.RoleName,
                Role = account.RoleCode
            };
            return View(accountModel);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AccountModel newAccount)
        {
            try
            {
                ActionResult result;
                if (ModelState.IsValid)
                {
                    try
                    {
                        var acc = _accountSvc.GetByLoginName(newAccount.CompanyCode);
                        if (acc != null)
                        {
                            ModelState.AddModelError(String.Empty,
                                             UserControllerResource.CreateAccountError.FormatWith("Tên đăng nhập đã tồn tại!"));
                            result = View();
                        }
                        else
                        {
                            var password = newAccount.Password;
                            var salt = EntityUtils.GenerateRandomBytes(Constants.PasswordSaltLength);
                            var pwdHash = EntityUtils.GetInputPasswordHash(password, salt);
                            var account = new Account
                            {
                                Phone = newAccount.Phone,
                                CompanyName = newAccount.CompanyName,
                                CompanyCode = newAccount.CompanyCode,
                                Status = 0,
                                RoleCode = (byte)newAccount.Role,
                                PasswordHash = pwdHash,
                                PasswordSalt = salt,
                                Permissions = 0
                            };
                            _accountSvc.Add(account);
                            result = RedirectToAction("Me");
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(String.Empty,
                                                 UserControllerResource.CreateAccountError.FormatWith(ex.Message));
                        result = View();
                    }
                }
                else
                {
                    ModelState.AddModelError(String.Empty, UserControllerResource.InvalidAccountInfo);
                    result = View();
                }
                return result;
            }
            catch (Exception ex)
            {
                TempData.Add("error", ex.ToString());
                return RedirectToAction("Index", "Error");
            }
        }
    }
}
