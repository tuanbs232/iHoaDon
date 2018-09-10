using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using Bkav.eHoadon.XML.eHoadon.Entity.Message;
using iHoaDon.Business;
using iHoaDon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iHoaDon.Web.Helper;
using iHoaDon.Web.Models;
using invoice = Bkav.eHoadon.XML.eHoadon.Entity.Create.invoice;
using invoiceData = Bkav.eHoadon.XML.eHoadon.Entity.Create.invoiceData;
using invoiceItem = Bkav.eHoadon.XML.eHoadon.Entity.Create.invoiceItem;
using invoiceTaxBreakdownInfo = Bkav.eHoadon.XML.eHoadon.Entity.Create.invoiceTaxBreakdownInfo;
using paymentInfo = Bkav.eHoadon.XML.eHoadon.Entity.Create.paymentInfo;


namespace iHoaDon.Web.Controllers
{
    public class InvoiceMarketController : Controller
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
        private readonly TransactionService _transactionSvc;

        private readonly AccountService _accountSvc;
        public InvoiceMarketController(InvoiceNumberService invoiceNumSvc, ListReleaseInvoiceService listReleaseInvoiceSvc, TemplateInvoiceService templateInvoiceSvc, CustomerService cumtomerSvc, InvoiceService invoiceSvc, BanksService banksSvc, CurrencyService currencySvc, ProductService productSvc, UnitService unitSvc, TransactionService transactionSvc, AccountService accountSvc)
        {
            _transactionSvc = transactionSvc;
            _unitSvc = unitSvc;
            _listReleaseInvoiceSvc = listReleaseInvoiceSvc;
            _templateInvoiceSvc = templateInvoiceSvc;
            _invoiceNumberSvc = invoiceNumSvc;
            _cumtomerSvc = cumtomerSvc;
            _invoiceSvc = invoiceSvc;
            _banksSvc = banksSvc;
            _currencySvc = currencySvc;
            _productSvc = productSvc;
            _accountSvc = accountSvc;
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = "02GTTT";
            }
            //lấy ra danh sách tờ khai thuộc GTGT
            var accountId = User.GetAccountId();
            var acc = _accountSvc.GetById(accountId);
            var lstInvoiceNum = _listReleaseInvoiceSvc.GetByTemplateId(id, accountId);
            if (lstInvoiceNum == null || lstInvoiceNum.Count() < 1)
            {
                return RedirectToAction("Index", "ListReleaseInvoice", new { message = "Chưa phát hành hóa đơn. Vui lòng chọn phát hành hóa đơn trước", messageType = "error" });
            }

            var invoiceModel = new InvoiceModel();
            invoiceModel.RelesaseNos = lstInvoiceNum.Select(c => new SelectListItem { Text = c.No, Value = c.Id + "" });
            invoiceModel.BanksSeller = _banksSvc.GelByAccountId(accountId);
            invoiceModel.Currencies = _currencySvc.GelAll().Select(c => new SelectListItem { Text = c.CurrencyCode, Value = c.Id + "" });
            invoiceModel.Products = _productSvc.GetByAccountId(accountId);
            invoiceModel.Units = _unitSvc.GelAll();
            
            //thông tin người bán
            invoiceModel.CompanyNameAcc = acc.CompanyName;
            invoiceModel.CompanyCodeAcc = acc.CompanyCode;
            invoiceModel.AddressAcc = acc.Address;
            invoiceModel.PhoneAcc = acc.Phone;
            ViewBag.TemplateCode = id;
            ViewBag.TemplateName = _templateInvoiceSvc.GetByTemplateCode(id).TemplateName;

