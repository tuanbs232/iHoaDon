using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iHoaDon.Business;
using iHoaDon.Web.Models;

namespace iHoaDon.Web.Controllers
{
    public class SearchInvoiceController : Controller
    {
       //
        // GET: /Invoice/
        private const string DefaultSortBy = "Id";
        private readonly InvoiceService _invoice;
        private readonly TemplateInvoiceService _templateInvoice;
        public SearchInvoiceController(InvoiceService invoice, TemplateInvoiceService templateInvoice)
        {
            _invoice = invoice;
            _templateInvoice = templateInvoice;
        }
        public ActionResult Index()
        {
            try
            {
                var model = new SortAndPageModel
                {
                    CurrentPageIndex = 1,
                    SortBy = DefaultSortBy,
                    SortDescending = false
                };

                int totalRecords;
                ViewBag.Invoice = _invoice.GetAllInvoice(out totalRecords);
                model.TotalRecordCount = totalRecords;
                ViewBag.SortAndPage = model;
                var searchInvoiceModel = new SearchInvoiceModel()
                {
                    ListNo = ListNos()
                };
                return View(searchInvoiceModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.ToString();
                return RedirectToAction("Index", "Error", new { area = "" });
            }
        }

        public IEnumerable<SelectListItem> ListNos()
        {
            var lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem() { Value = "", Text = "-Chọn loại hóa đợn-" });
            lstItem.AddRange(_templateInvoice.GetAll().Select(c => new SelectListItem { Text = c.TemplateName, Value = c.TemplateCode.ToString() }));
            return lstItem;
        }

    }
}
