﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FinalProjectMusic.FinalExam;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FinalPracticalExam : Page
    {
        public FinalPracticalExam()
        {
            this.InitializeComponent();
        }

        private void AddContact_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddContactPage));
        }

        private void SearchContact_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SearchContactPage));
        }

        private void ListContact_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ListContactPage));
        }
    }
}
