using Bibliotekarz.Model;
using Bibliotekarz.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bibliotekarz.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        internal BookWindow Window;

        public BookViewModel()
        {
            MyBook = new Book()
            {
                Borrower = new Customer()
            };
        }
        public Book MyBook { get; set; }
        public ICommand SaveCommand => new RelayCommand(Save);
        private void Save()
        {
            if (!MyBook.IsBorrowed)
            {
                MyBook.Borrower = null;
            }
            Window.OnBookCreated(MyBook);
            Window.Close();
        }
    }
}
