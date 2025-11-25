using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication2.Model
{
    public class GuessThePriceModel
    {
        private int _minPrice { get; set; }
        private int _maxPrice { get; set; }
        private string _actualPrice { get; set; }
        private int _opPick { get; set; }
        private int _plPoints { get; set; }
        private int _opPoints { get; set; }
        private int _rounds { get; set; }

        private Random _rand;

        public int PlayerPoints { get { return _plPoints; } }
        public int OpponentPoints { get { return _opPoints; } }


        public GuessThePriceModel()
        {
            _rand = new Random();
            _plPoints = 0;
            _opPoints = 0;
            _rounds = 1;
        }

        public event EventHandler<GuessThePriceEventArgs> SetUpNextRound;
        public event EventHandler<GuessThePriceEventArgs> AfterGuess;
        public event EventHandler<EventArgs> GameEnd;
        public void NextRound()
        {
            _minPrice = _rand.Next(1, 200);
            _maxPrice = _rand.Next(900, 1200);
            _actualPrice = $"{_rand.Next(_minPrice, _maxPrice)}";
            _opPick = _rand.Next(_minPrice, _maxPrice);
            _rounds++;

            SetUpNextRound.Invoke(this, new GuessThePriceEventArgs(
                _minPrice, _maxPrice,
                /*_actualPrice*/ "????",
                _opPick, ""));
        }
        public void AfterPlayerGuess(int plPick)
        {


            if (plPick < Convert.ToInt32(_actualPrice) && _opPick < Convert.ToInt32(_actualPrice))
            {
                if (plPick > _opPick)
                {
                    AfterGuess.Invoke(this, new GuessThePriceEventArgs(_minPrice, _maxPrice, _actualPrice, _opPick, "Player Won this round!"));
                    _plPoints++;
                }
                else
                {
                    AfterGuess.Invoke(this, new GuessThePriceEventArgs(_minPrice, _maxPrice, _actualPrice, _opPick, "Opponent Won this round!"));
                    _plPoints++;
                }
            }
            else if (plPick < Convert.ToInt32(_actualPrice) && _opPick > Convert.ToInt32(_actualPrice))
            {
                AfterGuess.Invoke(this, new GuessThePriceEventArgs(_minPrice, _maxPrice, _actualPrice, _opPick, "Player Won this round!"));
                _plPoints++;
            }
            else if (_opPick < Convert.ToInt32(_actualPrice) && plPick > Convert.ToInt32(_actualPrice))
            {
                AfterGuess.Invoke(this, new GuessThePriceEventArgs(_minPrice, _maxPrice, _actualPrice, _opPick, "Opponent Won this round!"));
                _opPoints++;
            }
            else
            {
                AfterGuess.Invoke(this, new GuessThePriceEventArgs(_minPrice, _maxPrice, _actualPrice, _opPick, "Both players went over!"));
            }
            if (_rounds == 10)
            {
                GameEnd.Invoke(this, new EventArgs());
            }
        }
    }
}