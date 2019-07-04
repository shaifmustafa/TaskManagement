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
    public class GroupController : Controller
    {        
        GroupManager groupManager = new GroupManager();
        GroupDetailsManager groupDetailsManager = new GroupDetailsManager();
        EmployeeInfoManager employeeInfoManager = new EmployeeInfoManager();

        public static Int64 groupId = 0;
        public static Int64 groupDetailsId = 0;

        GroupDetails groupDetails = new GroupDetails();

        // GET: Group
        public ActionResult Index()
        {
            var groupDetailsList = groupManager.GetAll();
            return View(groupDetailsList);
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
            var detail = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();
            //var employeeId = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;

            var query = groupManager.GetAll().Where(s => s.IsActive);
            var employeeList = employeeInfoManager.GetAll();

            //var query = (from g in groupList
            //             join e in employeeList on g.EmployeeInfoId equals e.Id
            //             select new GroupVm
            //             {
            //                 Id = g.Id,
            //                 Name = g.Name,
            //                 EmployeeInfoId = g.EmployeeInfoId,
            //                 EmployeeInfoName = e.Name,
            //                 ShortName = g.ShortName,
            //                 Details = g.Details,
            //                 Logo = g.Logo,
            //                 Banner = g.Banner
            //             });


            var total = query.Count();

            //SEARCHING(Loading Table Data)...

            query = query.Where(q => q.Name.ToLower().Contains(name) || string.IsNullOrEmpty(name));
            //query = query.Where(q => q.EmployeeInfoId.ToString() == employeeId || string.IsNullOrEmpty(employeeId));
            query = query.Where(q => q.ShortName.ToLower().Contains(shortName) || string.IsNullOrEmpty(shortName));
            query = query.Where(q => q.Details.ToLower().Contains(detail) || string.IsNullOrEmpty(detail));


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
        public JsonResult Save(Group model)
        {                        
            if (groupManager.SaveOrUpdate(model))
            {                
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult SaveEditData(GroupDetails model)
        {
            if (model.EmployeeArray.Count() > 0)
            {
                for (var i = 0; i < model.EmployeeArray.Count(); i++) 
                {
                    model.GroupId = groupId;
                    model.EmployeeInfoId = Int64.Parse(model.EmployeeArray[i]);

                    groupDetailsManager.SaveOrUpdate(model);                    
                }

                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);
            }            
        }


        [HttpPost]
        public JsonResult GetDetails(Int64 id)
        {
            var res = groupManager.GetById(id);
            var resText = new GroupVm
            {
                Id = res.Id,
                EmployeeArray = res.EmployeeArray,
                Name = res.Name,
                //EmployeeInfoId = res.EmployeeInfoId,
                EmployeeInfoName = res.Name,
                ShortName = res.ShortName,
                Details = res.Details,
                Logo = res.Logo,
                Banner = res.Banner
            };
            return Json(new { Data = resText, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetEmployees(Int64 id)
        {          
            var groupDetailsList = groupDetailsManager.GetAll();
            var employeeList = employeeInfoManager.GetAll();
            var groupList = groupManager.GetAll();

            Group group = new Group();
            groupId = id;

            var query = (from g in groupDetailsList
                          join e in employeeList on g.EmployeeInfoId equals e.Id where g.GroupId == id
                          where g.IsActive == true join gr in groupList on g.GroupId equals gr.Id
                          select new GroupDetailsVm
                          {
                              Id = g.Id,
                              GroupId = g.GroupId,
                              EmployeeInfoId = g.EmployeeInfoId,
                              EmployeeInfoName = e.Name,
                              GroupName = gr.Name
                          }).ToList();

            //query = groupDetailsManager.GetAll().Where(x => x.GroupId == id).Select(x => x.EmployeeInfoId).ToList();
            
            return Json(new { Data = query/*, status = query == null ? false : true*/ }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Delete(Int64 Id)
        {
            if (groupManager.Delete(Id))
                return Json(new { info = "Deleted", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Deleted", status = false }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteEmployee(Int64 Id)
        {
            var groupDetailsList = groupDetailsManager.GetAll();

            var query = (from g in groupDetailsList
                         where g.EmployeeInfoId.Equals(Id)
                         select g.Id).FirstOrDefault();

            if (groupDetailsManager.Delete(Id))
                return Json(new { info = "Deleted", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Deleted", status = false }, JsonRequestBehavior.AllowGet);

        }
    }
}