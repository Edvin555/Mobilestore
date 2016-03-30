using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public List<CartLine> Lines { get { return lineCollection; } }
        public void AddItem(MobilePhone book, int quantity)
        {
            CartLine line = lineCollection.Where(b => b.Book.BookId == book.BookId).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Book = book, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(MobilePhone book)
        {
            lineCollection.RemoveAll(l => l.Book.BookId == book.BookId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(l => l.Book.Price * l.Quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
    }

    public class CartLine
    {
        public MobilePhone Book { get; set; }
        public int Quantity { get; set; }

    }
}
