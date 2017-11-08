using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Game2048Agent
{
   public class TileStyle
    {
        short number = 0;
        Color backgraundColor;
        Color textColor;

        public short Number 
        {
            get
            {
                return number;
            } 
            set
            {
                number = value;
                switch (value)
                {
                    case 0:
                        backgraundColor = Colors.LightGray;
                        textColor = Colors.LightGray;
                        break;
                    case 2:
                        backgraundColor = Colors.LightBlue;
                        textColor = Colors.Black;
                        break;
                    case 4:
                        backgraundColor = Colors.LightSalmon;
                        textColor = Colors.Black;
                        break;
                    case 8:
                        backgraundColor = Colors.Orange;
                        textColor = Colors.Black;
                        break;
                    case 16:
                        backgraundColor = Colors.OrangeRed;
                        textColor = Colors.Black;
                        break;
                    case 32:
                        backgraundColor = Colors.Purple;
                        textColor = Colors.White;
                        break;
                    case 64:
                        backgraundColor = Colors.Fuchsia;
                        textColor = Colors.Black;
                        break;
                    case 128:
                        backgraundColor = Colors.Gold;
                        textColor = Colors.Black;
                        break;
                    case 256:
                        backgraundColor = Colors.SeaGreen;
                        textColor = Colors.White;
                        break;
                    case 512:
                        backgraundColor = Colors.White;
                        textColor = Colors.Black;
                        break;
                    case 1024:
                        backgraundColor = Colors.Red;
                        textColor = Colors.Black;
                        break;
                    default :
                        backgraundColor = Colors.Maroon;
                        textColor = Colors.White;
                        break;
                    
                }
            }
        }
        public SolidColorBrush BackgraundColor { get { return new SolidColorBrush(backgraundColor); } }
        public SolidColorBrush TextColor { get { return new SolidColorBrush(textColor); } }

        public static readonly TileStyle _instance = new TileStyle();

        public TileStyle()
        {
           
        }
    }
}
