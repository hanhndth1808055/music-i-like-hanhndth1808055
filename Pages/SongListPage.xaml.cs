using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        private static SongListPage _instance;
        public ObservableCollection<Song> FullListSongs;
        private SongService _service = new SongService();
        public Song currentSong;
        public int _currentSongIndex;
        public LayoutPage _LayoutPage;
        
        public SongListPage()
        {
            this.InitializeComponent();
            _instance = this;
            _LayoutPage = LayoutPage.CreateInstance();
        }

        public static SongListPage CreateInstance()
        {
            if (_instance == null)
            {
                _instance = new SongListPage();

            }
            
            return _instance;
        }

        private async void SongListPage_Loaded(object sender, RoutedEventArgs e)
        {
            FullListSongs = await _service.LoadSongs();
            ControlGridView.ItemsSource = FullListSongs;
            // Debug.WriteLine(JsonConvert.SerializeObject(FullListSongs));
        }

        public async Task<ObservableCollection<Song>> LoadSongsHere()
        {
            FullListSongs = await _service.LoadSongs();
            return FullListSongs;
        }

        private void ControlGridView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            _LayoutPage.ListName = "SongListPage";
            currentSong = e.ClickedItem as Song;
            _LayoutPage.currentSong = currentSong;
            Debug.WriteLine("Current song index" + FullListSongs.IndexOf(currentSong));
            Debug.WriteLine("After clicked " + ControlGridView.SelectedIndex);
            _currentSongIndex = FullListSongs.IndexOf(currentSong);
            _LayoutPage._currentSongIndex = _currentSongIndex;
            Debug.WriteLine("_currentSongIndex = FullListSongs.IndexOf(currentSong) " + FullListSongs.IndexOf(currentSong));
            _LayoutPage.PlayCurrentSong("SongListPage");
        }

        // public void ChangeSelectedIndexInSongList()
        // {
        //     Debug.WriteLine("Before " + ControlGridView.SelectedIndex);
        //
        //     ControlGridView.SelectedIndex = _currentSongIndex;
        //     Debug.WriteLine("After " + ControlGridView.SelectedIndex);
        //     // Debug.WriteLine(FullListSongs.IndexOf(currentSongInSongListPage));
        // }
        public async Task<Song> NextSongNormal(int index)
        {
            await LoadSongsHere();
            // Debug.WriteLine(FullListSongs.Count);
            // Debug.WriteLine("Selected index: " + ControlGridView.SelectedIndex);
            if (index >= FullListSongs.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            // Debug.WriteLine("index next song " + index);
            _LayoutPage._currentSongIndex = index;

            return FullListSongs[index];
        }

        public async Task<Song> PreviousSongNormal(int index)
        {
            await LoadSongsHere();
            // Debug.WriteLine(FullListSongs.Count);
            // Debug.WriteLine("Selected index: " + ControlGridView.SelectedIndex);
            if (index <= 0)
            {
                index = FullListSongs.Count - 1;
            }
            else
            {
                index--;
            }
            // Debug.WriteLine("index next song " + index);
            _LayoutPage._currentSongIndex = index;

            return FullListSongs[index];
        }

        public async Task<Song> ChangeSongShuffle()
        {
            await LoadSongsHere();

            var index = RandomNumber(0, FullListSongs.Count - 1);
            _LayoutPage._currentSongIndex = index;

            return FullListSongs[index];
        }

        // Generate a random number between two numbers  
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min - 1, max + 1);
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (LogInPage._token == null)
            {
                this.Frame.Navigate(typeof(LogInPage));
            }
        }
    }
}
