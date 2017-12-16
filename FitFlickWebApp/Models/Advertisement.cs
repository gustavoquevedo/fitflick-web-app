using System;
using System.ComponentModel.DataAnnotations;

namespace FitFlickWebApp.Models
{
    public class Advertisement
    {
        public int ID { get; set; }

        [Display(Name = "Payment Option")]
        [Required(ErrorMessage = "Please select your Payment Option")]
        public PaymentOption PaymentOption { get; set; }

        [Display(Name = "Business Name or Ads Title *")]
        [Required(ErrorMessage = "Please enter your Business Name or Ads Title")]
        public string BusinessName { get; set; }

        [Display(Name = "Email address *")]
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        
        [Display(Name = "Link to your Website *")]
        [DataType(DataType.Url)]
        public string WebsiteUrl { get; set; }

        [Display(Name = "Advertisement Type *")]
        [Required(ErrorMessage = "Please select the Advertisement Type")]
        public AdvertisementType? AdvertisementType { get; set; }

        [Display(Name = "Twitter Link *")]
        [DataType(DataType.Url)]
        public string TwitterLink { get; set; }

        [Display(Name = "Instagram user name *")]
        [Required(ErrorMessage = "Please enter your Instagram user name")]
        public string InstagramUserName { get; set; }

        [Display(Name = "Image file name *")]
        public string ImageFileName { get; set; }
        public byte[] ImageData { get; set; }

        [Display(Name = "Advertisement Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Created at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Modified at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime ModifyDate { get; set; }
        
        public bool Approved { get; set; }
    }
}