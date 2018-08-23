using iHoaDon.Business;
using iHoaDon.Resources.Admin;
using iHoaDon.Web.Areas.Admin.Models;
using iHoaDon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace iHoaDon.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Admin/Account/
        private readonly AccountService _accountSvc;
        public AccountController(AccountService accountSvc)
        {
            _accountSvc = accountSvc;
        }
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            ActionResult result;
            if (ModelState.IsValid)
            {
                try
                {
                    var identity = _accountSvc.DoAuthenticate(model.LoginName, model.PassWord, Request.ServerVariables["REMOTE_ADDR"]);
                    var userDataString = identity.ToCookieString();
                    FormsAuthentication.SetAuthCookie(identity.Name, model.Remember);
                    var authCookie = FormsAuthentication.GetAuthCookie(identity.Name, model.Remember);
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
                        result = RedirectToAction("Me", "User", new { area = "Admin" });

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
                            result = RedirectToAction("Me", "User", new { area = "Admin" });
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



        public ActionResult LogOff()
        {
            Session.Clear();
            if (Request.Cookies["checkLogin"] != null)
            {
                Response.Cookies["checkLogin"].Value = string.Empty;
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn", "Account", new { area = "Admin" });
        }
        private const string DefaultItemSortBy = "Id";
        [OutputCache(CacheProfile = "CacheClient")]
        public ActionResult Index()
        {
            try
            {
                var model = new SortAndPageModel
                {
                    CurrentPageIndex = 1,
                    SortBy = DefaultItemSortBy,
                    SortDescending = false
                };

                int totalRecords;
                ViewBag.Accounts = _accountSvc.GetAllAccount(out totalRecords);
                model.TotalRecordCount = totalRecords;
                ViewBag.SortAndPage = model;
                ViewBag.ModelSearch = new AccountSearchModel
                {
                    LoginName = null
                };
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Menus", new { area = "Admin" });
            }
        }
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Client, VaryByParam = "search;pageSize")]
        public ActionResult SearchAccounts(AccountSearchModel search, int pageSize)
        {

            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    var model = new SortAndPageModel
                    {
                        CurrentPageIndex = 1,
                        SortBy = "",
                        SortDescending = false,
                        PageSize = pageSize
                    };

                    int totalRecords;
                    ViewBag.Accounts = _accountSvc.GetAllAccount(out totalRecords,
                                                        model.CurrentPageIndex,
                                                        pageSize,
                                                        descending: true,
                                                        loginName: search.LoginName);

                    model.TotalRecordCount = totalRecords;
                    ViewBag.SortAndPage = model;
                }
                ViewBag.ModelSearch = search;
            }
            return PartialView("Partial", ViewBag.Accounts);
        }
        [OutputCache(Duration = 60,
                    Location = OutputCacheLocation.Client,
                    VaryByParam = "name;sortBy;sortDesc;page;pageSize")]
        public ActionResult SortingAndPagingAccounts(string loginName,
                                            string sortBy = null,
                                            bool sortDesc = true,
                                            int page = 1,
                                            int pageSize = 25)
        {
            if (Request.IsAjaxRequest())
            {

                var model = new SortAndPageModel
                {
                    CurrentPageIndex = page,
                    SortBy = sortBy,
                    SortDescending = sortDesc,
                    PageSize = pageSize
                };

                int totalRecords;
                ViewBag.Accounts = _accountSvc.GetAllAccount(out totalRecords,
                                                        model.CurrentPageIndex,
                                                        pageSize,
                                                        sortBy,
                                                        sortDesc,
                                                        loginName);
                model.TotalRecordCount = totalRecords;
                ViewBag.SortAndPage = model;
            }
            ViewBag.ModelSearch = new AccountSearchModel
            {
                LoginName = loginName
            };

            return PartialView("Partial", ViewBag.Accounts);
        }

        [AccessDeniedAuthorize(Roles = iHoaDon.Entities.Roles.Admin)]
        public ActionResult ChangePassword()
        {
            var password = new ChangePasswordModel();
            if (Response.Cookies["Password"] != null)
            {
                password.OldPassword = Request.Cookies["Password"].Value;
            }
            return View(password);
        }

        [HttpPost]
        [AccessDeniedAuthorize(Roles = iHoaDon.Entities.Roles.Admin)]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            ActionResult result = null;
            if (ModelState.IsValid)
            {
                try
                {
                    _accountSvc.ChangePassword(User.GetUserName(), model.OldPassword, model.NewPassword);
                    result = RedirectToAction("Me", "User", new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    result = View(model);
                }
            }

            return result;
        }
    }
}
