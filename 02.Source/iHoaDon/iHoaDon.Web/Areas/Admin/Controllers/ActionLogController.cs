using iHoaDon.Business;
using iHoaDon.Entities;
using iHoaDon.Web.Areas.Admin.Models;
using iHoaDon.Web.Models;
using System;
using System.Web.Mvc;
using System.Web.UI;

namespace iHoaDon.Web.Areas.Admin.Controllers
{
    [AccessDeniedAuthorize(Roles = Roles.Admin)]
    public class ActionLogController : Controller
    {
        private readonly LogService _svcLog;
        private const string DefaultItemSortBy = "LoginTime";
        private const string DefaultActionLogSortBy = "ActionTime";
        public ActionLogController(LogService svcLog)
        {
            _svcLog = svcLog;
        }
        //
        // GET: /Log/
        public ActionResult Index()
        {
            var model = new SortAndPageModel { CurrentPageIndex = 1, SortBy = DefaultItemSortBy, SortDescending = true };
            int totalRecords;
            ViewBag.LogItems = _svcLog.GetAllLogItems(out totalRecords);

            model.TotalRecordCount = totalRecords;
            ViewBag.ModelSearch = new LogItemSearchModel
            {
                Id = -1,
                LoginName = "",
                FromDate = null,
                ToDate = null,
                LoginIP = "",
                LoginTime = null,
                Status = null
            };

            ViewBag.SortAndPage = model;
            return View();
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Client, VaryByParam = "search;pageSize")]
        public ActionResult SearchLogItem(LogItemSearchModel search, int pageSize)
        {

            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    var model = new SortAndPageModel
                    {
                        CurrentPageIndex = 1,
                        SortBy = DefaultItemSortBy,
                        SortDescending = true,
                        PageSize = pageSize
                    };
                    int totalRecords;
                    ViewBag.LogItems = _svcLog.GetAllLogItems(out totalRecords,
                                                        pageSize: pageSize,
                                                        currentPage: model.CurrentPageIndex,
                                                        loginName: search.LoginName,
                                                        fromDate: string.IsNullOrEmpty(search.FromDate)
                                                            ? (DateTime?)null
                                                            : DateTime.Parse(search.FromDate,
                                                                           System.Globalization.CultureInfo.
                                                                                   GetCultureInfo("vi-VN").
                                                                                   DateTimeFormat),
                                                        toDate: string.IsNullOrEmpty(search.ToDate)
                                                            ? (DateTime?)null
                                                            : DateTime.Parse(search.ToDate,
                                                                           System.Globalization.CultureInfo.
                                                                                   GetCultureInfo("vi-VN").
                                                                                   DateTimeFormat),
                                                         status: search.Status);
                    model.TotalRecordCount = totalRecords;
                    ViewBag.SortAndPage = model;
                }
                ViewBag.ModelSearch = search;
            }
            return PartialView("PartialLogItem", ViewBag.LogItems);
        }
        public ActionResult SortingAndPagingItem(
                                            string loginName,
                                            string sortBy,
                                            bool sortDesc,
                                            string fromDate,
                                            string toDate,
                                            bool? status,
                                            int page,
                                            int pageSize)
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
                var from = !string.IsNullOrEmpty(fromDate)
                               ? DateTime.Parse(fromDate,
                                                System.Globalization.CultureInfo.GetCultureInfo("vi-VN").DateTimeFormat)
                               : (DateTime?)null;
                var to = !string.IsNullOrEmpty(toDate)
                             ? DateTime.Parse(toDate,
                                              System.Globalization.CultureInfo.GetCultureInfo("vi-VN").DateTimeFormat)
                             : (DateTime?)null;
                ViewBag.LogItems = _svcLog.GetAllLogItems(out totalRecords, model.CurrentPageIndex, pageSize, sortDesc, sortBy,
                                                        status: status,
                                                        loginName: string.IsNullOrEmpty(loginName) ? "" : loginName,
                                                        fromDate: from,
                                                        toDate: to);
                model.TotalRecordCount = totalRecords;
                ViewBag.SortAndPage = model;
            }
            ViewBag.ModelSearch = new LogItemSearchModel
            {
                LoginName = loginName,
                FromDate = fromDate,
                ToDate = toDate,
                Status = status
            };

            return PartialView("PartialLogItem", ViewBag.LogItems);
        }

        public ActionResult ActionLog()
        {
            var model = new SortAndPageModel { CurrentPageIndex = 1, SortBy = DefaultActionLogSortBy, SortDescending = true };

            int totalRecords;
            ViewBag.LogItems = _svcLog.GetAllActionLogItems(out totalRecords);

            model.TotalRecordCount = totalRecords;
            ViewBag.ModelSearch = new LogActionSearchModel
            {
                Id = -1,
                LoginName = "",
                ActionContent = "",
                ActionTime = ""
            };
            ViewBag.SortAndPage = model;
            return View();
        }

        public ActionResult SearchActionLog(LogActionSearchModel search, int pageSize)
        {

            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    var model = new SortAndPageModel
                    {
                        CurrentPageIndex = 1,
                        SortBy = DefaultActionLogSortBy,
                        SortDescending = false,
                        PageSize = pageSize
                    };
                    int totalRecords;
                    ViewBag.LogActionItems = _svcLog.GetAllActionLogItems(out totalRecords,
                                                                       pageSize: pageSize,
                                                                       currentPage: model.CurrentPageIndex,
                                                                       loginName: search.LoginName,
                                                                       actionContent: search.ActionContent,
                                                                       actionType: search.ActionType);
                    model.TotalRecordCount = totalRecords;
                    ViewBag.SortAndPage = model;
                }
                ViewBag.ModelSearch = search;
            }
            return PartialView("PartialActionLogItem", ViewBag.LogActionItems);
        }
        public ActionResult SortingAndPagingActionLog(
                                            string loginName,
                                            string sortBy,
                                            bool sortDesc,
                                            string actionContent,
                                            LogActionType? actionType,
                                            int page,
                                            int pageSize)
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
                ViewBag.LogActionItems = _svcLog.GetAllActionLogItems(out totalRecords, model.CurrentPageIndex, pageSize, sortDesc, sortBy,
                                                        string.IsNullOrEmpty(loginName) ? "" : loginName,
                                                        string.IsNullOrEmpty(actionContent) ? "" : actionContent,
                                                        actionType.HasValue ? actionType : null);
                model.TotalRecordCount = totalRecords;
                ViewBag.SortAndPage = model;
            }
            ViewBag.ModelSearch = new LogActionSearchModel
            {
                LoginName = loginName,
                ActionContent = actionContent,
                ActionType = actionType
            };

            return PartialView("PartialActionLogItem", ViewBag.LogActionItems);
        }
        public ActionResult Details(int id)
        {
            var actionLog = _svcLog.GetById(id);
            if (actionLog == null)
            {
                return RedirectToAction("Index");
            }
            if (actionLog.DataBeforeChange == null && actionLog.DataAfterChange == null)
            {
                return RedirectToAction("Index");
            }
            return View(actionLog);
        }
    }
}
