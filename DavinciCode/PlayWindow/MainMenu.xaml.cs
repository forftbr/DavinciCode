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
using System.Windows.Interop;
using System.Windows.Threading;
using System.Media;
using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;

namespace DavinciCode.PlayWindow
{
    /// <summary>
    /// MainMenu.xaml の相互作用ロジック
    /// </summary>
    public partial class MainMenu : Page
    {
        Image Logo, Top, Bottom;
        Button StartButton;
        Class.MenuButton[] _MenuButtun = new Class.MenuButton[6];
        int StartNum;
        int MenuKey;
        private Random Ran;

        //////////////////////////////////////////////////////
        int k = 0;
        UControl.HelpWindow help1;
        UControl.Credit credit;
        bool MenuLock = false;
        //////////////////////////////////////////////////////
        void aa()
        {
            MediaPlayer Audio = new MediaPlayer();
            Audio.Open(new Uri("a.wma", UriKind.Relative));
            Audio.Play();
        }
        public MainMenu()
        {
            InitializeComponent();
            Ran = new Random(this.GetHashCode());

            KinectRegion.SetKinectRegion(this, kinectRegion);

            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;

            // Use the default sensor
            this.kinectRegion.KinectSensor = KinectSensor.GetDefault();




        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StartNum = 0;
            CreateBack();
            //CreateLogo();
            CreateMenuButton();
            CreateToBu();
            //CreateStartButton();
            Panel.SetZIndex(MainGrid, 100);
            RenderOptions.ProcessRenderMode = RenderMode.Default;
            aa();
        }

        private void CreateMenuButton()
        {
            MenuKey = 2;
            for (int i = 0; i < 6; i++)
            {

                string stylekey = "ButSty" + i.ToString();
                _MenuButtun[i] = new Class.MenuButton();
                _MenuButtun[i].Key = i + 3;
                _MenuButtun[i].Margin = Class.STATE.State[_MenuButtun[i].Key].Margin;
                _MenuButtun[i].Width = Class.STATE.State[_MenuButtun[i].Key].Width;
                _MenuButtun[i].Height = Class.STATE.State[_MenuButtun[i].Key].Height;
                _MenuButtun[i].Content = " " + i.ToString();
                _MenuButtun[i].Opacity = Class.STATE.State[_MenuButtun[i].Key].Opacity;
                _MenuButtun[i].Click += new RoutedEventHandler(MenuButtun_Click);
                _MenuButtun[i].Style = (Style)FindResource(stylekey);
                MainGrid.Children.Add(_MenuButtun[i]);
                Panel.SetZIndex(_MenuButtun[i], Class.STATE.State[_MenuButtun[i].Key].ZIndex);
                _MenuButtun[i].Opacity = 0;
                _MenuButtun[i].BeginAnimation(OpacityProperty, MakeAnima(0, 1, 0.5, 1, 0.2, 0.7, false));
            }
        }

        private void CreateStartButton()
        {
            StartButton = new Button();
            StartButton.Style = (Style)FindResource("StartButton");
            MainCanvas.Children.Add(StartButton);
            StartButton.Height = 70;
            StartButton.Content = "开始";
            StartButton.Opacity = 0;
            StartButton.SetValue(Canvas.LeftProperty, 340d);
            StartButton.SetValue(Canvas.TopProperty, 500d);
            StartButton.BeginAnimation(OpacityProperty, MakeAnima(0, 1, 0.5, 1.2, 0, 0.7, false));
            StartButton.Click += new RoutedEventHandler(StartButton_Click);
        }

