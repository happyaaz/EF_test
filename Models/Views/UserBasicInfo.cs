using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostAndFound.Models
{
    public class UserBasicInfo
    {
        [Required]
        [Display(Name = "User fullname")]
        public string userFullName { get; set; }

        [Required]
        [Display(Name = "Employer")]
        [ForeignKey("clientId")]
        public virtual ClientBasicInfo clientBasicInfo { get; set; }
        
        [Required]
        [ForeignKey("roleId")]
        public virtual LostAndFoundRoles lostAndFoundRoles { get; set; }

        
    }
}