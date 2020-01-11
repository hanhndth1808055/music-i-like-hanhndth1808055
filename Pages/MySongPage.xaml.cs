using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using FinalProjectMusic.Models;
using FinalProjectMusic.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MySongPage : Page
    {
        public static ObservableCollection<Song> FullListSongs = new ObservableCollection<Song>();
        private SongService _service = new SongService();
        public static Song currentSongInMySongPage = null;
        public static int _currentSongIndex;

        public MySongPage()
        {
            this.InitializeComponent();
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void MySongPage_Loaded(object sender, RoutedEventArgs e)
        {
            FullListSongs = await _service.LoadMySongs();
            ControlGridView.ItemsSource = FullListSongs;
            // Debug.WriteLine(JsonConvert.SerializeObject(FullListSongs));
        }


        private void ControlGridView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            LayoutPage.ListName = "MySongPage";
            currentSongInMySongPage = e.ClickedItem as Song;
            LayoutPage.currentSong = currentSongInMySongPage;
            Debug.WriteLine("After clicked " + ControlGridView.SelectedIndex);
            _currentSongIndex = FullListSongs.IndexOf(currentSongInMySongPage);
            LayoutPage.PlayCurrentSong("MySongPage");
        }

        public void ChangeSelectedIndexInSongList()
        {
            Debug.WriteLine("Before " + ControlGridView.SelectedIndex);

            ControlGridView.SelectedIndex = _currentSongIndex;
            ControlGridView.SelectedItem = LayoutPage.currentSong;
            Debug.WriteLine("After " + ControlGridView.SelectedIndex);
            // Debug.WriteLine(FullListSongs.IndexOf(currentSongInMySongPage));
        }
    }
}
