using AvaloniaApplication2.Model;
using CommunityToolkit.Mvvm.Input;
using System;

namespace AvaloniaApplication2.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private GuessThePriceModel _model;
    private int _minPrice { get; set; }
    private int _maxPrice { get; set; }
    private string _actualPrice { get; set; }
    private int _opPick { get; set; }
    private int _plPick { get; set; }
    private string _winnerText { get; set; }
    private bool _isEnabled { get; set; }


    public int minPrice
    {
        get { return _minPrice; }
        set
        {
            if (_minPrice != value)
            {

                _minPrice = value;
                OnPropertyChanged(nameof(minPrice));
            }
        }
    }
    public int maxPrice
    {
        get { return _maxPrice; }
        set
        {
            if (_maxPrice != value)
            {

                _maxPrice = value;
                OnPropertyChanged(nameof(maxPrice));
            }
        }
    }
    public string actualPrice
    {
        get { return _actualPrice; }
        set
        {
            if (_actualPrice != value)
            {
                _actualPrice = value;
                OnPropertyChanged(nameof(actualPrice));
            }
        }
    }
    public int opPick
    {
        get { return _opPick; }
        set
        {
            if (_opPick != value)
            {

                _opPick = value;
                OnPropertyChanged(nameof(opPick));
            }
        }
    }
    public int plPick
    {
        get { return _plPick; }
        set
        {
            if (_plPick != value)
            {

                _plPick = value;
                OnPropertyChanged(nameof(plPick));
            }
        }
    }
    public string winnerText
    {
        get { return _winnerText; }
        set
        {
            if (_winnerText != value)
            {
                _winnerText = value;
                OnPropertyChanged(nameof(winnerText));
            }
        }
    }
    public bool isEnabled
    {
        get { return _isEnabled; }
        set
        {
            if (_isEnabled != value)
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(isEnabled));
            }
        }
    }

    public RelayCommand NextRoundCommand { get; set; }
    public RelayCommand PlayerGuessCommand { get; set; }
    public MainViewModel(GuessThePriceModel model)
    {
        _model = model;

        _model.SetUpNextRound += valami;
        _model.AfterGuess += aftrguess;
        _model.GameEnd += gameend;

        plPick = 0;

        NextRoundCommand = new RelayCommand(_model.NextRound);
        PlayerGuessCommand = new RelayCommand(() => _model.AfterPlayerGuess(plPick));
    }
    public void valami(object s, GuessThePriceEventArgs e)
    {
        minPrice = e._minPrice;
        maxPrice = e._maxPrice;
        actualPrice = e._actualPrice;
        opPick = e._opPick;
        isEnabled = false;
        winnerText = e._winnerText;
        OnPropertyChanged(nameof(opPick));
        OnPropertyChanged(nameof(minPrice));
        OnPropertyChanged(nameof(maxPrice));
        OnPropertyChanged(nameof(actualPrice));
        OnPropertyChanged(nameof(winnerText));

    }
    public void aftrguess(object s, GuessThePriceEventArgs e)
    {

        isEnabled = true;
        minPrice = e._minPrice;
        maxPrice = e._maxPrice;
        actualPrice = e._actualPrice;
        opPick = e._opPick;
        winnerText = e._winnerText;
        OnPropertyChanged(nameof(opPick));
        OnPropertyChanged(nameof(minPrice));
        OnPropertyChanged(nameof(maxPrice));
        OnPropertyChanged(nameof(actualPrice));
        OnPropertyChanged(nameof(winnerText));
    }
    public void gameend(object s, EventArgs e)
    {

        if (_model.PlayerPoints > _model.OpponentPoints)
        {
            Console.WriteLine("After 10 rounds you have won with " + _model.PlayerPoints + " points!");
        }
        else if (_model.OpponentPoints > _model.PlayerPoints)
        {
            Console.WriteLine("After 10 rounds your opponent has won with " + _model.OpponentPoints + " points!");
        }
        else
        {
            Console.WriteLine("After 10 rounds both player have " + _model.PlayerPoints + " points, so it is a draw!");
        }
    }
}
