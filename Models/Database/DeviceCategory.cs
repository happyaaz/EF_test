using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LostAndFound.Models.Database
{
    public class DeviceCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int deviceCategoryId { get; set; }
        public string deviceCategoryName { get; set; }
    }
}