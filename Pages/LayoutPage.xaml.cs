using System;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LayoutPage : Page
    {
        public LayoutPage()
        {
            this.InitializeComponent();
        }

        private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void LogInLink_OnClick(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(LogInPage));
        }

        private void RegisterLink_OnClick(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(RegisterPage));
        }

        private void SongListLink_OnClick(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(SongListPage));
        }

        private void AddNewSongLink_OnClick(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(AddNewSongPage));
        }
    }
}
