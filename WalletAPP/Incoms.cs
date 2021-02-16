using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;
using WalletGlobal;

namespace WalletAPP
{   
    /// <summary>
    /// Logical interpretation for Incomes page
    /// </summary>
    public partial class Incoms : Page
    {
        public Incoms()
        {
            InitializeComponent();
            updateIncomsList();
            updateCategoryList();
            countIncomes();
        }

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
                IncomeList.Items.Add($"{incom.Id}: +{incom.Income}PLN  Category: {findCategory(incom.CategoryId)}");
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
        private void addIncome_Click(object sender, RoutedEventArgs e)
        {
            if(incomeFormCategories.SelectedItem is not null)
            {
                string amount = incomeFormValue.Text;
                string category = incomeFormCategories.SelectedItem.ToString();
                if (long.TryParse(amount, out long res) && !string.IsNullOrEmpty(category))
                    addCategory(res, category);
                incomeFormValue.Text = "";
            }
        }
        private void removeIncome_Click(object sender, RoutedEventArgs e)
        {
            if (IncomeList.SelectedItem is not null)
            {
                using var db = new Wallet();
                long selected = long.Parse(IncomeList.SelectedItem.ToString().Split(':')[0]);
                IQueryable<Incom> incomeToRemove = db.Incoms.Where(el => el.Id == selected);
                db.Incoms.RemoveRange(incomeToRemove);
                int result = db.SaveChanges();
                updateIncomsList();
            }

        }
    }
}
