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

namespace DavinciCode.Class
{
    class STATE
    {
        public STATE(Thickness a, double b, double c, int d, double e)
        {
            this._margin = a;
            this._height = b;
            this._width = c;
            this._ZIndex = d;
            this._Opacity = e;

        }
        private Thickness _margin;

        public Thickness Margin
        {
            get { return _margin; }
        }
        private double _height;

        public double Height
        {
            get { return _height; }
        }
        private double _width;

        public double Width
        {
            get { return _width; }
        }
        private int _ZIndex;

        public int ZIndex
        {
            get { return _ZIndex; }
        }
        private double _Opacity;

        public double Opacity
        {
            get { return _Opacity; }
        }


        private static STATE[] _state = new STATE[11]
            {
                new STATE(new Thickness(-500, 0, 0, 0),180,213.75,2,0),
                new STATE(new Thickness(-500, 0, 0, 0),180,213.75,3,0),
                new STATE(new Thickness(-500, 0, 0, 0),180,213.75,4,0),
                new STATE(new Thickness(-500, 0, 0, 0),180,213.75,5,1),
                new STATE(new Thickness(-389, 0, 0, 0),250,213.75,6,1),
                new STATE(new Thickness(0, 0, 0, 0),300,213.75,7,1),
                new STATE(new Thickness(389, 0, 0, 0),250,213.75,6,1),
                new STATE(new Thickness(500, 0, 0, 0),180,213.75,5,1),
                new STATE(new Thickness(500, 0, 0, 0),180,213.75,4,0),
                new STATE(new Thickness(500, 0, 0, 0),180,213.75,3,0),
                new STATE(new Thickness(500, 0, 0, 0),180,213.75,2,0)
            };
        public static STATE[] State
        {
            get { return STATE._state; }
        }
    }

    class MenuButton : Button
    {
        private int _key;
        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public MenuButton()
        {
            //this.Style = (Style)FindResource("ButSty");
        }

    }
}
