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
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;

namespace DavinciCode.UControl
{
    /// <summary>
    /// PlayBox.xaml の相互作用ロジック
    /// </summary>
    public partial class PlayBox : UserControl
    {

        Button[] SignButton;
        int A;
        public PlayBox()
        {
            InitializeComponent();
            SignButton = new Button[13];
            for (int i = 0; i < 13; i++)
            {
                SignButton[i] = new Button();
                SignButton[i].Height = 40;
                SignButton[i].Width = 30;
                SignButton[i].Style = (Style)FindResource("Sign");
                SignButton[i].Content = i.ToString();
                SignButton[i].Click += new RoutedEventHandler(Sing_Click);
                SignBox.Children.Add(SignButton[i]);
                SignButton[i].SetValue(Canvas.TopProperty, 10d);
                SignButton[i].SetValue(Canvas.LeftProperty, 30d + i * (80 - 37));
                SignButton[i].Visibility = Visibility.Hidden;
            }
        }
        Point? mousePoint = null;
        private bool _locked = false;
        public bool Locked
        {
            get { return _locked; }
            set
            {
                _locked = value;
            }
        }

        private bool _rightmode = false;
        public bool Rightmode
        {
            get { return _rightmode; }
            set
            {
                _rightmode = value;
                if (_rightmode == true)
                {

                    StackP.FlowDirection = FlowDirection.RightToLeft;
                    SignBox.FlowDirection = FlowDirection.RightToLeft;
                }
                else
                {
                    StackP.FlowDirection = FlowDirection.LeftToRight;
                    SignBox.FlowDirection = FlowDirection.LeftToRight;
                }
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
            mousePoint = null;
            this.ReleaseMouseCapture();
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Locked == false)
            {
                mousePoint = e.GetPosition(null);
                this.CaptureMouse();
                var parentpanle = this.Parent as Panel;
                parentpanle.Children.Remove(this);
                parentpanle.Children.Add(this);
            }
            base.OnMouseLeftButtonUp(e);

        }

        public void addCheese(int key, UIElement uielement)
        {
            StackP.AddCheese(key, uielement);
        }

        public void ShowCanPutInSign(UIElement uielement)
        {
            List<int> _tmp = StackP.CalculateCanPutInSign(uielement);
            for (int i = 0; i < _tmp.Count; i++)
                ShowSign(_tmp[i]);
        }

        private void ShowSign(int a)
        {
            A = a;
            DoubleAnimation _OpacityChange = new DoubleAnimation(0d, 1d, new Duration(TimeSpan.FromSeconds(0.5)));
            SignButton[a].Visibility = Visibility.Visible;
            _OpacityChange.Completed += new EventHandler(ShowSecendPlaySign);
            SignButton[a].BeginAnimation(OpacityProperty, _OpacityChange);
        }

        public void hideSign()
        {
            for(int i=0;i<13;i++)
            {
                SignButton[i].Visibility = Visibility.Hidden;
            }

        }

        private void ShowSecendPlaySign(Object sender, EventArgs e)
        {
            var _parent = (this.Parent as Class.DavinciCodePlay);
            if (this.Name.CompareTo("_PlayBoxFirst") != 0) Sing_Click(SignButton[A], new RoutedEventArgs());
        }

        private void Sing_Click(object sender, RoutedEventArgs e)
        {
            //var _aa = (this.Parent as Canvas);
            var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);
            if (this.Name.CompareTo("_PlayBoxFirst") == 0) addCheese(Convert.ToInt32((sender as Button).Content.ToString()), _parent.CheeseShowFirst.RemoveCheese());
            else addCheese(Convert.ToInt32((sender as Button).Content.ToString()), _parent.CheeseShowSecend.RemoveCheese());
            for (int i = 0; i < 13; i++)
            {

                //DispatcherTimer timer = new DispatcherTimer();

                //timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                //timer.Tick += new EventHandler(timer_Tick1);
                //timer.Start();

                SignButton[i].Visibility = Visibility.Hidden;

            }
            if (this.Name.CompareTo("_PlayBoxFirst") == 0)
            {
                _parent._DavinciStep.Step = 3;
                _parent.StepControl();
            }
        }
        //int k = 0;
        //void timer_Tick1(object sender, EventArgs e)
        //{
        //    k++;
        //    if (k > 4)
        //    {
        //        sender as dis
        //    }
        //}

        public bool IsLose()
        {
            return StackP.IsLose();
        }

        public void addnum(int a, int b)
        {
            TextBlock num = new TextBlock();
            if (b == 12)
            {
                num.Text = " -";
            }
            else
            {
                if (b < 10)
                {
                    num.Text = " " + b.ToString();
                }
                else
                {
                    num.Text = b.ToString();
                }

            }
            num.FontSize = 28;
            num.FontFamily = new FontFamily("Microsoft YaHei");
            num.Foreground = Brushes.Blue;
            DropShadowEffect dropshadow = new DropShadowEffect();
            dropshadow.ShadowDepth = 2;

            num.Effect = dropshadow;
            NumBox.Children.Add(num);
            num.SetValue(Canvas.TopProperty, 8d);
            num.SetValue(Canvas.LeftProperty, 47d + a * (80 - 37));
        }

