using iHoaDon.Business;
using iHoaDon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iHoaDon.Web.Helper;
using iHoaDon.Web.Models;

namespace iHoaDon.Web.Controllers
{
    public class InvoiceTollTariffsController : Controller
    {
          //
        // GET: /Invoice/
        private readonly TemplateInvoiceService _templateInvoiceSvc;
        private readonly UnitService _unitSvc;

        private readonly ListReleaseInvoiceService _listReleaseInvoiceSvc;
        private readonly InvoiceService _invoiceSvc;

        private readonly BanksService _banksSvc;
        private readonly CustomerService _cumtomerSvc;

        private readonly InvoiceNumberService _invoiceNumberSvc;
        private readonly CurrencyService _currencySvc;
        private readonly ProductService _productSvc;

        public InvoiceTollTariffsController(InvoiceNumberService invoiceNumSvc, ListReleaseInvoiceService listReleaseInvoiceSvc, TemplateInvoiceService templateInvoiceSvc, CustomerService cumtomerSvc, InvoiceService invoiceSvc, BanksService banksSvc, CurrencyService currencySvc, ProductService productSvc, UnitService unitSvc)
        {
            _unitSvc = unitSvc;
            _listReleaseInvoiceSvc = listReleaseInvoiceSvc;
            _templateInvoiceSvc = templateInvoiceSvc;
            _invoiceNumberSvc = invoiceNumSvc;
            _cumtomerSvc = cumtomerSvc;
            _invoiceSvc = invoiceSvc;
            _banksSvc = banksSvc;
            _currencySvc = currencySvc;
            _productSvc = productSvc;
        }
        public ActionResult Index(string id)
        {
            //lấy ra danh sách tờ khai thuộc GTGT
            var accountId = User.GetAccountId();
            var lstInvoiceNum = _listReleaseInvoiceSvc.GetByTemplateId(id, accountId);
            var invoiceModel = new InvoiceModel();


            invoiceModel.RelesaseNos = lstInvoiceNum.Select(c => new SelectListItem
                                                                     {
                                                                         Text = c.No,
                                                                         Value = c.Id + ""
                                                                     });


            invoiceModel.BanksSeller = _banksSvc.GelByAccountId(accountId);

            invoiceModel.Currencies = _currencySvc.GelAll().Select(c => new SelectListItem
                                                                            {
                                                                                Text = c.CurrencyCode,
                                                                                Value = c.Id + ""
                                                                            });

            invoiceModel.Products = _productSvc.GetByAccountId(accountId);

            invoiceModel.Units = _unitSvc.GelAll();

            

            return View(invoiceModel);
        }



        public ActionResult InvoiceNumber(int id)
        {
            string number = "0";
            var lstRelease = _invoiceNumberSvc.GetByReleaseIdAnduseStatus(id, 0).FirstOrDefault();

            if (lstRelease==null)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                
                var num = lstRelease.InvoicesNumber.ToString();

                for(int i=1;i<7-num.Length;i++)
                {
                    number += "0";
                }
                number = number + num;

            }
            return Json(number, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ReleaseInvoiceNo(string name)
        {
            var lstRelease = _listReleaseInvoiceSvc.GetByNo(name);
            var lstItem = new List<SelectListItem>();
            foreach (var item in lstRelease)
            {
                lstItem.Add(new SelectListItem
                                {
                                    Text = item.SerialInvoice,
                                    Value = item.Id + ""
                                });
            }
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Customers(string name)
        {
            var lstRelease = _cumtomerSvc.GetByCompanyName(name);
            return Content(PartialView("_PartialCustomer", lstRelease).Capture(ControllerContext));

        }


        public ActionResult Banks(int id)
        {
            var bank = _banksSvc.GetById(id);
            return Json(bank, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Currencys(int id)
        {
            var bank = _currencySvc.GetById(id);
            return Json(bank, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Products(string itemCode)
        {
            var product = _productSvc.GetByCode(itemCode);
            
            return Json(new Products
                            {
                                Price = product.Price,
                                ProductCode = product.ProductCode,
                                ProductID = product.Id,
                                ProductName = product.ProductName,
                                UnitId = product.UnitID
                            }, JsonRequestBehavior.AllowGet);

        }

    }
}
