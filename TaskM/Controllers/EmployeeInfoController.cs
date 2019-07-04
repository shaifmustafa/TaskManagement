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
using Models.VM;

namespace TaskM.Controllers
{
    public class EmployeeInfoController : Controller
    {
        EmployeeInfoManager employeeInfoManager = new EmployeeInfoManager();
        DepartmentManager departmentManager = new DepartmentManager();
        CompanyListManager companyListManager = new CompanyListManager();
        RankListManager rankListManager = new RankListManager();


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
            var departmentId = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
            var companyListId = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();
            var rankListId = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();
            var mobile = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault().ToLower();
            //var emergencyMobile = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault().ToLower();
            var address = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault().ToLower();
            var email = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault().ToLower();
            var designation = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault().ToLower();
            var entryDate = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault().ToLower();
            //var dob = Request.Form.GetValues("columns[10][search][value]").FirstOrDefault().ToLower();
            var joiningDate = Request.Form.GetValues("columns[9][search][value]").FirstOrDefault().ToLower();


            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;

            var employeeInfo = employeeInfoManager.GetAll().Where(s => s.IsActive);
            var department = departmentManager.GetAll();
            var companyList = companyListManager.GetAll();
            var rankList = rankListManager.GetAll();

            var query = (from e in employeeInfo
                         join d in department on e.DepartmentId equals d.Id
                         join c in companyList on e.CompanyListId equals c.Id
                         join r in rankList on e.RankListId equals r.Id
                         select new EmployeeInfoVm
                         {
                             Id = e.Id,
                             DepartmentId = e.DepartmentId,
                             CompanyListId = e.CompanyListId,
                             RankListId = e.RankListId,
                             Name = e.Name,                             
                             DepartmentName = d.Name,       
                             CompanyListName = c.Name,
                             RankListName = r.Name,
                             Mobile = e.Mobile,
                             //EmergencyMobile = e.EmergencyMobile,
                             Address = e.Address,
                             Email = e.Email,
                             Designation = e.Designation,
                             EntryDate = e.EntryDate,
                             //Dob = e.Dob,
                             JoiningDate = e.JoiningDate
                         });            

            var total = query.Count();

            //SEARCHING...
            query = query.Where(q => q.Name.ToLower().Contains(name) || string.IsNullOrEmpty(name));
            query = query.Where(q => q.DepartmentId.ToString() == departmentId || string.IsNullOrEmpty(departmentId));
            query = query.Where(q => q.CompanyListId.ToString().ToLower().Contains(companyListId) || string.IsNullOrEmpty(companyListId));
            query = query.Where(q => q.RankListId.ToString().ToLower().Contains(rankListId) || string.IsNullOrEmpty(rankListId));
            query = query.Where(q => q.Mobile.ToString().ToLower().Contains(mobile) || string.IsNullOrEmpty(mobile));
            query = query.Where(q => q.Address.ToString().ToLower().Contains(address) || string.IsNullOrEmpty(address));
            query = query.Where(q => q.Email.ToString().ToLower().Contains(email) || string.IsNullOrEmpty(email));
            query = query.Where(q => q.Designation.ToString().ToLower().Contains(designation) || string.IsNullOrEmpty(designation));
            query = query.Where(q => q.EntryDate.ToString().ToLower().Contains(entryDate) || string.IsNullOrEmpty(entryDate));
            query = query.Where(q => q.JoiningDate.ToString().ToLower().Contains(joiningDate) || string.IsNullOrEmpty(joiningDate));



            if (!string.IsNullOrEmpty(entryDate))
            {
                var sdEffectiveDate = DateTime.Parse(entryDate);
                query = query.Where(q => q.EntryDate == sdEffectiveDate);
            }            
            if (!string.IsNullOrEmpty(joiningDate))
            {
                var sdExpiryDate = DateTime.Parse(joiningDate);
                query = query.Where(q => q.JoiningDate == sdExpiryDate);
            }


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
        public JsonResult Save(EmployeeInfo model)
        {
            //if (!ModelState.IsValid) return Json(new { info = "Failed", status = false }, JsonRequestBehavior.AllowGet);
            if (employeeInfoManager.SaveOrUpdate(model))
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public JsonResult GetDetails(Int64 id)
        {
            var res = employeeInfoManager.GetById(id);
            var resText = new EmployeeInfoVm
            {
                Id = res.Id,
                Name = res.Name,
                DepartmentId = res.DepartmentId,
                CompanyListId = res.CompanyListId,
                RankListId = res.RankListId,
                Mobile = res.Mobile,
                EmergencyMobile = res.EmergencyMobile,
                Address = res.Address,
                Email = res.Email,
                Designation = res.Designation,
                EntryDate = res.EntryDate,
                Dob = res.Dob,
                JoiningDate = res.JoiningDate
            };
            return Json(new { Data = resText, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Delete(Int64 Id)
        {
            if (employeeInfoManager.Delete(Id))
                return Json(new { info = "Deleted", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Deleted", status = false }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult EmployeeList()
        {
            return Json(new
            {
                Data = new EmployeeInfoManager().GetAll().Where(w => w.IsActive).Select(x => new {
                    x.Id,
                    x.Name
                }).ToList(),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}