using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication2.Model
{
    public class GuessThePriceEventArgs
    {
        public int _minPrice { get; set; }
        public int _maxPrice { get; set; }
        public string _actualPrice { get; set; }
        public int _opPick { get; set; }
        public string _winnerText { get; set; }

        public GuessThePriceEventArgs(int minprice, int maxprice, string actualprice, int opPick, string winnertext)
        {
            _minPrice = minprice;
            _maxPrice = maxprice;
            _actualPrice = actualprice;
            _opPick = opPick;
            _winnerText = winnertext;
        }


    }
}
