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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DavinciCode.UControl
{
    /// <summary>
    /// GameTable.xaml の相互作用ロジック
    /// </summary>
    public partial class GameTable : UserControl
    {
        public GameTable()
        {
            InitializeComponent();
        }
        private void Us_Loaded(object sender, RoutedEventArgs e)
        {
            DoTransformAnimX(RightTable, -416d);
            DoTransformAnimX(LeftTable, 416d);
            //DoTransformAnimY(TopPointBox, -43d,1.5);
            //DoTransformAnimY(BottomPointBox, 40d, 1.5);
            //<Image Name="TopPointBox" Source="../Images/point.png" Height="130" Width="130" Margin="38,60,610,371" Opacity="0"/>
            //<Image Name="BottomPointBox" Source="../Images/point.png" Height="130" Width="130" Margin="608,360,40,71" Opacity="0"/>

        }


        void DoTransformAnimX(Image a, double b)
        {

            DoubleAnimation Anim = new DoubleAnimation(b, new Duration(TimeSpan.FromSeconds(1.3)));
            Anim.AccelerationRatio = 0.2;
            Anim.DecelerationRatio = 0.7;
            TranslateTransform trans = new TranslateTransform();
            trans.BeginAnimation(TranslateTransform.XProperty, Anim);
            a.RenderTransform = trans;

            DoubleAnimation OpacityAnim = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.8)));
            OpacityAnim.AccelerationRatio = 0.2;
            OpacityAnim.DecelerationRatio = 0.7;
            a.BeginAnimation(OpacityProperty, OpacityAnim);

        }

        void DoTransformAnimY(Image a, double b, double dely)
        {

            DoubleAnimation TransAnim = new DoubleAnimation(b, new Duration(TimeSpan.FromSeconds(0.3)));
            TransAnim.AccelerationRatio = 0.2;
            TransAnim.DecelerationRatio = 0.7;
            TransAnim.BeginTime = TimeSpan.FromSeconds(dely);

            DoubleAnimation ScaleAnim = new DoubleAnimation(1.2, 1, new Duration(TimeSpan.FromSeconds(0.3)));
            ScaleAnim.AccelerationRatio = 0.2;
            ScaleAnim.DecelerationRatio = 0.7;
            ScaleAnim.BeginTime = TimeSpan.FromSeconds(dely);

            TransformGroup group = new TransformGroup();
            TranslateTransform trans = new TranslateTransform();
            ScaleTransform scale = new ScaleTransform();


            trans.BeginAnimation(TranslateTransform.YProperty, TransAnim);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, ScaleAnim);
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, ScaleAnim);
            group.Children.Add(scale);
            group.Children.Add(trans);
            a.RenderTransform = group;

            DoubleAnimation OpacityAnim = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
            OpacityAnim.AccelerationRatio = 0.2;
            OpacityAnim.DecelerationRatio = 0.7;
            OpacityAnim.BeginTime = TimeSpan.FromSeconds(dely);
            a.BeginAnimation(OpacityProperty, OpacityAnim);

        }

    }
}