            return View(invoiceModel);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            try
            {

                var invoice = new invoice();

                var invoiceData = new invoiceData();

                var currency = _currencySvc.GetById(int.Parse(formCollection["CurrencyId"]));

                invoice.invoiceData = invoiceData;
                invoiceData.id = "data"; //Thuộc tính ID của thẻ <invoiceData ID="data">

                var sss = Bkav.eHoadon.XML.eHoadon.Entity.Create.invoiceDataInvoiceType.Item02GTTT;

                //Thông tin chung về hóa đơn
                invoiceData.invoiceAppRecordId = getRandomNumber(999999);
                //ID của bản ghi được quản lý bởi phần mềm Lập hóa đơn của DN.
                invoiceData.invoiceType = sss; //Ký hiệu loại hóa đơn
                invoiceData.invoiceName = "Hóa đơn giá trị gia tăng"; //Tên loại hóa đơn
                var releaseId = -1;
                if (int.TryParse(formCollection["ResleaseIdNo"], out releaseId))
                {
                    var release = _listReleaseInvoiceSvc.GetById(releaseId);
                    invoiceData.templateCode = release.No;
                    invoiceData.invoiceSeries = release.SerialInvoice;
                    var template = _templateInvoiceSvc.GetByTemplateCode(release.TemplateCode);
                    if (template != null)
                    {
                        invoiceData.invoiceName = template.TemplateName;
                    }
                }
                else
                {
                    invoiceData.templateCode = formCollection["ResleaseIdNo"];
                    //"01GTKT0/089";                //Ký hiệu mẫu hóa đơn
                    invoiceData.invoiceSeries = formCollection["Serial"]; //"AC/14E";    
                }
                //"01GTKT0/089";                //Ký hiệu mẫu hóa đơn
                invoiceData.invoiceNumber = formCollection["InvoiceNumber"];
                //createUid(20);               //Số hóa đơn hiện tại có chiều dài 7 chữ số
                
                invoiceData.invoiceIssuedDate = DateTime.Now; //Ngày xuất hóa đơn
                invoiceData.signedDate = DateTime.Now; //Ngày ký số lên hóa đơn, có thể lấy là ngày xuất hóa đơn
                invoiceData.submittedDate = DateTime.Now;
                //Ngày gửi hóa đơn lên CQT, tương đương với ngày đẩy vào thiết bị xác thực, có thể lấy là ngày xuất hóa đơn
                invoiceData.currencyCode = currency != null ? currency.CurrencyCode : "";
                //Ký hiệu mã tiền tệ sử dụng: VND, USD,...
                //invoiceData.isAdjusted = 1;                              //Trạng thái hóa đơn: 1: hóa đơn thường, 5: hóa đơn điều chỉnh,.... trong file Doc mô tả chuẩn XML

                //Thông tin thanh toán hóa đơn
                var paymentInfo = new paymentInfo();
                paymentInfo.paymentMethodName = formCollection["PaymentsType"];
                //"Tiền mặt";                               //Thông tin về phương thức thanh toán hóa đơn: Tiền mặt, Tiền mặt/Chuyển khoản, Chuyển khoản
                List<paymentInfo> payments = invoiceData.payments;
                payments.Add(paymentInfo);

                //Thông tin người bán (Seller)
                //invoiceData.sellerAppRecordId = createUid(20);                            //Tùy doanh nghiệp có thể dùng chung trường ID của bản ghi được quản lý bởi phần mềm Lập hóa đơn của DN.
                invoiceData.sellerLegalName = formCollection["CompanyNameAcc"];
                //"CÔNG TY TNHH DỊCH VỤ TIN HỌC FPT (Demo)";  //Tên doanh nghiệp bán hàng hóa dịch vụ

                var random = new Random();
                //String tin = allowTin[random.Next(allowTin.Length)];
                //invoiceData.sellerTaxCode = tin;                                          //Mã số thuến người bán
                invoiceData.sellerAddressLine = formCollection["AddressAcc"];
                //"Tầng 6 Tòa nhà Thành Công, Dịch Vọng Hậu, Cầu Giấy, Hà Nội";  //Địa chỉ người bán
                invoiceData.sellerPhoneNumber = formCollection["PhoneAcc"];
                //"0812345678";                                                  //Số điện thoại người bán
                invoiceData.sellerFaxNumber = formCollection["PhoneAcc"];
                //invoiceData.sellerContactPersonName = formCollection["sellerAddressLine"];// "Đỗ C";                                            //Tên người đại diện công ty đăng ký kinh doanh
                //invoiceData.sellerEmail = formCollection["sellerAddressLine"]; //"yyy@fpt.com.vn";                                              //email đăng ký kinh doanh
                //invoiceData.sellerSignedPersonName = formCollection["sellerAddressLine"];// "Phạm A";                                   //Người bán hàng hoặc người thực hiện việc xuất hóa đơn
                //invoiceData.sellerSubmittedPersonName = formCollection["sellerAddressLine"]; //"Nguyễn B";                                      //Người thực hiện phê duyệt hoặc gửi hóa đơn đi xác thực

                //Thông tin người mua ( Buyer)            
                invoiceData.buyerLegalName = formCollection["CompanyName"];
                // "Công ty Thử Nghiệm";                                     //Tên doanh nghiệp bán hàng hóa dịch vụ
                invoiceData.buyerDisplayName = formCollection["CustomerName"]; //"Nguyễn Tiến X";
                invoiceData.buyerTaxCode = formCollection["CompanyCode"];
                //"0104128565";                                                       //Mã số thuến người mua

                invoiceData.buyerAddressLine = formCollection["Address"];
                //"15 Nguyễn Du - Hai Bà Trưng - Hà Nội";                   //Địa chỉ người bán
                invoiceData.buyerPhoneNumber = "0812345678"; //Số điện thoại người mua
                invoiceData.buyerFaxNumber = "0812345678";
                invoiceData.buyerEmail = formCollection["BuyerEmail"];
                //"xxx@fpt.com.vn";                                               //email đăng ký kinh doanh          

                invoiceData.invoiceNote = formCollection["Comment"];
                //Thông tin các mặt hàng
                invoiceData.items = new List<invoiceItem>();

                //Thêm mặt hàng thứ nhất
                var invoiceItem = new invoiceItem();

                //invoiceItemList[0].promotion
                uint i = 1;

                var data = formCollection.AllKeys.Count();

                for (int j = 0; j < (data - 27) / 10; j++)
                {
                    invoiceItem = new invoiceItem();
                    var vatCategoryPercentage = 0;
                    int.TryParse(formCollection[string.Format("invoiceItemList[{0}].vatCategoryPercentage", j)],
                                 out vatCategoryPercentage);

                    var itemTotalAmountWithoutVat = 0;
                    int.TryParse(formCollection[string.Format("invoiceItemList[{0}].itemTotalAmountWithoutVat", j)],
                                 out itemTotalAmountWithoutVat);
                    invoiceItem.lineNumber = i; //Dòng hóa đơn
                    invoiceItem.itemCode = formCollection[string.Format("invoiceItemList[{0}].itemCode", j)];
                    //Mã hàng hóa
                    invoiceItem.itemName = formCollection[string.Format("invoiceItemList[{0}].itemName", j)];
                    //Tên hàng hóa
                    invoiceItem.unitName = formCollection[string.Format("invoiceItemList[{0}].unitCode", j)];
                    ; //Đơn vị tính
                    invoiceItem.quantity = int.Parse(formCollection[string.Format("invoiceItemList[{0}].quantity", j)]);
                    //Số lượng                        
                    invoiceItem.unitPrice = int.Parse(formCollection[string.Format("invoiceItemList[{0}].unitPrice", j)]);
                    //Đơn giá
                    invoiceItem.vatPercentage = vatCategoryPercentage;
                    //Thuế xuất của mặt hàng: -2: không kê khai thuế, -1: không chịu thuế, 0: 0%, 5:5%, 10:10%.....
                    invoiceItem.itemTotalAmountWithoutVat = itemTotalAmountWithoutVat;
                    //Tổng tiền chưa thuế của dòng hóa đơn: = số lượng * đơn giá
                    invoiceData.items.Add(invoiceItem);
                    i++;
                }
                //invoiceData.invoiceTaxBreakdowns = new List<invoiceTaxBreakdownInfo>();

                ////Tạo thông tin cho mức thuế xuất 5%
                //var invoiceTaxBreakdownInfo = new invoiceTaxBreakdownInfo();
                //invoiceTaxBreakdownInfo.vatPercentage = 5; //Mức thuế 
                //invoiceTaxBreakdownInfo.vatTaxableAmount = 42000000; //Tổng tiền trên hóa đơn chịu mức thuế xuất này
                //invoiceTaxBreakdownInfo.vatTaxAmount = 2100000; //Tổng tiền thuế của mức thuế xuất này
                //invoiceData.invoiceTaxBreakdowns.Add(invoiceTaxBreakdownInfo);

                ////Tạo thông tin cho mức thuế xuất 10%
                //invoiceTaxBreakdownInfo = new invoiceTaxBreakdownInfo();
                //invoiceTaxBreakdownInfo.vatPercentage = 10; //Mức thuế 
                //invoiceTaxBreakdownInfo.vatTaxableAmount = 48000000; //Tổng tiền trên hóa đơn chịu mức thuế xuất này
                //invoiceTaxBreakdownInfo.vatTaxAmount = 4800000; //Tổng tiền thuế của mức thuế xuất này

                //invoiceData.invoiceTaxBreakdowns.Add(invoiceTaxBreakdownInfo);

                var totalAmountWithoutVAT = 0;
                int.TryParse(formCollection["totalAmountWithoutVat"], out totalAmountWithoutVAT);//Tổng tiền không chịu thuế trên toàn hóa đơn
                invoiceData.totalAmountWithoutVAT = totalAmountWithoutVAT;

                //var amountForPaymentVnd = 0;
                //int.TryParse(formCollection["amountForPaymentVnd"], out amountForPaymentVnd);
                //invoiceData.totalVATAmount = amountForPaymentVnd; //Tổng tiền thuế trên toàn hóa đơn
                //invoiceData.totalAmountWithVAT = amountForPaymentVnd; //Tổng tiền đã bao gồm cả thuế trên toàn hóa đơn
                //invoiceData.totalAmountWithVATInWords = formCollection["totalAmtWithVatInWords"];
                //Tổng tiền đã bao gồm cả thuế trên toàn hóa đơn được viết bằng chữ
                invoice.invoiceData = invoiceData;
                string xmlInvoice = invoice.Serialize(Encoding.UTF8);

                var fileName = Guid.NewGuid() + ".xml";
                var pathFile = Path.Combine(Server.MapPath("~/Content/File"), fileName);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlInvoice);
                doc.Save(pathFile);