        public void Guess_show()
        {
            StackP.Guess_show();
        }
        public void Guess_hide()
        {
            StackP.Guess_hide();
        }
        public void AllClear()
        {
            StackP.AllClear();
        }

        private Class.Cheese CheeseTmp = new Class.Cheese();
        public void AIPutACheeseIntoChesseShow()
        {

            var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);
            Class.Cheese _temp = new Class.Cheese();


            List<int> _tmp = new List<int>();

            int key = 0;
            foreach (var tempcheese in _parent.CheeseSet.Children)
            {
                if (tempcheese is Class.Cheese)
                {
                    if ((tempcheese as Class.Cheese).Pass == false)
                    {
                        _tmp.Add(key);
                    }
                }
                key++;
            }

            _temp = (_parent.CheeseSet.Children[_tmp[_parent.ran.Next(_tmp.Count)]] as Class.Cheese);
            _temp.Locked = true;

            //foreach (var _item in _parent.CheeseSet.Children)
            //{
            //    if (_item is Cheese)
            //    {
            //        _tmp = (_item as Cheese);
            //        break;
            //    }
            //}
            CheeseTmp = _temp;
            _parent.CheeseSet.Children.Remove(_temp);
            _parent.CheeseSet.Children.Add(_temp);
            DoubleAnimation TopMove = new DoubleAnimation(Canvas.GetTop(_temp), Canvas.GetTop(_parent.CheeseShowSecend) + 25, new Duration(TimeSpan.FromSeconds(1)));
            DoubleAnimation LeftMove = new DoubleAnimation(Canvas.GetLeft(_temp), Canvas.GetLeft(_parent.CheeseShowSecend) + 25, new Duration(TimeSpan.FromSeconds(1)));

            TopMove.DecelerationRatio = 0.7;
            LeftMove.AccelerationRatio = 0.3;
            LeftMove.DecelerationRatio = 0.7;
            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Completed += new EventHandler(EndAIPutACheeseIntoChesseShow);
            myStoryboard.Children.Add(TopMove);
            myStoryboard.Children.Add(LeftMove);

            Storyboard.SetTarget(TopMove, _temp);
            Storyboard.SetTargetProperty(TopMove, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTarget(LeftMove, _temp);
            Storyboard.SetTargetProperty(LeftMove, new PropertyPath(Canvas.LeftProperty));
            myStoryboard.Begin();

        }
        private void EndAIPutACheeseIntoChesseShow(Object sender, EventArgs e)
        {

            var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);

            _parent.CheeseSet.Children.Remove(CheeseTmp);
            _parent.CheeseShowSecend.AddCheese(CheeseTmp, false);
            _parent.StepSecendPlayTwoStep();
        }


        public void AIPutCheeseIntoPlayBox(UIElement uIElement)
        {
            var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);
            List<int> _tmp = StackP.CalculateCanPutInSign(uIElement);
            int key = (_parent as Class.DavinciCodePlay).ran.Next(_tmp.Count);

            ShowSign(_tmp[key]);
            //Sing_Click(SignButton[key], new RoutedEventArgs());
        }

        int timerKey = 0;


        public bool LowAIGuess()
        {
            List<int> _tmp = StackP.CatchUnpassCheese();
            var _parent = (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay);

            int guessnum = (_parent as Class.DavinciCodePlay).ran.Next(13);
            int CatchCheese = _tmp[(_parent as Class.DavinciCodePlay).ran.Next(_tmp.Count)];

            (StackP.MainSP.Children[CatchCheese] as Class.Cheese).Guess_in();
            addnum(CatchCheese, guessnum);

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            if (guessnum == (StackP.MainSP.Children[CatchCheese] as Class.Cheese).IconIndex)
            {
                (StackP.MainSP.Children[CatchCheese] as Class.Cheese).Pass = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timerKey++;
            if (timerKey > 20)
            {
                NumBox.Children.Clear();
                (sender as DispatcherTimer).Stop();
                timerKey = 0;
                foreach (var _tmp in StackP.MainSP.Children)
                {
                    if (_tmp is Class.Cheese)
                    {
                        (_tmp as Class.Cheese).Guess_out();
                    }
                }
            }
        }

        public void AIGuessWrong(/*UIElement uIElement*/)
        {
            List<int> _tmp = StackP.CatchUnpassCheese();
            //(uIElement as Cheese).Pass = true;
            Random Ran = new Random();
            int a = _tmp[Ran.Next(_tmp.Count)];
            (StackP.MainSP.Children[a] as Class.Cheese).Pass = true;
            (StackP.MainSP.Children[a] as Class.Cheese).Opera = true;
        }
    }
}
