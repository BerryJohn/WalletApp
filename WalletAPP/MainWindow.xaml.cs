using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;
using WalletGlobal;

namespace WalletAPP
{
    public partial class MainWindow : Window
    {
        private Incoms _incomsPage = new Incoms();
        private UserChoice _userChoice = new UserChoice();
        private Outgoings _outgoings = new Outgoings();
        public MainWindow()
        {
            InitializeComponent();
            PageNavigation.Navigate(_userChoice);
        }
        private void Button_Incomes(object sender, RoutedEventArgs e)
        {
            _incomsPage = new Incoms();
            PageNavigation.Navigate(_incomsPage);
        }

        private void Button_Outgoings(object sender, RoutedEventArgs e)
        {
            _outgoings = new Outgoings();
            PageNavigation.Navigate(_outgoings);
        }

        private void Button_UserChoice(object sender, RoutedEventArgs e)
        {
            PageNavigation.Navigate(_userChoice);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //PageNavigation.Navigate(_incomsPage);  TBA
        }

    }
}
