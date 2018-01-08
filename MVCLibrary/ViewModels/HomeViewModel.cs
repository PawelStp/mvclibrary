using MVCLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLibrary.ViewModels
{
    public class HomeViewModel
    {
        public List<BookViewModel> bookViewModel { get; set; }

        public List<message> messages { get; set; }




    }
}