using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLibrary.ViewModels
{
    public class CategoryAddViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name="Z rodziny")]
        public string SelectedCategoryId { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}