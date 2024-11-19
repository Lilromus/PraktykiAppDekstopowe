using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Threading;

namespace VCLApp
{
    public partial class MainWindow : Window
    {
        private bool isFullScreen = false;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateMediaSlider;

         
            MusicName.Visibility = Visibility.Collapsed;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            string filter;
            if (RadioMusic.IsChecked == true)
            {
                filter = "Muzyka (*.mp3)|*.mp3";
            }
            else
            {
                filter = "Wideo (*.mp4;*.gif)|*.mp4;*.gif";
            }

            var openFileDialog = new OpenFileDialog
            {
                Filter = filter,
                Title = "Wybierz plik multimedialny"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (RadioMusic.IsChecked == true)
                {
                    MusicName.Visibility = Visibility.Visible;
                    MediaViewer.Visibility = Visibility.Collapsed;

                    
                    MediaViewer.Source = new Uri(openFileDialog.FileName);
                    MusicName.Text = $"Odtwarza się: {System.IO.Path.GetFileName(openFileDialog.FileName)}";
                }
                else
                {
                    MusicName.Visibility = Visibility.Collapsed;
                    MediaViewer.Visibility = Visibility.Visible;

                   
                    MediaViewer.Source = new Uri(openFileDialog.FileName);
                }

                PlayMedia();
            }
        }

        private void PlayMedia()
        {
            MediaViewer.Play();
            timer.Start();
        }

        private void PlayMedia_Click(object sender, RoutedEventArgs e)
        {
            PlayMedia();
        }

        private void PauseMedia_Click(object sender, RoutedEventArgs e)
        {
            MediaViewer.Pause();
        }

        private void StopMedia_Click(object sender, RoutedEventArgs e)
        {
            MediaViewer.Stop();
            timer.Stop();
            MediaSlider.Value = 0;
            UpdateCurrentTimeDisplay(TimeSpan.Zero, MediaViewer.NaturalDuration.TimeSpan);
        }

        private void Rewind5Seconds_Click(object sender, RoutedEventArgs e)
        {
            if (MediaViewer.NaturalDuration.HasTimeSpan)
            {
                var newPosition = MediaViewer.Position - TimeSpan.FromSeconds(5);

                if (newPosition < TimeSpan.Zero)
                {
                    MediaViewer.Position = TimeSpan.Zero;
                }
                else
                {
                    MediaViewer.Position = newPosition;
                }
            }
        }

        private void FastForward5Seconds_Click(object sender, RoutedEventArgs e)
        {
            if (MediaViewer.NaturalDuration.HasTimeSpan)
            {
                var newPosition = MediaViewer.Position + TimeSpan.FromSeconds(5);

                if (newPosition > MediaViewer.NaturalDuration.TimeSpan)
                {
                    MediaViewer.Position = MediaViewer.NaturalDuration.TimeSpan;
                }
                else
                {
                    MediaViewer.Position = newPosition;
                }
            }
        }

        private void UpdateMediaSlider(object sender, EventArgs e)
        {
            if (MediaViewer.NaturalDuration.HasTimeSpan)
            {
                MediaSlider.Maximum = MediaViewer.NaturalDuration.TimeSpan.TotalSeconds;
                MediaSlider.Value = MediaViewer.Position.TotalSeconds;

                UpdateCurrentTimeDisplay(MediaViewer.Position, MediaViewer.NaturalDuration.TimeSpan);
            }
        }

        private void MediaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MediaViewer.NaturalDuration.HasTimeSpan && MediaSlider.IsMouseCaptureWithin)
            {
                MediaViewer.Position = TimeSpan.FromSeconds(MediaSlider.Value);
            }
        }

        private void UpdateCurrentTimeDisplay(TimeSpan currentTime, TimeSpan duration)
        {
            CurrentTimeText.Text = $"{currentTime:mm\\:ss} / {duration:mm\\:ss}";
        }

        private void ToggleFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (isFullScreen)
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Normal;
                isFullScreen = false;
            }
            else
            {
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
                isFullScreen = true;
            }
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}