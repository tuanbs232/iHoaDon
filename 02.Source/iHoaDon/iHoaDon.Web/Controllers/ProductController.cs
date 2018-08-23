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
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        private const string DefaultSortBy = "Id";
        private readonly ProductService _product;
        private readonly UnitService _unit;

        public ProductController(ProductService product, UnitService unit)
        {
            _product = product;
            _unit = unit;
        }
        public ActionResult Index(string sortBy, int? page)
        {
            int p = 1;
            int numOfRec = 20;
            if(page != null && page.HasValue)
            {
                p = page.GetValueOrDefault();
            }

            try
            {
                var model = new SortAndPageModel
                {
                    CurrentPageIndex =p,
                    SortBy = DefaultSortBy,
                    SortDescending = false,
                    PageSize = numOfRec
                };

                int totalRecords;
                ViewBag.Product = _product.GetAllProduct(out totalRecords, p, numOfRec);
                model.TotalRecordCount = totalRecords;
                ViewBag.SortAndPage = model;

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.ToString();
                return RedirectToAction("Index", "Error", new { area = "" });
            }
        }
        [HttpGet]
        public ActionResult Add(int? id)
        {
            int accId = User.GetAccountId();
            if (id == null || id == 0)
            {
                return View(new ProductModel()
                {
                    ListUnitId = UnitIdls()
                });
            }
            else
            {
                var product = _product.GetById(id.GetValueOrDefault());
                return View(new ProductModel()
                {
                    ListUnitId = UnitIdls(),
                    ProductID = product.Id,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    UnitID = Convert.ToInt32(product.UnitID),
                    Price = product.Price.ToString(),
                    Note = product.Note
                });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ProductModel productModel)
        {
            int accId = User.GetAccountId();
            if (ModelState.IsValid)
            {
                try
                {
                    if (productModel.ProductID == 0)
                    {

                        var product = new Product
                        {
                            Id = productModel.ProductID,
                            ProductCode = productModel.ProductCode,
                            ProductName = productModel.ProductName,
                            UnitID = productModel.UnitID,
                            Price = decimal.Parse(productModel.Price),
                            Note = productModel.Note,
                            AccountId = accId
                        };
                        _product.Create(product);
                        ViewBag.SaveSuccess = true;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var productEdit = _product.GetById(productModel.ProductID);
                        productEdit.Id = productModel.ProductID;
                        productEdit.ProductCode = productModel.ProductCode;
                        productEdit.ProductName = productModel.ProductName;
                        productEdit.UnitID = productModel.UnitID;
                        productEdit.Price = decimal.Parse(productModel.Price);
                        productEdit.Note = productModel.Note;
                        productEdit.AccountId = accId;

                        _product.Create(productEdit);

                        ViewBag.SaveSuccess = true;
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex);
                }
            }
            productModel.ListUnitId = UnitIdls();
            return View(productModel);
        }

        public IEnumerable<SelectListItem> UnitIdls()
        {
            var lstItem = new List<SelectListItem>
                {
                    new SelectListItem() { Value = "", Text = @"-Chọn đơn vị tính-" }
                };
            lstItem.AddRange(_unit.GelAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }));
            return lstItem;
        }
    }
}
