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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AlarmDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer myTimer;
        MediaElement alarmSound;

        public MainPage()
        {
            this.InitializeComponent();

            alarmSound = new MediaElement();
            StartTimer();
        }

        public void StartTimer()
        {
            myTimer = new DispatcherTimer();
            myTimer.Tick += ReapitingActions;
            myTimer.Interval = TimeSpan.FromSeconds(1);
            myTimer.Start();
        }

        public void ReapitingActions(object sender, object e)
        {
            string currentTime = DateTime.Now.TimeOfDay.ToString().Substring(0,8);
            textBlock.Text = currentTime;
            string pickedTime = timePicker.Time.ToString();

            if (currentTime == pickedTime)
                PlaySound();
        }

        public async void PlaySound()
        {
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            var file = await folder.GetFileAsync("Alarm-tone.mp3");
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            alarmSound.SetSource(stream, "");
            alarmSound.Play();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            alarmSound.Stop();
        }
    }
}
