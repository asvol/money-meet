using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace Asv.MoneyMeeting
{
    public class ShellViewModel: INotifyPropertyChanged
    {
        private decimal _totalSumm;
        private string _timeLeftString = "0:00";
        private string _summLeftString = "0 руб.";
        private Timer _timer;
        private DateTime _lastTickDateTime;
        private TimeSpan _spendTime;
        private decimal _rubSec;
        private bool _isSettingsOpened = true;
        private string _rubSecString;
        private static ShellViewModel _instance;
        private string _rubMinString;
        private string _rubDayString;
        private string _rubHourString;

        public static ShellViewModel Instance => _instance ?? (_instance = new ShellViewModel());


        public ShellViewModel()
        {
            TotalSumm = 1000000;
            _timer = new Timer(OnTick);
            _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        private void OnTick(object state)
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                OnTickInternal(DateTime.Now - _lastTickDateTime);
                _lastTickDateTime = DateTime.Now;
            });
        }

        private void OnTickInternal(TimeSpan timeSpan)
        {
            if (IsPause) return;
            _spendTime += timeSpan;
            TimeLeftString = $"{_spendTime.TotalMinutes:F0}:{_spendTime.Seconds:D2}";
            SummLeftString = $"{_rubSec * (decimal)_spendTime.TotalSeconds:F0} руб.";
        }

        public void Start()
        {
            IsSettingsOpened = false;
            _lastTickDateTime = DateTime.Now;
            IsPause = false;
        }

        public void Stop()
        {
            IsSettingsOpened = true;
            IsPause = true;
        }

        public bool IsPause { get; set; } = true;

        public bool IsSettingsOpened
        {
            get { return _isSettingsOpened; }
            set
            {
                _isSettingsOpened = value;
                OnPropertyChanged(nameof(IsSettingsOpened));
            }
        }

        public string RubSecString
        {
            get { return _rubSecString; }
            set
            {
                _rubSecString = value;
                OnPropertyChanged(nameof(RubSecString));
            }
        }

        public string RubMinString
        {
            get { return _rubMinString; }
            set
            {
                _rubMinString = value;
                OnPropertyChanged(nameof(RubMinString));
            }
        }

        public string RubDayString
        {
            get { return _rubDayString; }
            set
            {
                _rubDayString = value;
                OnPropertyChanged(nameof(RubDayString));
            }
        }

        public string RubHourString
        {
            get { return _rubHourString; }
            set
            {
                _rubHourString = value;
                OnPropertyChanged(nameof(RubHourString));
            }
        }

        public decimal TotalSumm
        {
            get { return _totalSumm; }
            set
            {
                _totalSumm = value;
                _rubSec = ((decimal) value) / 22m /8m/60/60;
                RubSecString = $"{_rubSec:F2}";
                RubMinString = $"{_rubSec*60:F2}";
                RubHourString = $"{_rubSec * 60*60:F2}";
                RubDayString = $"{_rubSec*60*60*8:F2}";
                OnPropertyChanged(nameof(TotalSumm));
                
            }
        }

        public string TimeLeftString
        {
            get { return _timeLeftString; }
            set
            {
                _timeLeftString = value;
                OnPropertyChanged(nameof(TimeLeftString));
            }
        }

        public string SummLeftString
        {
            get { return _summLeftString; }
            set
            {
                _summLeftString = value;
                OnPropertyChanged(nameof(SummLeftString));
            }
        }

        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {

            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void StartFromBegin()
        {
            _spendTime = TimeSpan.Zero;
            Start();
        }
    }

}