        private void CreateToBu()
        {
            BitmapImage TopUri = new BitmapImage();
            TopUri.BeginInit();
            TopUri.UriSource = new Uri("../Images/Top.png", UriKind.Relative);
            TopUri.EndInit();
            BitmapImage BottomUri = new BitmapImage();
            BottomUri.BeginInit();
            BottomUri.UriSource = new Uri("../Images/Bottom.png", UriKind.Relative);
            BottomUri.EndInit();

            Top = new Image();
            Top.Height = 132;
            Top.Width = 800;
            Top.Source = TopUri;

            Bottom = new Image();
            Bottom.Height = 132;
            Bottom.Width = 800;
            Bottom.Source = BottomUri;

            MainCanvas.Children.Add(Top);
            MainCanvas.Children.Add(Bottom);
            //Panel.SetZIndex(Top, 20);
            Top.SetValue(Canvas.TopProperty, -140d);
            Bottom.SetValue(Canvas.TopProperty, 630d);

            TranslateTransform transTop = new TranslateTransform();
            transTop.BeginAnimation(TranslateTransform.YProperty, MakeAnima(0, 140, 1, 0.2, 0, 0.7, false));
            Top.RenderTransform = transTop;

            TranslateTransform transBot = new TranslateTransform();
            transBot.BeginAnimation(TranslateTransform.YProperty, MakeAnima(0, -160, 1, 0.2, 0, 0.7, false));
            Bottom.RenderTransform = transBot;
            Top.BeginAnimation(OpacityProperty, MakeAnima(0, 1, 1, 0.2, 0, 0.7, false));
            Bottom.BeginAnimation(OpacityProperty, MakeAnima(0, 1, 1, 0.2, 0, 0.7, false));
        }

        private void CreateLogo()
        {
            Logo = new Image();
            Logo.Width = 512;
            Logo.Height = 384;
            BitmapImage LogoUri = new BitmapImage();
            LogoUri.BeginInit();
            LogoUri.UriSource = new Uri("../Images/Logo.png", UriKind.Relative);
            LogoUri.EndInit();
            Logo.Source = LogoUri;
            MainCanvas.Children.Add(Logo);
            Logo.Opacity = 0;
            Logo.SetValue(Canvas.TopProperty, 100d);
            Logo.SetValue(Canvas.LeftProperty, 140d);
            Logo.BeginAnimation(OpacityProperty, MakeAnima(0d, 1d, 2, 1.2, 0.2, 0.7, false));
        }

        private void CreateBack()
        {
            CreateTxt();
            CreateEllipse();
        }

        private void CreateEllipse()
        {
            int AmountEllipse = Ran.Next(10) + 5;
            for (int i = 0; i < AmountEllipse; i++)
            {
                Ellipse NewEllipse = new Ellipse();
                byte alpha = (byte)Ran.Next(96, 192);
                double alpha_fx = alpha / 255.0;
                double Size = Ran.NextDouble() * 15 + 5;
                NewEllipse = SetEllipse(NewEllipse, Size, Colors.White, Ran.NextDouble() * 5 + 15, 0);
                MainCanvas.Children.Add(NewEllipse);
                double left = 800 * Ran.NextDouble();
                double top = 600 * Ran.NextDouble();
                SetPosition(NewEllipse, top, left);
                double duration = 1.0 * (Ran.Next(20) + 1.0) + 6.0;
                double delay = Ran.NextDouble();
                NewEllipse.BeginAnimation(Ellipse.OpacityProperty, MakeKeyanim(duration, delay, alpha_fx, 0.1, 0.9));
            }
            double du = 1.0 * (Ran.NextDouble() + 0.2) + 0.2;
            double de = Ran.NextDouble();
            Ellipse BiEllipse = new Ellipse();
            BiEllipse.Height = 1200;
            BiEllipse.Width = 1200;
            RadialGradientBrush Brush = new RadialGradientBrush(Colors.White, Color.FromArgb(0, 0, 0, 0));
            BiEllipse.Fill = Brush;
            BiEllipse.Opacity = 0;

            MainCanvas.Children.Add(BiEllipse);
            SetPosition(BiEllipse, -200d, -50d);
            BiEllipse.BeginAnimation(OpacityProperty, MakeAnima(0d, 0.5, du, 0, 0, 1, false));


        }

