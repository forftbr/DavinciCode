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

namespace DavinciCode.Class
{
    class DavinciCodePlay : UControl.SinglePlay
    {
        public struct DavinciStep
        {
            private int _round;

            public int Round
            {
                get { return _round; }
                set { _round = value; }
            }
            private int _whoplay;

            public int Whoplay
            {
                get { return _whoplay; }
                set { _whoplay = value; }
            }
            private int _step;

            public int Step
            {
                get { return _step; }
                set { _step = value; }
            }
        };

        public Random ran = new Random();

        private Cheese _NewCheeses = new Cheese();
        public Cheese NewCheeses
        {
            get { return _NewCheeses; }
            set { _NewCheeses = value; }
        }

        private Cheese _NewGetCheeses = new Cheese();
        public Cheese NewGetCheeses
        {
            get { return _NewGetCheeses; }
            set { _NewGetCheeses = value; }
        }


        private Cheese[] _Cheeses = new Cheese[26];
        public Cheese[] Cheeses
        {
            get { return _Cheeses; }
            set { _Cheeses = value; }
        }

        private UControl.PlayBox _PlayBoxFirst = new UControl.PlayBox();
        private UControl.PlayBox _PlayBoxSecend = new UControl.PlayBox();

        public DavinciStep _DavinciStep;


        private UControl.CheeseShowBox _CheeseShowFirst = new UControl.CheeseShowBox();
        public UControl.CheeseShowBox CheeseShowFirst
        {
            get { return _CheeseShowFirst; }
            set { _CheeseShowFirst = value; }
        }
        private UControl.CheeseShowBox _CheeseShowSecend = new UControl.CheeseShowBox();
        public UControl.CheeseShowBox CheeseShowSecend
        {
            get { return _CheeseShowSecend; }
            set { _CheeseShowSecend = value; }
        }

        public DavinciCodePlay()
        {
            ran = new Random(this.GetHashCode());
        }

        private int gcd(int a, int b) { if (b == 0) return a; else return gcd(b, a % b); }


        public void AllClear()
        {
            CheeseSet.Children.Clear();
            PlayerOneBoxSet.Children.Clear();
            PlayerTwoBoxSet.Children.Clear();
            GuessBoxSet.Children.Clear();
            ContinueBoxSet.Children.Clear();
            WinOrLoseBoxSet.Children.Clear();
            CheeseShowFirst.MainCheeseShow.Children.Clear();
            //EscBoxSet.Children.Clear();
        }

        public void StartDavinciCodeSinglePlay()
        {
            AllClear();

            DoubleAnimation _OpacityChange = new DoubleAnimation(0d, 1d, new Duration(TimeSpan.FromSeconds(1.2)));
            _OpacityChange.BeginTime = TimeSpan.FromSeconds(1.2);

            DoubleAnimation _OpacityChangeEight = new DoubleAnimation(0d, 0.8, new Duration(TimeSpan.FromSeconds(1.2)));
            _OpacityChange.BeginTime = TimeSpan.FromSeconds(1.2);

            _DavinciStep.Step = 1;
            _DavinciStep.Whoplay = 1;
            _DavinciStep.Round = 1;


            for (int i = 0; i < 13; i++)
            {
                Cheeses[i] = new Cheese(i, 0);
                Cheeses[i + 13] = new Cheese(i, 1);
            }


            int k = 0;
            int c = ran.Next(200);
            while (gcd(c, 26) != 1)
            c = ran.Next(200);

            for (int i = 0; i < 26; i++)
            {
                k = (k + 11) % 26;
                Cheeses[k].Height = 80;
                Cheeses[k].Width = 80;
                Cheeses[k].Opacity = 0;
                Cheeses[k].Opacity = 0;
                Cheeses[k].KButton.Click += Cheeses[k].KButton_ClickInPlayBoard; 
                Cheeses[k].BeginAnimation(OpacityProperty, _OpacityChange);
                Cheeses[k].Locked = true;
                CheeseSet.Children.Add(Cheeses[k]);
                Canvas.SetLeft(Cheeses[k], 250 + ran.Next(200));
                Canvas.SetTop(Cheeses[k], 150 + ran.Next(160));
                //_Cheeses[k].MouseUp += new MouseButtonEventHandler(Cheess_MouseUp);
            }

            cheesenum = 26;


            _PlayBoxFirst.Name = "_PlayBoxFirst";
            _PlayBoxFirst.AllClear();
            _PlayBoxFirst.hideSign();
            _PlayBoxFirst.Opacity = 0;
            _PlayBoxFirst.Locked = true;

            PlayerOneBoxSet.Children.Add(_PlayBoxFirst);
            Canvas.SetLeft(_PlayBoxFirst, 0);
            Canvas.SetTop(_PlayBoxFirst, 472);



            _PlayBoxSecend.FlowDirection = System.Windows.FlowDirection.LeftToRight;
            _PlayBoxSecend.Name = "_PlayBoxSecend";
            _PlayBoxSecend.AllClear();
            _PlayBoxSecend.Opacity = 0;
            _PlayBoxSecend.Locked = true;
            _PlayBoxSecend.Rightmode = true;

            PlayerTwoBoxSet.Children.Add(_PlayBoxSecend);
            Canvas.SetLeft(_PlayBoxSecend, 0);
            Canvas.SetTop(_PlayBoxSecend, -40);


            _PlayBoxFirst.BeginAnimation(OpacityProperty, _OpacityChangeEight);
            _PlayBoxSecend.BeginAnimation(OpacityProperty, _OpacityChangeEight);



            CheeseShowSecend.Opacity = 0;
            CheeseShowFirst.Opacity = 0;

            CheeseSet.Children.Add(CheeseShowFirst);
            CheeseShowFirst.Name = "CheeseShowFirst";

            Canvas.SetLeft(CheeseShowFirst, 615);
            Canvas.SetTop(CheeseShowFirst, 420);
            CheeseSet.Children.Add(CheeseShowSecend);
            CheeseShowSecend.Name = "CheeseShowSecend";

            Canvas.SetLeft(CheeseShowSecend, 40);
            Canvas.SetTop(CheeseShowSecend, 30);
            CheeseShowFirst.BeginAnimation(OpacityProperty, _OpacityChange);
            CheeseShowSecend.BeginAnimation(OpacityProperty, _OpacityChange);

            StepControl();
        }


