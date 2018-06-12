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

namespace DavinciCode.UControl
{
    /// <summary>
    /// PlayerWinShow.xaml の相互作用ロジック
    /// </summary>
    public partial class PlayerWinShow : UserControl
    {
        public PlayerWinShow()
        {
            InitializeComponent();
            Canvas.SetTop(this, 150d);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {

            base.OnMouseLeftButtonDown(e);
            var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);
            _parent.StartDavinciCodeSinglePlay();
        }
    }
}
