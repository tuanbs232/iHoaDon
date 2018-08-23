using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using iHoaDon.Business;
using iHoaDon.Entities;
using iHoaDon.Resources.Client;
using iHoaDon.Web.Models;
using WebDemoSigning.Filter;

namespace iHoaDon.Web.Controllers
{
    [PublicWebAuthentication]
    public class ListReleaseInvoiceController : Controller
    {
        //
        // GET: /Invoice/
        private const string DefaultSortBy = "Id";
        private readonly TemplateInvoiceService _templateInvoice;
        private readonly ListReleaseInvoiceService _listReleaseInvoice;
        private readonly InvoiceNumberService _invoiceNumber;

        public ListReleaseInvoiceController(TemplateInvoiceService templateInvoice, ListReleaseInvoiceService listReleaseInvoice, InvoiceNumberService invoiceNumber)
        {
            _templateInvoice = templateInvoice;
            _listReleaseInvoice = listReleaseInvoice;
            _invoiceNumber = invoiceNumber;
        }

        public ActionResult Index(string message, string messageType)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var model = new SortAndPageModel
                {
                    CurrentPageIndex = 1,
                    SortBy = DefaultSortBy,
                    SortDescending = false
                };

                int totalRecords;

                var invoiceTemplate = _templateInvoice.GetAll();
                Dictionary<string, string> temp = new Dictionary<string, string>();
                if(invoiceTemplate != null && invoiceTemplate.Count() > 0)
                {
                    foreach(var t in invoiceTemplate)
                    {
                        temp.Add(t.TemplateCode, t.TemplateName);
                    }
                }

                var listRelease =  _listReleaseInvoice.GetAllListReleaseInvoice(out totalRecords);
                foreach(var t in listRelease)
                {
                    if(t == null || string.IsNullOrEmpty(t.TemplateCode) || !temp.ContainsKey(t.TemplateCode))
                    {
                        continue;
                    }
                    t.TemplateName = temp[t.TemplateCode];
                }
                ViewBag.ListReleaseInvoice = listRelease;

                model.TotalRecordCount = totalRecords;
                ViewBag.SortAndPage = model;