        public void StepControl()
        {
            switch (_DavinciStep.Whoplay)
            {
                case 1:
                    switch (_DavinciStep.Step)
                    {
                        case 1:
                            if (_DavinciStep.Round > 13)
                                StepOnePlayThreeStep();
                            else StepOnePlayOneStep();
                            break;
                        case 2:
                            break;
                        case 3:
                            if (_DavinciStep.Round <= 4)
                            {
                                StepSecendPlayOneStep();
                                _DavinciStep.Round++;
                                _DavinciStep.Step = 1;
                                StepOnePlayOneStep();
                            }
                            else StepOnePlayThreeStep();
                            break;
                        case 4:
                            if (_PlayBoxSecend.IsLose())
                            {
                                UControl.PlayerWinShow a = new UControl.PlayerWinShow();
                                a.Opacity = 0;
                                WinOrLoseBoxSet.Children.Add(a);
                                a.BeginAnimation(OpacityProperty, new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.3))));

                            }
                            else
                            {
                                UControl.GuessContinueBox a = new UControl.GuessContinueBox(300, 200);
                                a.Opacity = 0;
                                ContinueBoxSet.Children.Add(a);
                                a.BeginAnimation(OpacityProperty, new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.3))));
                            }
                            break;
                        case 5:
                            break;
                        case 6:
                            StepOnePlayfourStep();
                            if (_DavinciStep.Round <= 13)
                            {
                                StepSecendPlayOneStep();
                                StepSecendPlayThreeStep();
                                _DavinciStep.Step = 1;
                                StepOnePlayOneStep();
                            }
                            else
                            {
                                _DavinciStep.Step = 3;
                            }
                            break;
                    }
                    break;
                case 2:
                    switch (_DavinciStep.Step)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4: break;
                        case 5: break;
                    }
                    break;
            }
        }

        private void StepOnePlayOneStep()
        {
            foreach (var _item in CheeseSet.Children)
            {
                if (_item is Cheese)
                    (_item as Cheese).Locked = false;
                /////////////////////////////////////////////
            }
        }

        public void StepOnePlayTwoStep()
        {
            foreach (var _item in CheeseSet.Children)
            {
                if (_item is Cheese)
                    (_item as Cheese).Locked = true;
            }
            _PlayBoxFirst.ShowCanPutInSign(CheeseShowFirst.ShowCheese());
        }

        public void StepOnePlayThreeStep()
        {
            _PlayBoxSecend.Guess_show();
        }
        public void StepOnePlayfourStep()
        {
            _PlayBoxSecend.Guess_hide();
        }

        //first
        private void StepSecendPlayOneStep()
        {
            _PlayBoxSecend.AIPutACheeseIntoChesseShow();
        }

        public void StepSecendPlayTwoStep()
        {
            _PlayBoxSecend.AIPutCheeseIntoPlayBox(CheeseShowSecend.ShowCheese());
        }

        private void StepSecendPlayThreeStep()
        {

            if (!_PlayBoxFirst.LowAIGuess())
            {
                _PlayBoxSecend.AIGuessWrong();

            }
        }

        public int cheesenum = -1;

        public void Lose()
        {

            UControl.PlayerLoseShow Loseshow = new UControl.PlayerLoseShow();
            Loseshow.Opacity = 0;
            WinOrLoseBoxSet.Children.Add(Loseshow);
            Loseshow.BeginAnimation(OpacityProperty, new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.3))));

        }



    }
}
