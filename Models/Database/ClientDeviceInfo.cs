using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LostAndFound.Models.Database
{
    public class ClientDeviceInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name = "Client")]
        public string client { get; set; }

        [Display(Name = "Category")]
        public string deviceCategory { get; set; }

        [Display(Name = "Subcategory")]
        public string deviceSubcategory { get; set; }

        [Display(Name = "Name")]
        public string deviceName { get; set; }

        [Display(Name = "NUinta #")]
        public string deviceNUINTASerialNumber { get; set; }

        [Display(Name = "Carrier")]
        public string deviceCarrier { get; set; }

        [Display(Name = "Condition")]
        public string deviceCondition { get; set; }

        [Display(Name = "Bin")]
        public string deviceBin { get; set; }

        [Display(Name = "Used by")]
        public string deviceUsedBy { get; set; }

        [Display(Name = "Value")]
        public string deviceValue { get; set; }

        [Display(Name = "Sold for")]
        public string deviceSoldFor { get; set; }

        [Display(Name = "Repair cost")]
        public string deviceRepairCost { get; set; }

        [Display(Name = "Sales fees")]
        public string deviceSalesFees { get; set; }

        [Display(Name = "Commision to agent")]
        public string deviceCommisionToAgent { get; set; }

        [Display(Name = "NUinta profit")]
        public string deviceNUINTAProfit { get; set; }

        [Display(Name = "Added by user")]
        public int deviceAddedByUser { get; set; }

        [Display(Name = "Registration date")]
        public string deviceRegistrationDate { get; set; }

        [Display(Name = "Registration time")]
        public string deviceRegistrationTime { get; set; }

        [Display(Name = "Other comments")]
        public string deviceOtherComments { get; set; }
    }
}