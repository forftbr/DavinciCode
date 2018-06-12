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
using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;
namespace DavinciCode.PlayWindow
{
    /// <summary>
    /// PlayGame.xaml の相互作用ロジック
    /// </summary>
    public partial class PlayGame : Page
    {
        public PlayGame()
        {
            InitializeComponent();


            KinectRegion.SetKinectRegion(this, kinectRegion);

            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;

            // Use the default sensor
            this.kinectRegion.KinectSensor = KinectSensor.GetDefault();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PlayOne.StartDavinciCodeSinglePlay();
        }

        public void Return()
        {
            NavigationService.Navigate(new Uri("../PlayWindow/MainMenu.xaml", UriKind.Relative));
        }

        public void Reload()
        {
            PlayOne.StartDavinciCodeSinglePlay();
        }
    }
}
