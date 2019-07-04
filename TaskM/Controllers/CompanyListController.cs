using Bll.Managers;
using Models.DatabaseContext;
using Models.Entity_Models;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;


namespace TaskM.Controllers
{
    public class CompanyListController : Controller
    {
        CompanyListManager companyListManager = new CompanyListManager();


        // GET: Department
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GetList()
        {
            //jQuery DataTables Param
            var draw = Request.Form.GetValues("draw").FirstOrDefault();

            //Find paging info
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            //Find order columns info
            var sortColumnIndex = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortColumnName = Request.Form.GetValues("columns[" + sortColumnIndex + "][data]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            //find search columns info
            var search = Request.Form.GetValues("search[value]").FirstOrDefault().ToLower();
            var name = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
            var shortName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
            var contactPerson = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();
            var contactMobile = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();
            var registerNo = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault().ToLower();
            var address = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault().ToLower();
            var status = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault().ToLower();            


            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;
            var query = companyListManager.GetAll().Where(s => s.IsActive);
            var total = query.Count();

            //SEARCHING(Loading Table Data)...
            query = query.Where(q => (q.Name.ToLower().Contains(name) || string.IsNullOrEmpty(name)
                                       ) &&
                                       (
                                         q.ShortName.ToString().ToLower().Contains(shortName) || string.IsNullOrEmpty(shortName)
                                       ) &&
                                       (
                                         q.ContactPerson.ToString().ToLower().Contains(contactPerson) || string.IsNullOrEmpty(contactPerson)
                                       ) &&
                                       (
                                         q.ContactMobile.ToString().ToLower().Contains(contactMobile) || string.IsNullOrEmpty(contactMobile)
                                       ) &&
                                       (
                                         q.RegisterNo.ToString().ToLower().Contains(registerNo) || string.IsNullOrEmpty(registerNo)
                                       ) && 
                                       (
                                         q.Address.ToString().ToLower().Contains(address) || string.IsNullOrEmpty(address)
                                       ) &&
                                       (
                                         q.Status.ToString().ToLower().Contains(status) || string.IsNullOrEmpty(status)
                                       )
                                     );
            //SORTING...  (For sorting we need to add a reference System.Linq.Dynamic)
            if (!(string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(sortColumnDir)))
            {
                query = query.OrderBy(sortColumnName + " " + sortColumnDir);

            }
            var filtered = query.Count();
            if (pageSize != -1)
            {
                query = query.Skip(skip).Take(pageSize);
            }

            return Json(new { draw, recordsFiltered = filtered, recordsTotal = total, data = query.ToList() },
                    JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Save(CompanyList model)
        {
            //if (!ModelState.IsValid) return Json(new { info = "Failed", status = false }, JsonRequestBehavior.AllowGet);
            if (companyListManager.SaveOrUpdate(model))
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetDetails(Int64 id)
        {
            var res = companyListManager.GetById(id);
            return Json(new { Data = res, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Delete(Int64 Id)
        {
            if (companyListManager.Delete(Id))
                return Json(new { info = "Deleted", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Deleted", status = false }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult CompanyList()
        {
            return Json(new
            {
                Data = new CompanyListManager().GetAll().Where(w => w.IsActive).Select(x => new {
                    x.Id,
                    x.Name
                }).ToList(),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}