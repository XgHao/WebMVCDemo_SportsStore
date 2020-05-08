﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter a name")]
        [Display(Name = "收货人")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        [Display(Name = "省")]
        public string Line1 { get; set; }

        [Display(Name = "市")]
        public string Line2 { get; set; }

        [Display(Name = "区")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        [Display(Name = "城市")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        [Display(Name = "镇")]
        public string State { get; set; }

        [Display(Name = "邮编")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        [Display(Name = "村")]
        public string Country { get; set; }

        [Display(Name = "是否赠送")]
        public bool GiftWrap { get; set; }
    }
}
