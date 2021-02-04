using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;

namespace WalletAPP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            using var db = new Wallet();
            IQueryable<User> users = db.Users;
            lista.Items.Clear();
            foreach (var user in users)
                lista.Items.Add($"{user.Nickname}-{user.Money}PLN");
        }

        private void lista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string curItem = lista.SelectedItem.ToString();
            tekst.Content = curItem;
        }
    }
}
