using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LostAndFound.Models.Database
{
    public class DeviceSubcategory
    {
        [Key]
        public int deviceSubcategoryId { get; set; }
        [Column("deviceCategoryId")]
        public virtual DeviceCategory deviceCategory { get; set; }
        public string deviceSubcategoryName { get; set; }
    }
}