using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    public class ChartLineColorGenerator
    {
        int _LastUsedIndex = -1; // so that it can start at 0

        Color[] _ColorList = new Color[]
         {
                Color.Aqua,
                Color.BlueViolet,
                Color.Brown,
                Color.YellowGreen,
                Color.Coral,
                Color.DarkGoldenrod,
                Color.IndianRed,
                Color.Maroon,
                Color.SaddleBrown,
                Color.Peru,
                Color.DarkOliveGreen,
                Color.Yellow,
                Color.DarkSlateBlue,
                Color.OrangeRed,
                Color.MidnightBlue,
                Color.DarkSlateGray,
                Color.Fuchsia,
                Color.GreenYellow,
                Color.LightCoral,
                Color.LightGreen,
                Color.LightSlateGray,
                Color.Orange,
                Color.Olive,
                Color.DeepPink,
         };

        int IncrementIndex()
        {
            _LastUsedIndex++;
            if (_LastUsedIndex >= _ColorList.Length) 
                _LastUsedIndex = 0;
            return _LastUsedIndex;
        }

        public Color GetNextColor()
        {
            return _ColorList[IncrementIndex()];
        }
    }
}
