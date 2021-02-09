﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;
using WalletGlobal;

namespace WalletAPP
{
    public partial class Outgoings : Page
    {
        private string findCategory(long id)
        {
            using var db = new Wallet();
            IQueryable<OutgoingsCategory> category = db.OutgoingsCategories.Where(el => el.Id == id);
            return category.First().Category;
        }
        private void updateOutgoingsList()
        {
            OutgoingsList.Items.Clear();
            using var db = new Wallet();
            IQueryable<Outgoing> outgoin = db.Outgoings.Where(el => el.UserId == GLOBALS.CurrentUserID);
            foreach (var outgo in outgoin)
                OutgoingsList.Items.Add($"-{outgo.Outcome}PLN  Category: {findCategory(outgo.CategoryId)}");
#warning Zmień literówke w gazie i tutaj ( OUTCOME -> outgoing)
        }
        private long findCategoryID(string category)
        {
            using var db = new Wallet();
            IQueryable<OutgoingsCategory> categories = db.OutgoingsCategories.Where(el => el.Category == category);
            return categories.First().Id;
        }
        private int addCategory(long amount, string category)
        {
            using var db = new Wallet();
            var newOutgoing = new Outgoing();
            newOutgoing.Outcome = amount;
#warning Zmień literówke w gazie i tutaj ( OUTCOME -> outgoing)
            newOutgoing.CategoryId = findCategoryID(category);
            newOutgoing.UserId = GLOBALS.CurrentUserID;
            db.Outgoings.Add(newOutgoing);
            int succes = db.SaveChanges();
            updateOutgoingsList();
            return succes;
        }

        private void updateCategoryList()
        {
            outgoingFormCategories.Items.Clear();
            using var db = new Wallet();
            IQueryable<OutgoingsCategory> categories = db.OutgoingsCategories;
            foreach (var category in categories)
                outgoingFormCategories.Items.Add($"{category.Category}");
        }
        public Outgoings()
        {
            InitializeComponent();
            updateOutgoingsList();
            updateCategoryList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string amount = outgoingFormValue.Text;
            string category = "";
            try
            {
                category = outgoingFormCategories.SelectedItem.ToString();
            }
            catch
            {
                return;
            }
            if (long.TryParse(amount, out long res) && !string.IsNullOrEmpty(category))
            {
                addCategory(res, category);
            }
        }
    }
}
