using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;
using WalletGlobal;
using System;

namespace WalletAPP
{
    /// <summary>
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class Categories : Page
    {
        private void updateIncomeCategory()
        {
            incomeCategoryList.Items.Clear();
            using var db = new Wallet();
            IQueryable<IncomeCategory> category = db.IncomeCategories;
            foreach (var el in category)
                incomeCategoryList.Items.Add($"{el.Category}");
        }
        private void updateOutgoingCategory()
        {
            outgoingCategoryList.Items.Clear();
            using var db = new Wallet();
            IQueryable<OutgoingsCategory> category = db.OutgoingsCategories;
            foreach (var el in category)
                outgoingCategoryList.Items.Add($"{el.Category}");
        }
        public Categories()
        {
            InitializeComponent();
            updateIncomeCategory();
            updateOutgoingCategory();
        }

        private void removeIncomeCategory_Click(object sender, RoutedEventArgs e)
        {
            if (incomeCategoryList.SelectedItem is not null)
            {
                using var db = new Wallet();
                string selected = incomeCategoryList.SelectedItem.ToString();
                IQueryable<IncomeCategory> categoryToRemove = db.IncomeCategories.Where(el => el.Category == selected);
                db.IncomeCategories.RemoveRange(categoryToRemove);
                int result = db.SaveChanges();
                updateIncomeCategory();
            }
        }
        private void removeOutgoingCategory_Click(object sender, RoutedEventArgs e)
        {
            if(outgoingCategoryList.SelectedItem is not null)
            {
                using var db = new Wallet();
                string selected = outgoingCategoryList.SelectedItem.ToString();
                IQueryable<OutgoingsCategory> categoryToRemove = db.OutgoingsCategories.Where(el => el.Category == selected);
                db.OutgoingsCategories.RemoveRange(categoryToRemove);
                int result = db.SaveChanges();
                updateOutgoingCategory();
            }
        }
        private void addIncomeCat(string cat)
        {
            try
            {
                if (!string.IsNullOrEmpty(cat))
                {
                    using var db = new Wallet();
                    var newCategory = new IncomeCategory();
                    newCategory.Category = cat;
                    db.IncomeCategories.Add(newCategory);
                    int succes = db.SaveChanges();
                    if (!string.IsNullOrEmpty(addCategoryError.Content.ToString()))
                        addCategoryError.Content = "";
                    updateIncomeCategory();
                }
            }
            catch(Exception ex)
            {
                if (ex is Microsoft.EntityFrameworkCore.DbUpdateException)
                    addCategoryError.Content = "That name is already used!";
            }
        }
        private void addOutgoingCat(string cat)
        {
            try
            {
                if (!string.IsNullOrEmpty(cat))
                {
                    using var db = new Wallet();
                    var newCategory = new OutgoingsCategory();
                    newCategory.Category = cat;
                    db.OutgoingsCategories.Add(newCategory);
                    int succes = db.SaveChanges();
                    if (!string.IsNullOrEmpty(addCategoryError.Content.ToString()))
                        addCategoryError.Content = "";
                    updateOutgoingCategory();
                }
            }
            catch (Exception ex)
            {
                if (ex is Microsoft.EntityFrameworkCore.DbUpdateException)
                    addCategoryError.Content = "That name is already used!";
            }

        }
        private void addCategory_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = newCategoryName.Text;
            if (incomeRadio.IsChecked == true)
                addIncomeCat(categoryName);
            else if (outgoingRadio.IsChecked == true)
                addOutgoingCat(categoryName);

        }
    }
}
