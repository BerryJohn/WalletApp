
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
        private string findCategory(long id)
        {
            using var db = new Wallet();
            IQueryable<IncomeCategory> category = db.IncomeCategories.Where(el => el.Id == id);
            return category.First().Category;
        }
        private void UpdateUserList()
        {
            IncomeList.Items.Clear();
            using var db = new Wallet();
            IQueryable<Incom> Incoms = db.Incoms;
            foreach (var incom in Incoms)
                IncomeList.Items.Add($"+{incom.Income}PLN  Category: {findCategory(incom.CategoryId)}"); 
        }
        private void UpdateCategoryList()
        {
            incomeFormCategories.Items.Clear();
            using var db = new Wallet();
            IQueryable<IncomeCategory> categories = db.IncomeCategories;
            foreach (var category in categories)
                incomeFormCategories.Items.Add($"{category.Category}"); 
        }
        public Page1()
        {
            InitializeComponent();
            UpdateUserList();
            UpdateCategoryList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void incomeFormCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private long findCategoryID(string category)
        {
            using var db = new Wallet();
            IQueryable<IncomeCategory> categories = db.IncomeCategories.Where(el => el.Category == category);
            return categories.First().Id;
        }
        private int addCategory(long amount, string category)
        {
            using var db = new Wallet();
            var newIncom = new Incom();
            newIncom.Income = amount;
            newIncom.CategoryId = findCategoryID(category);
            newIncom.UserId = 1; 
#warning TODO user selection now working only for first user
            db.Incoms.Add(newIncom);
            int succes = db.SaveChanges();
            UpdateUserList();
            return succes;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string amount = incomeFormValue.Text;
            string category = "";
            try 
            {
                category = incomeFormCategories.SelectedItem.ToString();
            }
            catch
            {
                return;
            }
            if (long.TryParse(amount,out long res) && !string.IsNullOrEmpty(category))
            {
                addCategory(res, category);
            }
        }
    }
}
