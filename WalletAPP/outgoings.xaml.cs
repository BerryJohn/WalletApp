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
        public Outgoings()
        {
            InitializeComponent();
            updateOutgoingsList();
        }
    }
}
