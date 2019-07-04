using Bll.Managers;
using Models.Entity_Models;
using Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;


namespace TaskM.Controllers
{
    public class TaskAssignController : Controller
    {
        // GET: TaskAssign

        DepartmentManager departmentManager = new DepartmentManager();
        GroupManager groupManager = new GroupManager();
        EmployeeInfoManager employeeInfoManager = new EmployeeInfoManager();
        MemberManager memberManager = new MemberManager();
        TaskAssignManager taskAssignManager = new TaskAssignManager();
        CommentOnTaskManager commentOnTaskManager = new CommentOnTaskManager();
        ChatMessageManager chatMessageManager = new ChatMessageManager();


        public static Int64 taskAssignId = 0;

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GetList(string Status)
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

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;

            IEnumerable<TaskAssign> taskList = null;

            if (Status == "" || Status == null)
            {
                taskList = taskAssignManager.GetAll().Where(s => s.IsActive);
            }
            else
            {
                taskList = taskAssignManager.GetAll().Where(s => s.IsActive && s.Status == Status);                
            }

            var query = (from t in taskList
                         select new TaskAssignVm
                         {
                             Id = t.Id,
                             TaskName = t.TaskName,
                             TaskShortName = t.TaskShortName,
                             TaskAssignTypeId = t.TaskAssignTypeId,
                             TaskAssignType = t.TaskAssignType,
                             TaskType = t.TaskType,
                             Status = t.Status,
                             StartDate = t.StartDate,
                             EndDate = t.EndDate
                         });

            var total = query.Count();

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
        public JsonResult Save(TaskAssign model)
        {
            if (model.TaskAssignType == "Department")
            {
                var dept = departmentManager.GetById(model.TaskAssignTypeId);

                model.TaskType = dept.Name;
            }
            else if (model.TaskAssignType == "Group")
            {
                var group = groupManager.GetById(model.TaskAssignTypeId);

                model.TaskType = group.Name;
            }
            else if (model.TaskAssignType == "Employee")
            {
                var employee = employeeInfoManager.GetById(model.TaskAssignTypeId);

                model.TaskType = employee.Name;
            }
            else if (model.TaskAssignType == "Member")
            {
                var member = memberManager.GetById(model.TaskAssignTypeId);

                model.TaskType = member.Name;
            }



            if (taskAssignManager.SaveOrUpdate(model))
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetDetails(Int64 id)
        {
            var res = taskAssignManager.GetById(id);
            var group = groupManager.GetById(res.TaskAssignTypeId);

            var resText = new CommentOnTaskVm
            {
                Id = res.Id,
                TaskName = res.TaskName,
                TaskShortName = res.TaskShortName,
                TaskAssignType = res.TaskAssignType,
                TaskAssignTypeId = res.TaskAssignTypeId,
                TaskDetails = res.TaskDetails,
                TaskType = res.TaskType,
                Status = res.Status,
                Priority = res.Priority,
                StartDate = res.StartDate,
                EndDate = res.EndDate,
                Created = res.Created,
                CreatedBy = res.CreatedBy,
                Modified = res.Modified,
                ModifiedBy = res.ModifiedBy,
                IsActive = res.IsActive
            };

            return Json(new { Data = resText, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveComment(CommentOnTaskVm commentOnTask)
        {
            CommentOnTask commentModel = new CommentOnTask();
            TaskAssign taskModel = new TaskAssign();

            var exist = taskAssignManager.GetById(commentOnTask.Id);

            taskModel.Id = commentOnTask.Id;
            taskModel.Logo = commentOnTask.Logo;
            taskModel.Priority = commentOnTask.Priority;
            taskModel.StartDate = commentOnTask.StartDate;
            taskModel.Status = commentOnTask.Status;
            taskModel.TaskAssignType = commentOnTask.TaskAssignType;
            taskModel.TaskAssignTypeId = commentOnTask.TaskAssignTypeId;
            taskModel.TaskDetails = commentOnTask.TaskDetails;
            taskModel.TaskName = commentOnTask.TaskName;
            taskModel.TaskShortName = commentOnTask.TaskShortName;
            taskModel.TaskType = commentOnTask.TaskType;
            taskModel.Banner = commentOnTask.Banner;
            taskModel.EndDate = commentOnTask.EndDate;

            taskModel.Created = exist.Created;
            taskModel.CreatedBy = exist.CreatedBy;
            taskModel.Modified = exist.Modified;
            taskModel.ModifiedBy = exist.ModifiedBy;
            taskModel.IsActive = exist.IsActive;

            commentModel.TaskAssignId = taskModel.Id;
            commentModel.TaskComment = commentOnTask.Comment;

            if (taskAssignManager.SaveOrUpdate(taskModel))
            {
                commentOnTaskManager.SaveComments(commentModel);
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult ViewChatMessage(Int64 id)
        {
            taskAssignId = id;

            var chatMessages = chatMessageManager.GetAll().Where(x => x.TaskAssignId == id).ToList();

            var messages = (from c in chatMessages where c.TaskAssignId == id
                            select new ChatMessageVm
                            {
                                Id = c.Id,
                                TaskAssignId = c.TaskAssignId,
                                ChatMessageText = c.ChatMessageText,                                
                                Created = c.Created
                            }).OrderByDescending(x => x.Id).ToList();

            return Json(new { Data = messages, status = messages == null ? false : true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendChatMessage(ChatMessage model)
        {
            model.TaskAssignId = taskAssignId;

            if (chatMessageManager.SaveOrUpdate(model))
            {
                var msg = model.ChatMessageText;
                var taskId = model.TaskAssignId;
                var dateTime = model.Created;
                return Json(new { message = msg, tId = taskId, status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Delete(Int64 Id)
        {
            if (taskAssignManager.Delete(Id))
                return Json(new { info = "Deleted", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Deleted", status = false }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetTaskAssignType(string TaskAssignType)
        {
            if (TaskAssignType == "Department")
            {
                return Json(new
                {
                    Data = new DepartmentManager().GetAll().Where(w => w.IsActive).Select(x => new {
                        x.Id,
                        x.Name
                    }).ToList(),
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }

            if (TaskAssignType == "Group")
            {
                return Json(new
                {
                    Data = new GroupManager().GetAll().Where(w => w.IsActive).Select(x => new {
                        x.Id,
                        x.Name
                    }).ToList(),
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }

            if (TaskAssignType == "Employee")
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

            if (TaskAssignType == "Member")
            {
                return Json(new
                {
                    Data = new MemberManager().GetAll().Where(w => w.IsActive).Select(x => new {
                        x.Id,
                        x.Name
                    }).ToList(),
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Data = "No result found"
            }, JsonRequestBehavior.AllowGet);
        }

    }
}