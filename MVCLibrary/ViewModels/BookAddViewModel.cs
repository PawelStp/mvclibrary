using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLibrary.ViewModels
{
    public class BookAddViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Tytuł")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Autor")]
        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Display(Name = "Kategoria")]
        [Required]
        public string SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}