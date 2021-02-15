using System.Linq;
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
        private void countOutgoings()
        {
            long totalOutgoing = 0;
            using var db = new Wallet();
            IQueryable<Outgoing> outgoin = db.Outgoings.Where(el => el.UserId == GLOBALS.CurrentUserID);
            foreach (var el in outgoin)
                totalOutgoing += el.Outcome;

            totalOutgoingLabel.Content = $"-{totalOutgoing}PLN";
        }
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
                OutgoingsList.Items.Add($"{outgo.Id}: -{outgo.Outcome}PLN  Category: {findCategory(outgo.CategoryId)}");
#warning Zmień literówke w gazie i tutaj ( OUTCOME -> outgoing)
            countOutgoings();
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
            countOutgoings();
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
            countOutgoings();
        }

        private void addOutgoing_Click(object sender, RoutedEventArgs e)
        {
            if (outgoingFormCategories.SelectedItem is not null)
            {
                string amount = outgoingFormValue.Text;
                string category = outgoingFormCategories.SelectedItem.ToString();
                if (long.TryParse(amount, out long res) && !string.IsNullOrEmpty(category))
                    addCategory(res, category);
            }
        }

        private void removeOutgoing_Click(object sender, RoutedEventArgs e)
        {
            if (OutgoingsList.SelectedItem is not null)
            {
                using var db = new Wallet();
                long selected = long.Parse(OutgoingsList.SelectedItem.ToString().Split(':')[0]);
                IQueryable<Outgoing> outgoingToRemove = db.Outgoings.Where(el => el.Id == selected);
                db.Outgoings.RemoveRange(outgoingToRemove);
                int result = db.SaveChanges();
                updateOutgoingsList();
            }
        }
    }
}