                ViewBag.Message = message;
                ViewBag.MessageType = messageType;
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.ToString();
                return RedirectToAction("Index", "Error", new { area = "" });
            }
        }

        public ActionResult SearchListReleaseInvoice(SearchListReleaseInvoiceModel search, int pageSize = 1)
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
                    ViewBag.ListReleaseInvoice = _listReleaseInvoice.GetAllListReleaseInvoice(out totalRecords,
                                                        model.CurrentPageIndex,
                                                        pageSize,
                                                        descending: true);

                    model.TotalRecordCount = totalRecords;
                    ViewBag.SortAndPage = model;
                }
                ViewBag.ModelSearch = search;
            }
            return PartialView("Partial", ViewBag.ListReleaseInvoice);
        }
        public ActionResult Reissueinvoice(string message, string messageType)
        {
            var reissueinvoiceModel = new ReissueinvoiceModel()
                {
                    ListNo = ListNos()
                };

            ViewBag.Message = message;
            ViewBag.MessageType = messageType;
            return View("Reissueinvoice", reissueinvoiceModel);
        }
        [HttpPost]
        public ActionResult Reissueinvoice(ReissueinvoiceModel reiinv)
        {

            int accId = User.GetAccountId();
            var reissueinvoiceModel = new ReissueinvoiceModel()
            {
                ListNo = ListNos()
            };

            DateTime dateTime = DateTime.Parse(reiinv.StartUsingDate.ToString(), System.Globalization.CultureInfo.
                                                                             GetCultureInfo("vi-VN").
                                                                             DateTimeFormat);
            //if (dateTime < DateTime.Now)
            //{
            //    ModelState.AddModelError(String.Empty, ReissueinvoiceResource.StartUsingDateEqua.ToString());
            //    return View(reissueinvoiceModel);
            //}
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    var listReleaseInvoice = new ListReleaseInvoice()
                        {
                            Quantity = reiinv.Quantity,
                            StartNumber = reiinv.StartNumber,
                            EndNumber = reiinv.EndNumber,
                            StartUsingDate = dateTime,
                            AccountId = accId,
                            TemplateCode = reiinv.TemplateId,
                            Status = 0,
                            SerialInvoice = reiinv.SerialInvoice,
                            No = reiinv.No

                        };
                    _listReleaseInvoice.CreateListReleaseInvoices(listReleaseInvoice);

                    for (int i = reiinv.StartNumber; i <= reiinv.EndNumber; i++)
                    {
                        var invoiceNumber = new InvoiceNumber()
                            {
                                InvoicesNumber = i,
                                UseStatus = 0,
                                ReleaseId = listReleaseInvoice.Id,
                                Status = 0,
                                AccountId = accId
                            };
                        _invoiceNumber.CreateInvoiceNumbers(invoiceNumber);
                    }
                    scope.Complete();
                }
                return RedirectToAction("Index", new {message="Phát hành hóa đơn thành công. Xem lại trên giao diện", messageType="info" });
            }
            else
            {
                return View(reissueinvoiceModel);
            }

        }
        [HttpGet]
        public ActionResult Cancelinvoice(int id)
        {
            int accId = User.GetAccountId();
            if (id == 0)
            {
                var reissueinvoiceModel = new CancelinvoiceModel()
                {
                    ListNo = ListNoCancel(accId)
                };
                return View(reissueinvoiceModel);
            }
            else
            {
                var lri = _listReleaseInvoice.GetById(id);
                var reissueinvoiceModel = new CancelinvoiceModel()
                {
                    Id = id,
                    ListNo = ListNoCancel(accId),
                    Quantity = lri.Quantity,
                    StartNumber = lri.StartNumber,
                    EndNumber = lri.EndNumber,
                    StartUsingDate = lri.StartUsingDate.ToString(),
                    TemplateId = lri.TemplateCode,
                    SerialInvoice = lri.SerialInvoice,
                    No = lri.No

                };
                return View(reissueinvoiceModel);
            }
        }

        [HttpPost]
        public ActionResult Cancelinvoice(CancelinvoiceModel reiinv)
        {
            int accId = User.GetAccountId();
            var cancelinvoiceModel = new CancelinvoiceModel()
            {
                ListNo = ListNoCancel(accId)
            };

            DateTime dateTime = DateTime.Parse(reiinv.StartUsingDate.ToString(), System.Globalization.CultureInfo.
                                                                             GetCultureInfo("vi-VN").
                                                                             DateTimeFormat);
            //if (dateTime < DateTime.Now)
            //{
            //    ModelState.AddModelError(String.Empty, ReissueinvoiceResource.StartUsingDateEqua.ToString());
            //    return View(reissueinvoiceModel);
            //}
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    var listReleaseInvoice = new ListReleaseInvoice()
                    {
                        Quantity = reiinv.Quantity,
                        StartNumber = reiinv.StartNumber,
                        EndNumber = reiinv.EndNumber,
                        StartUsingDate = dateTime,
                        AccountId = accId,
                        TemplateCode = reiinv.TemplateId,
                        Status = 0,
                        SerialInvoice = reiinv.SerialInvoice,
                        No = reiinv.No

                    };
                    //_listReleaseInvoice.CreateListReleaseInvoices(listReleaseInvoice);
                    ListReleaseInvoice list = null;
                    try
                    {
                        list = _listReleaseInvoice.GetById(reiinv.Id);
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError("", "Không tìm thấy thông tin phát hành");
                        return View(reiinv);
                    }
                    
                    if(list == null)
                    {
                        ModelState.AddModelError("", "Không tìm thấy thông tin phát hành");
                        return View(reiinv);
                    }

                    var inUse = _invoiceNumber.GetByReleaseIdAnduseStatus(list.Id, 0);
                    if(inUse == null || inUse.Count() < list.Quantity)
                    {
                        ModelState.AddModelError("", "Đã xuất hóa đơn trên phát hành này. Không thể xóa");
                        return View(reiinv);
                    }

                    foreach (var invoiceNum in inUse)
                    {
                        _invoiceNumber.DeleteInvoiceNumbers(invoiceNum);
                    }

                    _listReleaseInvoice.CancelListReleaseInvoices(list);

                    scope.Complete();
                }
                return RedirectToAction("Index", new { message = "Hủy phát hành hóa đơn thành công", messageType = "info" });
            }
            else
            {
                return View(cancelinvoiceModel);
            }
        }

        public IEnumerable<SelectListItem> ListNos()
        {
            var lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem() { Value = "", Text = "-Chọn loại hóa đợn-" });
            lstItem.AddRange(_templateInvoice.GetAll().Select(c => new SelectListItem { Text = c.TemplateName, Value = c.TemplateCode.ToString() }));
            return lstItem;
        }
        public IEnumerable<SelectListItem> ListNoCancel(int accId)
        {
            var lstItem = new List<SelectListItem> { new SelectListItem() { Value = "", Text = "-Chọn loại hóa đợn-" } };
            var listGroup = _listReleaseInvoice.GetByAcc(accId).GroupBy(c => c.TemplateCode).ToList();
            var tempInv = _templateInvoice.GetAll();
            lstItem.AddRange(from item in tempInv from ite in listGroup where item.TemplateCode == ite.Key select new SelectListItem() { Value = item.TemplateCode, Text = item.TemplateName });
            return lstItem;
        }

        [HttpGet]
        public ActionResult ListNoTemp(string templateCode)
        {
            int accId = User.GetAccountId();
            var list = _listReleaseInvoice.GetByAccTemp(accId, templateCode).GroupBy(c => c.No);
            var lstItem = list.Select(item => new SelectListItem() {Value = item.Key, Text = item.Key.ToString()}).ToList();
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }
    }
}
