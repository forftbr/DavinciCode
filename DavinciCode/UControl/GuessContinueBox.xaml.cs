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
    /// GuessContinueBox.xaml の相互作用ロジック
    /// </summary>
    public partial class GuessContinueBox : UserControl
    {
        public GuessContinueBox()
        {
            InitializeComponent();
        }

        bool Islock = false;
        int k = 0;
        public GuessContinueBox(double Left, double Top)
        {
            Canvas.SetLeft(this, Left);
            Canvas.SetTop(this, Top);
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            Islock = true;
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.3))));

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            k++;
            if (k > 3)
            {
                var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);
                _parent._DavinciStep.Step = 5;
                _parent.ContinueBoxSet.Children.Remove(this);
                _parent.StepControl();
                k = 0;
                Islock = false;
                (sender as DispatcherTimer).Stop();

            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Islock = true;
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.3))));

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += new EventHandler(timer_Tick1);
            timer.Start();



        }

        void timer_Tick1(object sender, EventArgs e)
        {
            k++;
            if (k > 3)
            {
                var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);
                _parent._DavinciStep.Step = 6;
                _parent.ContinueBoxSet.Children.Remove(this);
                _parent.StepControl();
                k = 0;
                Islock = false;
                (sender as DispatcherTimer).Stop();

            }
        }

    }
}
