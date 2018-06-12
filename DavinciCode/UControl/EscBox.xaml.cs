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
namespace DavinciCode.UControl
{
    /// <summary>
    /// EscBox.xaml の相互作用ロジック
    /// </summary>
    public partial class EscBox : UserControl
    {
        public EscBox()
        {
            InitializeComponent();
        }
        private void but2_Click(object sender, RoutedEventArgs e)
        {
            var _parent = ((((((this.Parent as Grid).Parent as Grid).Parent as Class.DavinciCodePlay).Parent as KinectRegion).Parent as Grid).Parent as PlayWindow.PlayGame);
            _parent.Reload();
        }

        private void but1_Click(object sender, RoutedEventArgs e)
        {

            var _parent = ((((((this.Parent as Grid).Parent as Grid).Parent as Class.DavinciCodePlay).Parent as KinectRegion).Parent as Grid).Parent as PlayWindow.PlayGame);
            _parent.Return();

        }
    }
}
