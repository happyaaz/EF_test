using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace LostAndFound.Models
{
    public class ApplicationUser
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get;set;
        }

        public string UserName
        {
            get;set;
        }

        public string Password
        {
            get; set;
        }

        public string UserFullName
        {
            get; set;
        }

        public string UserEmployer
        {
            get; set;
        }

        public string UserRole
        {
            get; set;
        }
    }
}