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
using Windows.UI.Xaml.Media.Imaging;
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
        private static LayoutPage _instance;
        public Song currentSong;
        public int _currentSongIndex;
        public bool _autoReplaying = false;
        public bool _shuffleMode = false;
        public string ListName;
        public SongService _songService = new SongService();
        public bool _isPlaying = false;
        // public ObservableCollection<Song> Songs = new ObservableCollection<Song>();
        public static SongListPage _SongListPage;
        public static MySongPage _MySongPage;

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
            _SongListPage = SongListPage.CreateInstance();
            _MySongPage = MySongPage.CreateInstance();
            // _LayoutPage = this;
        }

        public void PlayCurrentSong(string pageName)
        {
            if (pageName == "SongListPage" || ListName == "SongListPage")
            {
                MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
                PlayingStatus();
                // _currentSongIndex = _SongListPage.FullListSongs.IndexOf(currentSong);
            }

            if (pageName == "MySongPage" || ListName == "MySongPage")
            {
                Debug.WriteLine(MySongPage.CreateInstance().currentSong.name);
                MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
                PlayingStatus();
                // _currentSongIndex = _MySongPage.FullListSongs.IndexOf(currentSong);
            }

        }

        public void PlayingStatus()
        {
            if (ListName == "MySongPage")
            {
                this.StatusText.Text = "Now Playing: " + currentSong.name;
            }
            if (ListName == "SongListPage")
            {
                this.StatusText.Text = "Now Playing: " + currentSong.name;
            }

            MyPlayer.MediaPlayer.Play();
            this.PlayButton.Icon = new SymbolIcon(Symbol.Pause);
            _isPlaying = true;
            SongThumbnail.Source = new BitmapImage(new Uri(currentSong.thumbnail, UriKind.Absolute));
            Debug.WriteLine(StatusText.Text);
        }

        public void PausedStatus()
        {
            if (ListName == "MySongPage")
            {
                StatusText.Text = "Paused. Song: " + currentSong.name;
            }
            if (ListName == "SongListPage")
            {

                StatusText.Text = "Paused. Song: " + currentSong.name;
            }
            MyPlayer.MediaPlayer.Pause();
            PlayButton.Icon = new SymbolIcon(Symbol.Play);
            _isPlaying = false;
        }

        public async void PlayButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (LogInPage._token == null)
            {
                return;
            }

            if (LogInPage._token != null & currentSong == null)
            {
                _SongListPage.FullListSongs = await _songService.LoadSongs();
                ListName = "SongListPage";
                currentSong = _SongListPage.FullListSongs.FirstOrDefault();
                PlayCurrentSong("SongListPage");
                PlayingStatus();
                _currentSongIndex = _SongListPage.FullListSongs.IndexOf(currentSong);
                // Debug.WriteLine(_currentSongIndex);
                return;
            }

            if (
                (currentSong != null && ListName == "SongListPage"
                    || currentSong != null && ListName == "MySongPage")
                    && _isPlaying)
            {
                PausedStatus();
            }
            else
            {
                PlayingStatus();
            }
        }

        public async void Next_OnClick(object sender, RoutedEventArgs e)
        {
            if (_shuffleMode == false)
            {
                if (ListName == "SongListPage")
                {
                    currentSong = await _SongListPage.NextSongNormal(_currentSongIndex);
                }

                if (ListName == "MySongPage")
                {
                    currentSong = await _MySongPage.NextSongNormal(_currentSongIndex);
                }
            }
            else
            {
                if (ListName == "SongListPage")
                {
                    currentSong = await _SongListPage.ChangeSongShuffle();
                }
                if (ListName == "MySongPage")
                {
                    currentSong = await _MySongPage.ChangeSongShuffle();
                }
            }

            MyPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            PlayingStatus();
        }

        public async void Previous_OnClick(object sender, RoutedEventArgs e)
        {
            if (_shuffleMode == false)
            {
                if (ListName == "SongListPage")
                {
                    currentSong = await _SongListPage.PreviousSongNormal(_currentSongIndex);
                    
                }
                if (ListName == "MySongPage")
                {
                    currentSong = await _MySongPage.PreviousSongNormal(_currentSongIndex);
                }
            }
            else
            {
                if (ListName == "SongListPage")
                {
                    currentSong = await _SongListPage.ChangeSongShuffle();
                }
                if (ListName == "MySongPage")
                {
                    currentSong = await _MySongPage.ChangeSongShuffle();
                }
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

        private void Shuffle_Clicked(object sender, RoutedEventArgs e)
        {
            if (_shuffleMode == false)
            {
                _shuffleMode = true;

            }
            else
            {
                _shuffleMode = false;
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
