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

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        public string Email { get; set; }
  
        [Display(Name = "Limit wypożyczeń")]
        public Nullable<int> BorrowLimit { get; set; }

        [Display(Name = "Limit oczekiwania")]
        public Nullable<int> WaitLimit { get; set; }

        [Display(Name = "Rola")]
        [Required]
        public string Role { get; set; }

        [Display(Name = "Role")]
        public IEnumerable<SelectListItem> Rols { get; set; }
    }
}