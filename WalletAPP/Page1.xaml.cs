
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;

namespace WalletAPP
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private void UpdateUserList()
        {
            IncomeList.Items.Clear();
            using var db = new Wallet();
            IQueryable<Incom> Incoms = db.Incoms;
            foreach (var incom in Incoms)
                IncomeList.Items.Add($"+{incom.Income}PLN  Category:{incom.CategoryId}"); //ID IS TEMPORARY
        }
        public Page1()
        {
            InitializeComponent();
            UpdateUserList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
