using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLibrary.ViewModels
{
    public class LendViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string State { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime DateReturn { get; set; }

    }
}