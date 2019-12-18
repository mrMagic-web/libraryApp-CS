using Bibliotekarz.Model;
using Bibliotekarz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bibliotekarz.Views
{
    public partial class BookWindow : Window
    {
        public BookWindow()
        {
            InitializeComponent();
            ((BookViewModel)DataContext).Window = this;

        }

        public event Action<Book> BookCreated;

        internal void OnBookCreated(Book myBook)
        {
            BookCreated?.Invoke(myBook);
        }
    }
}
