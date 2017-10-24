using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models
{
    public class LostAndFoundRoles
    {
        [Key]
        public int roleId { get; set; }
        public string roleName { get; set; }
    }
}