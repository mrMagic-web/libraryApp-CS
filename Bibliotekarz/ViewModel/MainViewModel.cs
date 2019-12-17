using Bibliotekarz.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace Bibliotekarz.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {

            GenerateFakeData();
        }

        private void GenerateFakeData()
        {
            BookList = new ObservableCollection<Book>
            {
                new Book
                {
                    Id = 1,
                    Author = "Maciej Drahusz",
                    Title = "Programowanie w C#",
                    PageCount = 234,
                    IsBorrowed = true,
                    Borrower = new Customer
                    {
                        Id = 1,
                        FirstName = "Adam",
                        LastName = "X"
                    }
                },
                new Book
                {
                    Id = 2,
                    Author = "John Sharp",
                    Title = "Visual C# krok po kroku",
                    PageCount = 234,
                    IsBorrowed = false
                }
            };
        }

        public ObservableCollection<Book> BookList { get; set; }

        public ICommand CloseCommand => new RelayCommand(Close);
        public ICommand ExportCommand => new RelayCommand(ExportData);

        private void Close()
        {
            Environment.Exit(0);
            
        }

        private void ExportData()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV documents (.csv)|*.csv|Text documents (.txt)|*.txt";

            bool? result = dlg.ShowDialog();

            if(result == true)
            {
                string filename = dlg.FileName;
                string[] data = PrepareDataFile();
                File.WriteAllLines(filename, data);
            }
        }

        private string[] PrepareDataFile()
        {
            string[] result = new string[BookList.Count + 1];
            result[0] = $"Id;Title;Author;PageCount;IsBorrowed;BorrowerId;BorrowerName;BorrowerSurname";
            const string separator = ";";

            for (int i = 0; i < BookList.Count; i++)
            {
                StringBuilder sb = new StringBuilder();
                Book book = BookList[i];

                sb.Append(book.Id).Append(separator);
                sb.Append(book.Title).Append(separator);
                sb.Append(book.Author).Append(separator);
                sb.Append(book.PageCount).Append(separator);
                sb.Append(book.IsBorrowed).Append(separator);
                if (book.IsBorrowed)
                {
                    sb.Append(book.Borrower.Id).Append(separator);
                    sb.Append(book.Borrower.FirstName).Append(separator);
                    sb.Append(book.Borrower.LastName).Append(separator);

                } else
                {
                    sb.Append(separator).Append(separator).Append(separator);
                }
                result[i + 1] = sb.ToString();
            }
            
            return result;
        }
    }
}