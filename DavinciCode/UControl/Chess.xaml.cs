using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading.Tasks;
using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;
namespace DavinciCode.UControl
{
    /// <summary>
    /// Chess.xaml の相互作用ロジック
    /// </summary>
    public partial class Chess : UserControl
    {
        public Chess()
        {
            InitializeComponent();
        }

        protected Point? mousePoint = null;
        protected Point? startPoint = null;

        private bool _locked = false;

        public bool Locked
        {
            get { return _locked; }
            set
            {
                _locked = value;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (mousePoint != null)
            {
                double newTop = e.GetPosition(null).Y - mousePoint.Value.Y + Canvas.GetTop(this);
                double newLeft = e.GetPosition(null).X - mousePoint.Value.X + Canvas.GetLeft(this);
                Canvas.SetTop(this, newTop);
                Canvas.SetLeft(this, newLeft);
                mousePoint = e.GetPosition(null);
            }
            base.OnMouseMove(e);
        }




        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (mousePoint == null) return;
            double newTop = e.GetPosition(null).Y - mousePoint.Value.Y + Canvas.GetTop(this);
            double newLeft = e.GetPosition(null).X - mousePoint.Value.X + Canvas.GetLeft(this);
            if (!(newTop > 110 && newTop < 370 && newLeft < 700 && newLeft > 0))
            {
                newTop = startPoint.Value.Y - mousePoint.Value.Y + Canvas.GetTop(this);
                newLeft = startPoint.Value.X - mousePoint.Value.X + Canvas.GetLeft(this);
                this.SetValue(Canvas.TopProperty, newTop);
                this.SetValue(Canvas.LeftProperty, newLeft);
            }
            mousePoint = null;
            startPoint = null;
            this.ReleaseMouseCapture();
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Locked == false)
            {
                mousePoint = e.GetPosition(null);
                startPoint = mousePoint;
                this.CaptureMouse();
                var parent = this.Parent;
                if (parent is StackPanel)
                {

                }
                else
                {
                    var parentpanle = this.Parent as Panel;
                    parentpanle.Children.Remove(this);
                    parentpanle.Children.Add(this);
                }
            }
     
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            //Sel_Rectangle.Visibility = System.Windows.Visibility.Visible;
            IconLight.BeginAnimation(OpacityProperty, new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.2))));
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            IconLight.BeginAnimation(OpacityProperty, new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.2))));
            //Sel_Rectangle.Visibility = System.Windows.Visibility.Collapsed;
            base.OnMouseLeave(e);
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }



    }
}
