using MVCLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLibrary.MyCart
{
    public class Cart
    {
        public List<Books> books { get; set; }

        public Cart()
        {
            books = new List<Books>();
        }

        public bool addNewBook(Books b)
        {
            books.Add(b);

            return true;
        }

    }
}