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
using System.Windows.Threading;
using System.Threading.Tasks;
namespace DavinciCode.UControl
{
    /// <summary>
    /// CheeseShowBox.xaml の相互作用ロジック
    /// </summary>
    public partial class CheeseShowBox : UserControl
    {
        public CheeseShowBox()
        {
            InitializeComponent();
        }
        public void AddCheese(UIElement uielement, bool _mySelf)
        {
            (uielement as Class.Cheese).Locked = true;
            this.MainCheeseShow.Children.Add(uielement as Class.Cheese);
            if (_mySelf)
            {
                (uielement as Class.Cheese).Opera = true;
                (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay).NewGetCheeses = (uielement as Class.Cheese);
                (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay).StepOnePlayTwoStep();
            }
            else
            {
                (uielement as Class.Cheese).InPlaySecend = true;
            }
            //(this.Parent as DavinciCodeSinglePlay)._DavinciStep.Step++;
            //(this.Parent as DavinciCodeSinglePlay).StepControl();
        }
        public UIElement ShowCheese()
        {
            return this.MainCheeseShow.Children[0];
        }


        int k = 0;
        public UIElement RemoveCheese()
        {
            Class.Cheese _tmp = (this.MainCheeseShow.Children[0] as Class.Cheese);



            (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay).cheesenum--;

            if ((((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay).cheesenum == 0)
            {
                (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay).Lose();
            }
            this.MainCheeseShow.Children.RemoveAt(0);
            return _tmp;
        }
        public void RemoveCheeseNoReturn()
        {
            try
            {
                Class.Cheese _tmp = (this.MainCheeseShow.Children[0] as Class.Cheese);



                (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay).cheesenum--;

                if ((((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay).cheesenum == 0)
                {
                    (((this.Parent as Canvas).Parent as Grid).Parent as Class.DavinciCodePlay).Lose();
                }
                this.MainCheeseShow.Children.RemoveAt(0);
            }
            catch
            { }
            //return _tmp;
        }
    }
}
