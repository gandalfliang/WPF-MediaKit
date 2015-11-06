using System;
using System.Linq;
using System.Windows;
using WPFMediaKit.DirectShow.Controls;

namespace Test_Application
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mediaUriElement.DesiredPixelHeight = 720;
            mediaUriElement.DesiredPixelWidth = 1280;
            //mediaUriElement.UseYuv = true;
            mediaUriElement.FPS = 20;
            if (MultimediaUtil.VideoInputDevices.Any())
            {
                mediaUriElement.VideoCaptureDevice = MultimediaUtil.VideoInputDevices[0];
            }
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaUriElement.Stop();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            mediaUriElement.Play();
            //            var dlg = new OpenFileDialog();
            //            var result = dlg.ShowDialog();
            //            if (result == true)
            //                mediaUriElement.Source = new Uri(dlg.FileName);
        }
        private int defaultFocus = 50;
        private void AddFocus_OnClick(object sender, RoutedEventArgs e)
        {
            defaultFocus += 5;
            mediaUriElement.SetFocus(defaultFocus);
        }

        private void MinusFocus_OnClick(object sender, RoutedEventArgs e)
        {
            defaultFocus -= 5;
            mediaUriElement.SetFocus(defaultFocus);
        }

        private int defaultExposure = -1;
        private void AddExposure_OnClick(object sender, RoutedEventArgs e)
        {
            defaultExposure++;
            mediaUriElement.SetExposure(defaultExposure);
        }

        private void MinusExposure_OnClick(object sender, RoutedEventArgs e)
        {
            defaultExposure--;
            mediaUriElement.SetExposure(defaultExposure);
        }

        private void ToDefault_OnClick(object sender, RoutedEventArgs e)
        {
            mediaUriElement.SetToAuto();
        }

        private void Switch_OnClick(object sender, RoutedEventArgs e)
        {
            mediaUriElement.VideoCaptureDevice = MultimediaUtil.VideoInputDevices[1];
        }
    }
}
