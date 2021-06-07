﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalSiteMVC.UI.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = "*Message is required.")]
        [UIHint("MultilineText")]
        public string Message { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Meet Date")]
        public DateTime MeetDateTime { get; set; }
    }
}