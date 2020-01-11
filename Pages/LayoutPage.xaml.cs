using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class LayoutPage : Page
    {
        public static MediaPlayerElement MyPlayer = new MediaPlayerElement();
        private static LayoutPage _instance;
        public static Song currentSong;
        public static int _currentSongIndex;
        public static bool _autoReplaying = false;
        public static bool _shuffleMode = false;
        public static string ListName;
        public SongService _songService = new SongService();
        public bool _isPlaying = false;
        public static ObservableCollection<Song> Songs = new ObservableCollection<Song>();
        public static SongListPage _SongListPage = new SongListPage();
        public static MySongPage _MySongPage = new MySongPage();

        public static LayoutPage CreateInstance()
        {
            if (_instance == null)
            {
                _instance = new LayoutPage();
            }

            return _instance;
        }
        public LayoutPage()
        {
            this.InitializeComponent();
            _instance = this;
            // _LayoutPage = this;
        }

        public static void PlayCurrentSong(string pageName)
        {
            if (pageName == "SongListPage")
            {
                Songs = SongListPage.FullListSongs;
            }

            if (pageName == "MySongPage")
            {
                Songs = MySongPage.FullListSongs;
            }

            MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            CreateInstance().PlayingStatus();
            _currentSongIndex = Songs.IndexOf(currentSong);
        }

        public void PlayingStatus()
        {
            MyPlayer.MediaPlayer.Play();
            this.PlayButton.Icon = new SymbolIcon(Symbol.Pause);
            _isPlaying = true;
            this.StatusText.Text = "Now Playing: " + currentSong.name;
            Debug.WriteLine(StatusText.Text);
        }

        public void PausedStatus()
        {
            MyPlayer.MediaPlayer.Pause();
            PlayButton.Icon = new SymbolIcon(Symbol.Play);
            _isPlaying = false;
            StatusText.Text = "Paused. Song: " + currentSong.name;
        }

        public async void PlayButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (LogInPage._token == null)
            {
                return;
            }

            if (LogInPage._token != null & currentSong == null)
            {
                SongListPage.FullListSongs = await _songService.LoadSongs();
                Songs = SongListPage.FullListSongs;
                ListName = "SongListPage";
                currentSong = Songs.FirstOrDefault();
                PlayCurrentSong(null);
                PlayingStatus();
                _currentSongIndex = Songs.IndexOf(currentSong);
                // Debug.WriteLine(_currentSongIndex);
                return;
            }

            if (currentSong != null && _isPlaying)
            {
                PausedStatus();
            }
            else
            {
                PlayingStatus();
            }
        }

        public void Next_OnClick(object sender, RoutedEventArgs e)
        {
            _currentSongIndex = Songs.IndexOf(currentSong);

            // Debug.WriteLine(_currentSongIndex);

            _currentSongIndex++;

            if (_currentSongIndex >= Songs.Count)
            {
                _currentSongIndex = 0;
            }

            currentSong = Songs[_currentSongIndex] as Song;
            Debug.WriteLine("Next " + currentSong);
            if (ListName == "SongListPage")
            {
                SongListPage.currentSongInSongListPage = currentSong;
                SongListPage._currentSongIndex = _currentSongIndex;
                _SongListPage.ChangeSelectedIndexInSongList();
            }
            if (ListName == "MySongPage")
            {
                MySongPage.currentSongInMySongPage = currentSong;
                MySongPage._currentSongIndex = _currentSongIndex;
                _MySongPage.ChangeSelectedIndexInSongList();
            }

            MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            PlayingStatus();
        }

        public void Previous_OnClick(object sender, RoutedEventArgs e)
        {
            _currentSongIndex = Songs.IndexOf(currentSong);

            // Debug.WriteLine(_currentSongIndex);

            _currentSongIndex--;

            if (_currentSongIndex < 0)
            {
                _currentSongIndex = Songs.IndexOf(Songs.Last());
            }

            currentSong = Songs[_currentSongIndex] as Song;

            if (ListName == "SongListPage")
            {
                SongListPage.currentSongInSongListPage = currentSong;
                _SongListPage.ChangeSelectedIndexInSongList();
            }

            if (ListName == "MySongPage")
            {
                MySongPage.currentSongInMySongPage = currentSong;
                _MySongPage.ChangeSelectedIndexInSongList();
            }

            MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            PlayingStatus();
        }

        public void LogInLink_OnClick(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(LogInPage));
        }

        public void RegisterLink_OnClick(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(RegisterPage));
        }

        public void SongListLink_OnClick(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(SongListPage));
        }

        public void AddNewSongLink_OnClick(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(AddNewSongPage));
        }

        public void MySongListLink_OnClick(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(MySongPage));
        }
        public void MyProfileLink_OnClick(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(MyProfilePage));
        }

        public async void Layout_Loaded(object sender, RoutedEventArgs e)
        {
            await LogInPage.GetToken();
        }

        public void ReplayAll_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_autoReplaying)
            {
                MyPlayer.MediaPlayer.IsLoopingEnabled = true;
                _autoReplaying = true;
            }
            else
            {
                MyPlayer.MediaPlayer.IsLoopingEnabled = false;
                _autoReplaying = false;
            }
        }

        private async void Shuffle_Clicked(object sender, RoutedEventArgs e)
        {
            if (ListName == "SongListPage")
            {
                if (_shuffleMode == false)
                {
                    Debug.WriteLine("index before before " + _currentSongIndex);
                    Shuffle<Song>(Songs);
                    Debug.WriteLine("Shuffled Songs" + JsonConvert.SerializeObject(Songs));

                    Debug.WriteLine("Shuffled Songs" + JsonConvert.SerializeObject(SongListPage.FullListSongs));
                    _shuffleMode = true;

                }
                else
                {
                    Debug.WriteLine("Song index before: " + _currentSongIndex);
                    Debug.WriteLine("Shuffled " + JsonConvert.SerializeObject(Songs));
                    SongListPage.FullListSongs = await _songService.LoadSongs();
                    Songs = SongListPage.FullListSongs;
                    Debug.WriteLine("Old " + JsonConvert.SerializeObject(SongListPage.FullListSongs));
                    _shuffleMode = false;
                    var song = Songs.First(x => x.id == currentSong.id);
                    _currentSongIndex = Songs.IndexOf(song);
                    currentSong = song;
                    Debug.WriteLine("Song index after: " + _currentSongIndex);
                }
            }
            if (ListName == "MySongPage")
            {
                if (_shuffleMode == false)
                {
                    Debug.WriteLine("index before before " + _currentSongIndex);
                    Shuffle<Song>(Songs);
                    Debug.WriteLine("Shuffled Songs" + JsonConvert.SerializeObject(Songs));

                    Debug.WriteLine("Shuffled Songs" + JsonConvert.SerializeObject(MySongPage.FullListSongs));
                    _shuffleMode = true;

                }
                else
                {
                    Debug.WriteLine("Song index before: " + _currentSongIndex);
                    Debug.WriteLine("Shuffled " + JsonConvert.SerializeObject(Songs));
                    MySongPage.FullListSongs = await _songService.LoadMySongs();
                    Songs = MySongPage.FullListSongs;
                    Debug.WriteLine("Old " + JsonConvert.SerializeObject(MySongPage.FullListSongs));
                    _shuffleMode = false;
                    var song = Songs.First(x => x.id == currentSong.id);
                    _currentSongIndex = Songs.IndexOf(song);
                    currentSong = song;
                    Debug.WriteLine("Song index after: " + _currentSongIndex);
                }
            }
        }
        public static void Shuffle<T>(ObservableCollection<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void Logout_Clicked(object sender, RoutedEventArgs e)
        {
            LogInPage._token = null;
            FileHandleService.DeleteFile("token.txt");
            this.MyFrame.Navigate(typeof(LogInPage));
        }
    }
}
