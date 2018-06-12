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
    /// PutCheese.xaml の相互作用ロジック
    /// </summary>
    public partial class PutCheese : UserControl
    {
        public bool IsChoose = false;


        public PutCheese()
        {
            InitializeComponent();
        }

        public void AddCheese(int key, UIElement uielement)
        {
            (uielement as Class.Cheese).Margin = new Thickness(-37, 0, 0, 0);
            MainSP.Children.Insert(key, uielement);
        }
        

        public void Guess_show()
        {

            List<int> _tmp = CatchUnpassCheese();

            for (int i = 0; i < _tmp.Count; i++)
            {
                (MainSP.Children[_tmp[i]] as Class.Cheese).Guess_in();

            }
            //foreach (var temp in MainSP.Children)
            //{
            //    if (temp is Cheese)
            //    {
            //        var temp1 = temp as Cheese;
            //        temp1.Guess_in();
            //    }
            //}
        }
        public void Guess_hide()
        {




            foreach (var temp in MainSP.Children)
            {
                if (temp is Class.Cheese)
                {
                    var temp1 = temp as Class.Cheese;
                    temp1.Guess_out();
                }
            }
        }

        public bool IsLose()
        {
            for (int i = 0; i < this.MainSP.Children.Count; i++)
                if ((this.MainSP.Children[i] as Class.Cheese).Opera == false && (this.MainSP.Children[i] as Class.Cheese).Pass == false) return false;
            return true;
        }
        public void AllClear()
        {
            this.MainSP.Children.Clear();
        }

        public List<int> CalculateCanPutInSign(UIElement uielement)
        {
            List<int> _tmp = new List<int>();
            if (this.MainSP.Children.Count == 0)
            {
                _tmp.Add(0);
                return _tmp;
            }
            int L = 0, R = 0, i;
            for (i = 0; i < this.MainSP.Children.Count; i++)
                if ((uielement as Class.Cheese).CompareTo(this.MainSP.Children[i] as Class.Cheese) == 1) { R = i; break; }
            if (i == this.MainSP.Children.Count) R = i;
            for (i = this.MainSP.Children.Count - 1; i >= 0; i--)
                if ((uielement as Class.Cheese).CompareTo(this.MainSP.Children[i] as Class.Cheese) == -1) { L = i; break; }
            if (i == -1) L = i;
            for (i = L + 1; i <= R; i++)
                _tmp.Add(i);
            return _tmp;
        }

        public List<int> CatchUnpassCheese()
        {
            List<int> _tmp = new List<int>();
            int key = 0;
            foreach (var tempcheese in this.MainSP.Children)
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
            return _tmp;
        }

    }
}
