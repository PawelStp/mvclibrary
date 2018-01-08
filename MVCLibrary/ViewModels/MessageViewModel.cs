using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLibrary.ViewModels
{
    public class MessageViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Contnet { get; set; }
    }
}