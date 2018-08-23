using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iHoaDon.Business;
using iHoaDon.Entities;
using iHoaDon.Web.Models;

namespace iHoaDon.Web.Controllers
{
   
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        private const string DefaultSortBy = "Id";
        private readonly CustomerService _customer;

        public CustomerController(CustomerService customer)
        {
            _customer = customer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
        public ActionResult Index(string message, string messageType)
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
                ViewBag.Customer = _customer.GetAllCustomer(out totalRecords);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add(int id)
        {
            int accId = User.GetAccountId();
            if (id == 0)
            {
                return View();
            }
            else
            {
                var customer = _customer.GetById(id);
                return View(new CustomerModel()
                {
                    Id = customer.Id,
                    CustomerName = customer.CompanyName,
                    CompanyName = customer.CompanyName,
                    CompanyCode = customer.CompanyCode,
                    Address = customer.Address,
                    BankAccount = customer.BankAccount,
                    BankName = customer.BankName,
                    Phone = customer.Phone,
                    BuyerEmail = customer.BuyerEmail
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(CustomerModel customerModel)
        {
            int accId = User.GetAccountId();
            if (ModelState.IsValid)
            {
                try
                {
                    if (customerModel.Id == 0)
                    {
                        var customer = new Customer
                            {
                                Id = customerModel.Id,
                                CustomerName = customerModel.CustomerName,
                                CompanyName = customerModel.CompanyName,
                                CompanyCode = customerModel.CompanyCode,
                                Address = customerModel.Address,
                                BankAccount = customerModel.BankAccount,
                                BankName = customerModel.BankName,
                                Phone = customerModel.Phone,
                                BuyerEmail = customerModel.BuyerEmail,
                                AccountId = accId
                            };

                        if (!_customer.Create(customer))
                        {
                            ModelState.AddModelError("", "Có lỗi khi lưu thông tin người mua hàng (Lỗi hệ thống)");
                            return View(customerModel);
                        }

                        ViewBag.SaveSuccess = true;
                        return RedirectToAction("Index", new {message="Thêm mới người mua hàng thành công", messageType="info" });
                    }
                    else
                    {
                        var customerEdit = _customer.GetById(customerModel.Id);
                        customerEdit.Id = customerModel.Id;
                        customerEdit.CustomerName = customerModel.CompanyName;
                        customerEdit.CompanyName = customerModel.CompanyName;
                        customerEdit.CompanyCode = customerModel.CompanyCode;
                        customerEdit.Address = customerModel.Address;
                        customerEdit.BankAccount = customerModel.BankAccount;
                        customerEdit.BankName = customerModel.BankName;
                        customerEdit.Phone = customerModel.Phone;
                        customerEdit.BuyerEmail = customerModel.BuyerEmail;
                        customerEdit.AccountId = accId;

                        if (!_customer.Create(customerEdit))
                        {
                            ModelState.AddModelError("", "Có lỗi khi lưu thông tin người mua hàng (Lỗi hệ thống)");
                            return View(customerModel);
                        }

                        ViewBag.SaveSuccess = true;
                        return RedirectToAction("Index", new { message = "Thêm mới người mua hàng thành công", messageType = "info" });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex);
                }
            }

            return View(customerModel);
        }
    }
}
