using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Streaming.Adaptive;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FinalProjectMusic.Models;
using FinalProjectMusic.Services;
using Newtonsoft.Json;
using FinalProjectMusic.Pages;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongListPage : Page
    {
        public static ObservableCollection<Song> FullListSongs = new ObservableCollection<Song>();
        private SongService _service = new SongService();
        public static Song currentSongInSongListPage = null;
        public static int _currentSongIndex;

        public SongListPage()
        {
            this.InitializeComponent();
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void SongListPage_Loaded(object sender, RoutedEventArgs e)
        {
            FullListSongs = await _service.LoadSongs();
            ControlGridView.ItemsSource = FullListSongs;
            // Debug.WriteLine(JsonConvert.SerializeObject(FullListSongs));
        }


        private void ControlGridView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            LayoutPage.ListName = "SongListPage";
            currentSongInSongListPage = e.ClickedItem as Song;
            LayoutPage.currentSong = currentSongInSongListPage;
            Debug.WriteLine("After clicked " + ControlGridView.SelectedIndex);
            _currentSongIndex = FullListSongs.IndexOf(currentSongInSongListPage);
            LayoutPage.PlayCurrentSong("SongListPage");
        }

        public void ChangeSelectedIndexInSongList()
        {
            Debug.WriteLine("Before " + ControlGridView.SelectedIndex);

            ControlGridView.SelectedIndex = _currentSongIndex;
            ControlGridView.SelectedItem = LayoutPage.currentSong;
            Debug.WriteLine("After " + ControlGridView.SelectedIndex);
            // Debug.WriteLine(FullListSongs.IndexOf(currentSongInSongListPage));
        }
    }
}
