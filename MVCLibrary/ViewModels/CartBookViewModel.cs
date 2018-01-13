using MVCLibrary.MyCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLibrary.ViewModels
{
    public class CartBookViewModel
    {
        public Cart cart { get; set; }
        public List<BookViewModel> BookViewModels { get; set; }
        public string SerchWord { get; set; }
        public CartBookViewModel()
        {
            BookViewModels = new List<BookViewModel>();
        }
    }
}