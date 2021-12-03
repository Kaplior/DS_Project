using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Models
{
    public class DS_Model
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int Comp_num { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
