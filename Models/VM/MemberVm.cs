﻿using Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.VM
{
    [NotMapped]
    public class MemberVm : Member
    {
        public string DepartmentName { get; set; }
        public string CompanyListName { get; set; }
        public string RankListName { get; set; }
        public string EntryDateText { get { return EntryDate.ToString("yyyy-MM-dd"); } }
        public string RegistrationDateText { get { return RegistrationDate.ToString("yyyy-MM-dd"); } }
        public string JoiningDateText { get { return JoiningDate.ToString("yyyy-MM-dd"); } }
    }
}
