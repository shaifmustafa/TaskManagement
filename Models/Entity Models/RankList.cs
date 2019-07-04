using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entity_Models
{
    public class RankList : Entity
    {
        public Int64 Id { get; set; }

        [Index]
        [MaxLength(100)]
        [Required]
        public String Name { get; set; }

        public String Detail { get; set; }
        public String Status { get; set; }
    }
}
