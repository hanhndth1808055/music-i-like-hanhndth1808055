using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserDataTasks;
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
        private static MySongPage _instance;
        public ObservableCollection<Song> FullListSongs;
        private SongService _service = new SongService();
        public Song currentSong;
        public int _currentSongIndex;
        public static LayoutPage _LayoutPage;
        
        public MySongPage()
        {
            this.InitializeComponent();
            _instance = this;
            _LayoutPage = LayoutPage.CreateInstance();
        }
        public static MySongPage CreateInstance()
        {
            if (_instance == null)
            {
                _instance = new MySongPage();
            }

            return _instance;
        }

        private async void MySongPage_Loaded(object sender, RoutedEventArgs e)
        {
            FullListSongs = await _service.LoadMySongs();
            ControlGridView.ItemsSource = FullListSongs;
            // Debug.WriteLine(JsonConvert.SerializeObject(FullListSongs));
        }

        public async Task<ObservableCollection<Song>> LoadMySongsHere()
        {
            FullListSongs = await _service.LoadMySongs();
            return FullListSongs;
        }
        private void ControlGridView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            _LayoutPage.ListName = "MySongPage";
            currentSong = e.ClickedItem as Song;
            _LayoutPage.currentSong = currentSong;
            Debug.WriteLine("Current song " + currentSong.name);
            Debug.WriteLine("After clicked " + ControlGridView.SelectedIndex);
            _currentSongIndex = FullListSongs.IndexOf(currentSong);
            _LayoutPage._currentSongIndex = _currentSongIndex;
            Debug.WriteLine("_currentSongIndex = FullListSongs.IndexOf(currentSong) " + FullListSongs.IndexOf(currentSong));
            _LayoutPage.PlayCurrentSong("MySongPage");
        }

        // public void ChangeSelectedIndexInSongList()
        // {
        //     Debug.WriteLine("Before " + ControlGridView.SelectedIndex);
        //
        //     ControlGridView.SelectedIndex = _currentSongIndex;
        //     Debug.WriteLine("After " + ControlGridView.SelectedIndex);
        //     // Debug.WriteLine(FullListSongs.IndexOf(currentSongInMySongPage));
        // }

        public async Task<Song> NextSongNormal(int index)
        {
            await LoadMySongsHere();
            // Debug.WriteLine(FullListSongs.Count);
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

        public async Task<Song> ChangeSongShuffle()
        {
            await LoadMySongsHere();

            var index = RandomNumber(0, FullListSongs.Count);
            _LayoutPage._currentSongIndex = index;

            return FullListSongs[index];
        }

        // Generate a random number between two numbers  
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public async Task<Song> PreviousSongNormal(int index)
        {
            await LoadMySongsHere();
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

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (LogInPage._token == null)
            {
                this.Frame.Navigate(typeof(LogInPage));
            }
        }
    }
}
