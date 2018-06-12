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
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace DavinciCode.UControl
{
    /// <summary>
    /// GuessBox.xaml の相互作用ロジック
    /// </summary>
    public partial class GuessBox : UserControl
    {
        public GuessBox()
        {
            InitializeComponent();
        }
        int k = 0;
        bool _lock = false;
        public GuessBox(double left, double top)
        {
            Canvas.SetLeft(this, left);
            Canvas.SetTop(this, top);
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_lock == false)
            {
                _lock = true;
                var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);
                if ((sender as Button).Content.ToString().CompareTo("-") == 0)
                {
                    if (_parent.NewCheeses.IconIndex == 12)
                    {
                        _parent.NewCheeses.Pass = true;
                        _parent.NewCheeses.Opera = true;
                        _parent._DavinciStep.Step = 4;
                        _parent.StepControl();
                    }
                    else
                    {
                        _parent.NewGetCheeses.Pass = true;
                        _parent._DavinciStep.Step = 6;
                        _parent.StepControl();
                    }
                }
                else
                {
                    if ((sender as Button).Content.ToString().CompareTo(_parent.NewCheeses.IconIndex.ToString()) == 0)
                    {
                        _parent.NewCheeses.Pass = true;
                        _parent.NewCheeses.Opera = true;
                        _parent._DavinciStep.Step = 4;
                        _parent.StepControl();
                    }
                    else
                    {
                        _parent.NewGetCheeses.Pass = true;
                        _parent._DavinciStep.Step = 6;
                        _parent.StepControl();
                    }
                }
                DoubleAnimation Anim = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.3)));
                this.BeginAnimation(OpacityProperty, Anim);
                DispatcherTimer timer = new DispatcherTimer();

                timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
            }
            //_parent.GuessBoxSet.Children.Remove(this);
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {

            if (_lock == false)
            {
                _lock = true;
                DoubleAnimation Anim = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.3)));
                this.BeginAnimation(OpacityProperty, Anim);

                DispatcherTimer timer = new DispatcherTimer();

                timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
            }

        }

        void timer_Tick(object sender, EventArgs e)
        {
            k++;
            if (k > 3)
            {
                var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);
                _parent.GuessBoxSet.Children.Remove(this);
                PlayBox _tmp = new PlayBox();
                foreach (var _temp in _parent.PlayerTwoBoxSet.Children)
                {
                    if (_temp is PlayBox)
                    {
                        _tmp = (_temp as PlayBox);
                        break;
                    }
                }

                _tmp.StackP.IsChoose = false;
                _lock = false;
                k = 0;
                (sender as DispatcherTimer).Stop();
            }

        }
    }
}
