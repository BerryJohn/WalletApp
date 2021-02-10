using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;
using WalletGlobal;

namespace WalletAPP
{
    public partial class Incoms : Page
    {
        private void countIncomes()
        {
            long totalIncome = 0;
            using var db = new Wallet();
            IQueryable<Incom> Incoms = db.Incoms.Where(el => el.UserId == GLOBALS.CurrentUserID);
            foreach (var el in Incoms)
                totalIncome += el.Income;

            totalIncomeLabel.Content = $"+{totalIncome}PLN";
        }
        private string findCategory(long id)
        {
            using var db = new Wallet();
            IQueryable<IncomeCategory> category = db.IncomeCategories.Where(el => el.Id == id);
            return category.First().Category;
        }
        private void updateIncomsList()
        {
            IncomeList.Items.Clear();
            using var db = new Wallet();
            IQueryable<Incom> Incoms = db.Incoms.Where(el => el.UserId == GLOBALS.CurrentUserID);
            foreach (var incom in Incoms)
                IncomeList.Items.Add($"+{incom.Income}PLN  Category: {findCategory(incom.CategoryId)}");
            countIncomes();
        }
        private void updateCategoryList()
        {
            incomeFormCategories.Items.Clear();
            using var db = new Wallet();
            IQueryable<IncomeCategory> categories = db.IncomeCategories;
            foreach (var category in categories)
                incomeFormCategories.Items.Add($"{category.Category}"); 
        }
        public Incoms()
        {
            InitializeComponent();
            updateIncomsList();
            updateCategoryList();
            countIncomes();
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
            newIncom.UserId = GLOBALS.CurrentUserID; 
            db.Incoms.Add(newIncom);
            int succes = db.SaveChanges();
            updateIncomsList();
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