        private void CreateTxt()
        {
            int AmountText = Ran.Next(10) + 5;
            for (int i = 0; i < AmountText; i++)
            {
                int Word = Ran.Next(12);
                int Size = Ran.Next(90) + 10;
                FontFamily[] WordFamily = new FontFamily[] { new FontFamily("Bradley Hand ITC"), new FontFamily("Blackadder ITC"), new FontFamily("Chiller"), new FontFamily("Curlz MT"), new FontFamily("Giddyup Std") };
                int index = Ran.Next(5);

                byte alpha = (byte)Ran.Next(96, 192);
                double alpha_fx = alpha / 255.0;
                TextBlock NewTextBlock = new TextBlock();
                double blrEff = Ran.NextDouble() * 10 + 10;
                MainCanvas.Children.Add(SetTextBlock(NewTextBlock, Size, Word.ToString(), blrEff, 0, Colors.White, WordFamily[index]));
                double left = 600 * Ran.NextDouble() + 50;
                double top = 400 * Ran.NextDouble() + 50;
                SetPosition(NewTextBlock, top, left);

                double duration = 1.0 * (Ran.Next(20) + 1.0) + 15.0;
                double delay = 10.0 * Ran.NextDouble();

                TransformGroup group = new TransformGroup();
                ScaleTransform scale = new ScaleTransform();
                TranslateTransform trans = new TranslateTransform();
                RotateTransform rot = new RotateTransform();

                double x = Ran.NextDouble() * 800 - 400;
                double y = Ran.NextDouble() * 600 - 300;
                double fr = Ran.Next(50) - 20;
                double r = Ran.Next(200) - 100;
                double c = Ran.NextDouble() + 0.5;

                scale.BeginAnimation(ScaleTransform.ScaleXProperty, MakeAnima(1D, c, duration, delay, 0, 0.7, true));
                scale.BeginAnimation(ScaleTransform.ScaleYProperty, MakeAnima(1D, c, duration, delay, 0, 0.7, true));
                rot.BeginAnimation(RotateTransform.AngleProperty, MakeAnima(fr, r, duration, delay, 0, 0.7, true));
                trans.BeginAnimation(TranslateTransform.XProperty, MakeAnima(0D, x, duration, delay, 0, 0.7, true));
                trans.BeginAnimation(TranslateTransform.YProperty, MakeAnima(0D, y, duration, delay, 0, 0.7, true));

                group.Children.Add(scale);
                group.Children.Add(trans);
                group.Children.Add(rot);
                NewTextBlock.RenderTransform = group;
                NewTextBlock.BeginAnimation(TextBlock.OpacityProperty, MakeKeyanim(duration, delay, alpha_fx, 0.1, 0.9));

            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        private void CreateHelp()
        {
            //MenuKey = -1;
            MainGrid.BeginAnimation(OpacityProperty, MakeAnima(1, 0, 0.3d, 0d, 0, 0, false));
            help1 = new UControl.HelpWindow();
            HelpWindow.Opacity = 0;
            HelpWindow.Children.Add(help1);
            HelpWindow.BeginAnimation(OpacityProperty, MakeAnima(0, 0.8, 0.3d, 0.2d, 0, 0, false));
            help1.MouseLeftButtonUp += new MouseButtonEventHandler(help1_MouseLeftButtonUp);
            MenuLock = true;
        }

        void help1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            //HelpWindow.Children.Clear();
            //MainGrid.BeginAnimation(OpacityProperty, MakeAnima(0, 1, 0.3d, 0.2d, 0, 0, false));
            HelpWindow.BeginAnimation(OpacityProperty, MakeAnima(0.8, 0, 0.3d, 0.2d, 0, 0, false));

        }

        void timer_Tick(object sender, EventArgs e)
        {
            k++;
            if (k > 4)
            {
                HelpWindow.Children.Clear();
                MainGrid.BeginAnimation(OpacityProperty, MakeAnima(0, 1, 0.3d, 0.2d, 0, 0, false));
                (sender as DispatcherTimer).Stop();
                k = 0;
                MenuLock = false;

            }

        }



        private void CreateCredit(string a)
        {
            //MenuKey = -1;
            MainGrid.BeginAnimation(OpacityProperty, MakeAnima(1, 0, 0.3d, 0d, 0, 0, false));
            credit = new UControl.Credit(a);
            HelpWindow.Opacity = 0;
            HelpWindow.Children.Add(credit);
            HelpWindow.BeginAnimation(OpacityProperty, MakeAnima(0, 0.8, 0.3d, 0.2d, 0, 0, false));
            credit.MouseLeftButtonUp += new MouseButtonEventHandler(help1_MouseLeftButtonUp);
            MenuLock = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////

        void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartNum == 0)
            {
                StartButton.BeginAnimation(OpacityProperty, MakeAnima(1, 0, 0.5, 0, 0, 0, false));
                Logo.BeginAnimation(OpacityProperty, MakeAnima(1, 0, 0.5, 0, 0, 0, false));
                CreateMenuButton();

                StartNum += 1;
            }

        }

