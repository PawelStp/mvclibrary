using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLibrary.ViewModels
{
    public class LendViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Autor")]
        public string Author { get; set; }

        public string ISBN { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        public string Email { get; set; }

        [Display(Name = "Stan")]
        public string State { get; set; }

        [Display(Name = "Data wypożyczenia")]
        public DateTime DateBorrowed { get; set; }

        [Display(Name = "Data zwrotu")]
        public DateTime DateReturn { get; set; }

    }
}