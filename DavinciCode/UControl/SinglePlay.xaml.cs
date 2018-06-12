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
using System.Windows.Media.Effects;

namespace DavinciCode.UControl
{
    /// <summary>
    /// SinglePlay.xaml の相互作用ロジック
    /// </summary>
    public partial class SinglePlay : UserControl
    {
        bool k = false;
        Random Ran = new Random();
        public SinglePlay()
        {
            InitializeComponent();
            Ran = new Random(this.GetHashCode());
        }

        private void escBox1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (k == false)
            {
                DoTransformAnimX(escBox1, new Thickness(-10, 155, 500, 227));
                k = true;
            }
        }

        void DoOpacityAnim(EscBox a)
        {
            DoubleAnimation OpacityAnim = new DoubleAnimation(0.8, new Duration(TimeSpan.FromSeconds(1)));
            OpacityAnim.AccelerationRatio = 0.2;
            OpacityAnim.DecelerationRatio = 0.7;
            a.BeginAnimation(OpacityProperty, OpacityAnim);
        }



        void DoTransformAnimX(EscBox a, Thickness b)
        {

            //DoubleAnimation Anim = new DoubleAnimation(b, new Duration(TimeSpan.FromSeconds(1.3)));

            //TranslateTransform trans = new TranslateTransform();
            //trans.BeginAnimation(TranslateTransform.XProperty, Anim);
            //a.RenderTransform = trans;
            ThicknessAnimation Anim = new ThicknessAnimation(b, new Duration(TimeSpan.FromSeconds(1.3)));
            Anim.AccelerationRatio = 0.2;
            Anim.DecelerationRatio = 0.7;
            a.BeginAnimation(MarginProperty, Anim);


        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            DoOpacityAnim(escBox1);
            CreateEllipse();
        }

        private void escBox1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (k == true)
            {
                DoTransformAnimX(escBox1, new Thickness(-250, 155, 740, 227));

                k = false;
            }
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
                BackSet.Children.Add(NewEllipse);
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

            BackSet.Children.Add(BiEllipse);
            SetPosition(BiEllipse, -200d, -50d);
            BiEllipse.BeginAnimation(OpacityProperty, MakeAnima(0d, 0.5, du, 0, 0, 1, false));


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
