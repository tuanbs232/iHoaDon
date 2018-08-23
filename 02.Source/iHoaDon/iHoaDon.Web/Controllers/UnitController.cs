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
    public class UnitController : Controller
    {
        //
        // GET: /Unit/
        // GET: /Unit/
        private const string DefaultSortBy = "Id";
          private readonly UnitService _unit;

          public UnitController(UnitService unit)
        {
            _unit = unit;
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
                ViewBag.Unit = _unit.GetAllUnit(out totalRecords);
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
        public ActionResult Add(int id)
        {
           
            if (id == 0)
            {
                return View();
            }
            else
            {
                var unit = _unit.GetById(id);
                return View(new UnitModel()
                {
                    Id = unit.Id,
                    Name = unit.Name
                });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(UnitModel unitModel)
        {
            int accId = User.GetAccountId();
            if (ModelState.IsValid)
            {
                try
                {
                    if (unitModel.Id == 0)
                    {
                        var unit = new Unit()
                        {
                            Id = unitModel.Id,
                            Name = unitModel.Name
                        };
                        
                        _unit.Create(unit);
                        ViewBag.SaveSuccess = true;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var cunitEdit = _unit.GetById(unitModel.Id);
                        cunitEdit.Id = unitModel.Id;
                        cunitEdit.Name = unitModel.Name;
                        
                        _unit.Create(cunitEdit);

                        ViewBag.SaveSuccess = true;
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex);
                }
            }

            return View(unitModel);
        }
    }
}