                //lưu vào bảng transaction/invoice/invoicedetail

                var invoices = new Entities.Invoice();

                if (invoice.invoiceData.currencyCode != null)
                {
                    var currencys = _currencySvc.GetByCode(invoice.invoiceData.currencyCode);
                    if (currencys != null && currencys.Any())
                    {
                        var cus = currencys.First();
                        invoices.CurrencyID = cus.Id;
                        //invoices.Currency = cus.BankName;
                    }

                }
                invoices.InvoiceNote = invoice.invoiceData.invoiceNote;

                if (formCollection[string.Format("CustomerId")] != null)
                    invoices.CustomerInvoiceID = int.Parse(formCollection[string.Format("CustomerId")]);
                invoices.AccountId = User.GetAccountId();



                invoices.InvoiceNumber = invoiceData.invoiceNumber;
                invoices.Serial = invoiceData.invoiceSeries;

                invoices.TotalAmountWithVAT = invoiceData.totalAmountWithoutVAT;
                invoices.TotalAmountWithoutVAT = invoiceData.totalAmountWithoutVAT;
                invoices.TotalVATAmount = invoiceData.totalAmountWithVAT;
                invoices.TemplateCode = formCollection["templateCode"];



                var invoiceDetails = new List<InvoiceDetail>();
                foreach (var item in invoiceData.items)
                {
                    var invoiceDetail = new InvoiceDetail();

                    //invoiceDetail.AdjustmentVatAmount = "";
                    // invoiceDetail.TotalAmountWithoutVAT = invoiceData.totalAmountWithoutVAT;
                    //invoiceDetail.TotalVATAmount = invoiceData.totalAmountWithVAT;
                    //invoiceDetail.TemplateCode = formCollection["templateCode"];

                    invoiceDetails.Add(invoiceDetail);
                }

