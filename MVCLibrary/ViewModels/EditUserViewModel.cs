using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLibrary.ViewModels
{
    public class EditUserViewModel
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
  
        public Nullable<int> BorrowLimit { get; set; }
        public Nullable<int> WaitLimit { get; set; }

        [Display(Name = "Rola")]
        [Required]
        public string Role { get; set; }

        public IEnumerable<SelectListItem> Rols { get; set; }
    }
}