        void MenuButtun_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Class.MenuButton).Key == 5)
            {
                //MessageBox.Show(MenuKey.ToString());
                switch (MenuKey)
                {
                    case 0:
                        CreateCredit("Coming Soon");
                        break;
                    case 1:
                        CreateHelp();
                        break;
                    case 2:
                        NavigationService.Navigate(new Uri("../PlayWindow/PlayGame.xaml", UriKind.Relative));
                        break;
                    case 3:
                        CreateCredit("Coming Soon");
                        break;
                    case 4:
                        CreateCredit("Cheng Hao");
                        break;
                    case 5:
                        foreach (Window win in App.Current.Windows)
                        {
                            win.Close();
                        }
                        break;
                    //case -1:
                    //    break;
                }


            }
            if ((sender as Class.MenuButton).Key < 5)
            {
                Move(false);
            }
            if ((sender as Class.MenuButton).Key > 5)
            {
                Move(true);
            }
        }
        private void Move(bool _IsLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                if (_IsLeft) _MenuButtun[i].Key--;
                else _MenuButtun[i].Key++;
                if (_MenuButtun[i].Key >= 3 && _MenuButtun[i].Key <= 7)
                {
                    if (_MenuButtun[i].Key == 5)
                    {
                        MenuKey = i;
                    }
                    DoubleAnimation _to1size = new DoubleAnimation(Class.STATE.State[_MenuButtun[i].Key].Height, new Duration(TimeSpan.FromSeconds(0.2)));
                    ThicknessAnimation _to1set = new ThicknessAnimation(Class.STATE.State[_MenuButtun[i].Key].Margin, new Duration(TimeSpan.FromSeconds(0.2)));
                    _MenuButtun[i].BeginAnimation(HeightProperty, _to1size);
                    _MenuButtun[i].BeginAnimation(MarginProperty, _to1set);
                }
                if (_IsLeft)
                {
                    if (_MenuButtun[i].Key == 2 || _MenuButtun[i].Key == 7)
                    {
                        DoubleAnimation _toopacity = new DoubleAnimation(Class.STATE.State[_MenuButtun[i].Key].Opacity, new Duration(TimeSpan.FromSeconds(0.2)));
                        _MenuButtun[i].BeginAnimation(OpacityProperty, _toopacity);
                    }
                }
                else
                {
                    if (_MenuButtun[i].Key == 3 || _MenuButtun[i].Key == 8)
                    {
                        DoubleAnimation _toopacity = new DoubleAnimation(Class.STATE.State[_MenuButtun[i].Key].Opacity, new Duration(TimeSpan.FromSeconds(0.2)));
                        _MenuButtun[i].BeginAnimation(OpacityProperty, _toopacity);
                    }
                }
                Panel.SetZIndex(_MenuButtun[i], Class.STATE.State[_MenuButtun[i].Key].ZIndex);
            }
        }

        private Ellipse SetEllipse(Ellipse NewEllipse, double _size, Color _color, double _blurEff, double _opacity)
        {
            NewEllipse.Fill = new SolidColorBrush(_color);
            NewEllipse.Height = _size;
            NewEllipse.Width = _size;
            NewEllipse.Opacity = _opacity;
            BlurEffect BlurEff = new BlurEffect();
            BlurEff.Radius = _blurEff;
            NewEllipse.Effect = BlurEff;
            return NewEllipse;
        }

        private void SetPosition(Ellipse _obj, double _top, double _left)
        {
            _obj.SetValue(Canvas.TopProperty, _top);
            _obj.SetValue(Canvas.LeftProperty, _left);
        }

        private TextBlock SetTextBlock(TextBlock NewTextBlock, double _size, string _word, double _blurEff, double _opacity, Color _wordColor, FontFamily _txtFamily)
        {
            NewTextBlock.Text = _word;
            NewTextBlock.FontFamily = _txtFamily;
            NewTextBlock.FontSize = _size;
            NewTextBlock.Opacity = _opacity;
            NewTextBlock.Foreground = new SolidColorBrush(_wordColor);
            BlurEffect BlurEff = new BlurEffect();
            BlurEff.Radius = _blurEff;

            NewTextBlock.Effect = BlurEff;
            return NewTextBlock;
        }

        private void SetPosition(TextBlock obj, double _top, double _left)
        {
            obj.SetValue(Canvas.TopProperty, _top);
            obj.SetValue(Canvas.LeftProperty, _left);
        }

        private DoubleAnimation MakeAnima(double _from, double _to, double _time, double _delay, double _acce, double _dece, bool _repeat)
        {
            DoubleAnimation NewAnim = new DoubleAnimation(_from, _to, new Duration(TimeSpan.FromSeconds(_time)));
            NewAnim.BeginTime = TimeSpan.FromSeconds(_delay);
            if (_repeat)
                NewAnim.RepeatBehavior = RepeatBehavior.Forever;
            NewAnim.AccelerationRatio = 0.2;
            NewAnim.DecelerationRatio = 0.7;
            return NewAnim;
        }

        private DoubleAnimationUsingKeyFrames MakeKeyanim(double _time, double _delay, double _value, double _pre1, double _pre2)
        {
            DoubleAnimationUsingKeyFrames Anim = new DoubleAnimationUsingKeyFrames();
            Anim.Duration = new Duration(TimeSpan.FromSeconds(_time));
            LinearDoubleKeyFrame Anim_kf_1 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame Anim_kf_2 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame Anim_kf_3 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame Anim_kf_4 = new LinearDoubleKeyFrame();
            Anim_kf_1.KeyTime = KeyTime.FromPercent(0);
            Anim_kf_1.Value = 0.0;
            Anim_kf_2.KeyTime = KeyTime.FromPercent(_pre1);
            Anim_kf_2.Value = _value;
            Anim_kf_3.KeyTime = KeyTime.FromPercent(_pre2);
            Anim_kf_3.Value = _value;
            Anim_kf_4.KeyTime = KeyTime.FromPercent(1);
            Anim_kf_4.Value = 0.0;
            Anim.KeyFrames.Add(Anim_kf_1);
            Anim.KeyFrames.Add(Anim_kf_2);
            Anim.KeyFrames.Add(Anim_kf_3);
            Anim.KeyFrames.Add(Anim_kf_4);
            Anim.BeginTime = TimeSpan.FromSeconds(_delay);
            Anim.RepeatBehavior = RepeatBehavior.Forever;
            return Anim;
        }
    }
}
