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
using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;
namespace DavinciCode.Class
{
    class Cheese : UControl.Chess
    {
        public Cheese()
        {
            IconIndex = 1;
            Color = 0;
        }
        public Cheese(int _key, int _color)
        {
            IconIndex = _key;
            Color = _color;
            Pass = false;
            InPlaySecend = false;
            Opera = false;
        }
        private int _Color = 0;
        public int Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                if (_Color == 0)
                {
                    var uri = new Uri("../Images/Icon_Black.png", UriKind.Relative);
                    ShowImage.Source = new System.Windows.Media.Imaging.BitmapImage(uri);
                    Number.Foreground = Brushes.White;
                }
                if (_Color == 1)
                {
                    var uri = new Uri("../Images/Icon_White.png", UriKind.Relative);
                    ShowImage.Source = new System.Windows.Media.Imaging.BitmapImage(uri);
                    Number.Foreground = Brushes.Black;
                }
            }
        }

        private int _Iconindex = -1;

        private bool _InPlaySecend = false;
        public bool InPlaySecend
        {
            get { return _InPlaySecend; }
            set { _InPlaySecend = value; }
        }

        public int IconIndex
        {
            get { return _Iconindex; }
            set
            {
                _Iconindex = value;
                if (_Iconindex == 12)
                {
                    Number.Text = "-";
                }
                else
                {
                    Number.Text = _Iconindex.ToString();
                }
            }
        }


        public int CompareTo(Cheese a)
        {
            if (IconIndex == a.IconIndex && Color == a.Color) return 0;
            if (IconIndex == 12 || a.IconIndex == 12) return 2;
            if (IconIndex > a.IconIndex) return -1;
            if (IconIndex < a.IconIndex) return 1;
            if (Color == 1) return 1;
            return -1;
        }



        private bool _Pass = false;

        public bool Pass
        {
            get { return _Pass; }
            set
            {
                _Pass = value;
                if (_Pass == true)
                {
                    IconRight.BeginAnimation(OpacityProperty, new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.2))));
                }
            }
        }


        public void Guess_in()
        {
            IconGuess.BeginAnimation(OpacityProperty, new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.2))));
        }

        public void Guess_out()
        {
            IconGuess.BeginAnimation(OpacityProperty, new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.2))));
        }


        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (this.Locked != true)
            {
                if (mousePoint == null) return;
                double newTop = e.GetPosition(null).Y - mousePoint.Value.Y + Canvas.GetTop(this);
                double newLeft = e.GetPosition(null).X - mousePoint.Value.X + Canvas.GetLeft(this);
                var _parent = this.Parent as Canvas;
                foreach (var _item in _parent.Children)
                {
                    if (_item is UControl.CheeseShowBox && (_item as UControl.CheeseShowBox).Name.CompareTo("CheeseShowFirst") == 0)
                    {
                        if (newTop > Canvas.GetTop((_item as UControl.CheeseShowBox)) && newTop < Canvas.GetTop((_item as UControl.CheeseShowBox)) + 130 && newLeft > Canvas.GetLeft((_item as UControl.CheeseShowBox)) && newLeft < Canvas.GetLeft((_item as UControl.CheeseShowBox)) + 130)
                        {
                            (this.Parent as Canvas).Children.Remove(this);
                            (_item as UControl.CheeseShowBox).AddCheese(this, true);
                            this.Locked = true;
                            break;
                        }
                    }
                }
            }
            base.OnMouseLeftButtonUp(e);
        }



        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {

            if (InPlaySecend)
            {

                if (this.Parent is StackPanel)
                {
                    var _parent = ((((((((((this.Parent as StackPanel).Parent as Grid).Parent as UControl.PutCheese).Parent as Grid).Parent as Grid).Parent as Grid).Parent as UControl.PlayBox).Parent as Canvas).Parent as Grid).Parent as DavinciCodePlay);
                    if (_parent._DavinciStep.Step == 3 || _parent._DavinciStep.Step == 5)
                    {
                        if (this.Pass == false)
                        {
                            if ((((this.Parent as StackPanel).Parent as Grid).Parent as UControl.PutCheese).IsChoose == false)
                            {
                                (((this.Parent as StackPanel).Parent as Grid).Parent as UControl.PutCheese).IsChoose = true;
                                UControl.GuessBox GuessB = new UControl.GuessBox(500, 100);
                                GuessB.Opacity = 0;
                                DoubleAnimation Anim = new DoubleAnimation(0.8, new Duration(TimeSpan.FromSeconds(0.3)));
                                _parent.GuessBoxSet.Children.Add(GuessB);
                                GuessB.BeginAnimation(OpacityProperty, Anim);
                                _parent.NewCheeses = this;
                            }
                        }
                    }
                }
            }

            base.OnMouseLeftButtonDown(e);
        }

        private bool _opera = false;

        public bool Opera
        {
            get { return _opera; }
            set
            {
                _opera = value;
                if (_opera == true)
                {
                    Number.BeginAnimation(OpacityProperty, new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.5))));
                }
                else
                {
                    Number.BeginAnimation(OpacityProperty, new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.5))));
                }
            }
        }


        public void KButton_ClickInPlayBoard(object sender, RoutedEventArgs e)
        {
            if (this.Locked != true)
            {
                var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);
                foreach (var _item in _parent.CheeseSet.Children)
                {
                    if (_item is Cheese)
                        (_item as Cheese).Locked = true;
                }
                _parent.CheeseSet.Children.Remove(this);
                _parent.CheeseSet.Children.Add(this);
                DoubleAnimation TopMove = new DoubleAnimation(Canvas.GetTop(this), Canvas.GetTop(_parent.CheeseShowFirst) + 25, new Duration(TimeSpan.FromSeconds(1)));
                DoubleAnimation LeftMove = new DoubleAnimation(Canvas.GetLeft(this), Canvas.GetLeft(_parent.CheeseShowFirst) + 25, new Duration(TimeSpan.FromSeconds(1)));
                TopMove.DecelerationRatio = 0.3;
                LeftMove.AccelerationRatio = 0.7;
                LeftMove.DecelerationRatio = 0.3;
                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Children.Add(TopMove);
                myStoryboard.Children.Add(LeftMove);
                myStoryboard.Completed += new EventHandler(PutACheese);
                Storyboard.SetTarget(TopMove, this);
                Storyboard.SetTargetProperty(TopMove, new PropertyPath(Canvas.TopProperty));
                Storyboard.SetTarget(LeftMove, this);
                Storyboard.SetTargetProperty(LeftMove, new PropertyPath(Canvas.LeftProperty));
                myStoryboard.Begin();

            }

            if (InPlaySecend)
            {

                if (this.Parent is StackPanel)
                {
                    var _parent = ((((((((((this.Parent as StackPanel).Parent as Grid).Parent as UControl.PutCheese).Parent as Grid).Parent as Grid).Parent as Grid).Parent as UControl.PlayBox).Parent as Canvas).Parent as Grid).Parent as DavinciCodePlay);
                    if (_parent._DavinciStep.Step == 3 || _parent._DavinciStep.Step == 5)
                    {
                        if (this.Pass == false)
                        {
                            if ((((this.Parent as StackPanel).Parent as Grid).Parent as UControl.PutCheese).IsChoose == false)
                            {
                                (((this.Parent as StackPanel).Parent as Grid).Parent as UControl.PutCheese).IsChoose = true;
                                UControl.GuessBox GuessB = new UControl.GuessBox(500, 100);
                                GuessB.Opacity = 0;
                                DoubleAnimation Anim = new DoubleAnimation(0.8, new Duration(TimeSpan.FromSeconds(0.3)));
                                _parent.GuessBoxSet.Children.Add(GuessB);
                                GuessB.BeginAnimation(OpacityProperty, Anim);
                                _parent.NewCheeses = this;
                            }
                        }
                    }
                }
            }

        }

        private void PutACheese(Object sender, EventArgs e)
        {

            var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);
            _parent.CheeseSet.Children.Remove(this);
            _parent.CheeseShowFirst.AddCheese(this, true);
        }
    }
}
