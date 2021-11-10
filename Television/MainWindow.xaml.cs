using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Television
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TV tv = new TV();
        public MainWindow()
        {
            InitializeComponent();
            var worker = Worker.Instance;
        }

        private void OnOff_Click(object sender, RoutedEventArgs e)
        {
            if (tv.Active)
            {
                tv.ShutDown();
                OnOff.Background = new SolidColorBrush(Colors.Red);
                DispCh.Content = "--";
                DispVol.Content = "--";
                DispSrc.Content = "--";

            }
            else
            {
                tv.StartUp();
                OnOff.Background = new SolidColorBrush(Colors.Green);
                DispCh.Content = (Defaults.Channels)tv.Channel;
                DispVol.Content = tv.Volume;
                DispSrc.Content = (Defaults.Sources)tv.Source;

            }
        }

        private void VolUp_Click(object sender, RoutedEventArgs e)
        {
            tv.VolumeUp();
            Debug.WriteLine(tv.Volume);
            DispVol.Content = tv.Volume;
        }

        private void VolDown_Click(object sender, RoutedEventArgs e)
        {
            tv.VolumeDown();
            Debug.WriteLine(tv.Volume);
            DispVol.Content = tv.Volume;
        }

        private void ChUp_Click(object sender, RoutedEventArgs e)
        {
            tv.ChannelUp();
            Debug.WriteLine(tv.Channel);
            DispCh.Content = (Defaults.Channels)tv.Channel;
        }

        private void ChDown_Click(object sender, RoutedEventArgs e)
        {
            tv.ChannelDown();
            Debug.WriteLine(tv.Channel);
            DispCh.Content = (Defaults.Channels)tv.Channel;
        }

        private void SrcUp_Click(object sender, RoutedEventArgs e)
        {
            tv.SourceUp();
            DispSrc.Content = (Defaults.Sources)tv.Source;
        }

        private void SrcDown_Click(object sender, RoutedEventArgs e)
        {
            tv.SourceDown();
            DispSrc.Content = (Defaults.Sources)tv.Source;
        }
    }
}
