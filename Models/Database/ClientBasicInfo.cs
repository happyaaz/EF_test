using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostAndFound.Models
{
    // AKA Employer for users
    public class ClientBasicInfo
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int clientId { get; set; }

        [Display(Name = "Client name")]
        public string clientName { get; set; }

        [Display(Name = "Contact person")]
        public string clientContactPerson { get; set; }

        [Display(Name = "Contact person's number")]
        public string clientContactNumber { get; set; }

        [Display(Name = "Contact number")]
        public string clientContactEmail { get; set; }

        [Display(Name = "Commision in %")]
        public string clientCommisionInPercent { get; set; }

        [Display(Name = "Per item base fee in USD")]
        public string clientPerItemBaseFee { get; set; }

        [Display(Name = "Sales fee in USD or %")]
        public string clientSalesFee { get; set; }

        [Display(Name = "Shipping fee in USD")]
        public string clientShippingFee { get; set; }
    }
}