                var transaction = new Transaction();


                transaction.InvoiceXML = fileName;
                transaction.TemplateCode = invoices.TemplateCode;
                transaction.InvoiceSeries = invoices.Serial;
                int tranId = _invoiceSvc.Create(invoices, transaction);

                if (releaseId > 0)
                {
                    var invoiceNums = _invoiceNumberSvc.GetByReleaseIdAnduseStatus(releaseId, 0);
                    if (invoiceNums != null && invoiceNums.FirstOrDefault() != null)
                    {
                        var invoiceNum = invoiceNums.FirstOrDefault();
                        invoiceNum.UseStatus = 1;
                        invoiceNum.Status = 1;
                        _invoiceNumberSvc.UpdateInvoiceNumbers(invoiceNum);
                    }
                }

                return Json(new ResultInvoice
                                {
                                    Flag = true,
                                    Message = "Lưu dữ liệu thành công",
                                    TransactionId = tranId
                                });
            }
            catch (Exception ex)
            {
                return Json(new ResultInvoice
                {
                    Flag = true,
                    Message = ex.Message,
                    TransactionId = 0
                });
            }
        }

        public string ViewXML(int id)
        {
            var transaction = _transactionSvc.GetById(id);
            string result = string.Empty;
            var mappath = Server.MapPath("~/Content/File");
            string xmlContent = System.IO.File.ReadAllText(string.Format("{0}\\{1}", mappath, transaction.InvoiceXML));
            string xsltContent = System.IO.File.ReadAllText(string.Format("{0}\\01GTKT.xslt", mappath));
            try
            {
                XslCompiledTransform transform = new XslCompiledTransform();
                XsltSettings settings = new XsltSettings(true, true);
                using (XmlReader reader = XmlReader.Create(new StringReader(xsltContent)))
                {
                    transform.Load(reader, settings, new XmlUrlResolver());
                }
                StringWriter results = new StringWriter();
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlContent)))
                {
                    transform.Transform(reader, null, results);
                }
                result = string.IsNullOrEmpty(results.ToString()) ? xsltContent : results.ToString();
            }
            catch (Exception exception)
            {

                System.Diagnostics.Debug.WriteLine(exception);
            }
            return result;
        }
        /// <summary>
        /// Tạo số ngẫu nhiên
        /// </summary>
        /// <param name="max">Độ dài tối đa</param>
        /// <returns></returns>
        private String getRandomNumber(int max)
        {
            var foo = new Random();
            int randomNumber = foo.Next((max + 1) - 0) + 0;
            return randomNumber.ToString();
        }
        //private invoice CreateDataInvoice(FormCollection formCollection)
        //{

        //    return invoice;
        //}

        public ActionResult InvoiceNumber(int id)
        {
            string number = "0";
            var lstRelease = _invoiceNumberSvc.GetByReleaseIdAnduseStatus(id, 0).FirstOrDefault();

            if (lstRelease == null)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {

                var num = lstRelease.InvoicesNumber.ToString();

                for (int i = 1; i < 7 - num.Length; i++)
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
