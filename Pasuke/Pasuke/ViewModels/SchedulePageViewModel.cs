using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using Pasuke.ViewModels;

namespace Pasuke.ViewModels
{
    public class SchedulePageViewModel : ViewModelBase
    {

        public string AddShift { get; private set; }

        public DelegateCommand Mode1Command { get; set; }


        public SchedulePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ButtonText = "シフト入力";
            Enable = true;
            StartTime = new TimeSpan(17, 00, 00);
            EndTime = new TimeSpan(21, 30, 00);
            Mode1Command = new DelegateCommand(Mode1);
        }

        private List<DateTime> _specialdates = new List<DateTime>();
        public List<DateTime> SpecialDate
        {
            get => _specialdates;
            set => SetProperty(ref _specialdates, value);
        }

        private TimeSpan _starttime;
        public TimeSpan StartTime
        {
            get => _starttime;
            set => SetProperty(ref _starttime, value);
        }

        private TimeSpan _endtime;
        public TimeSpan EndTime
        {
            get => _endtime;
            set => SetProperty(ref _endtime, value);
        }

        private TimeSpan _resulttime;
        public TimeSpan timeSpan
        {
            get => _resulttime;
            set => SetProperty(ref _endtime, value);
        }

        private int _check;
        public int Check
        {
            get => _check;
            set => SetProperty(ref _check, value);
        }

        private string _info;
        public string info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        private string _buttontext;
        public string ButtonText
        {
            get => _buttontext;
            set => SetProperty(ref _buttontext, value);
        }
        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set => SetProperty(ref _enable, value);
        }

        private async void Mode1()
        {
            if (Check != 1)
            {
                Check = 1;
                info = "出勤日を選択してください";
                ButtonText = "終了";
            }
            else
            {
                Check = 0;
                Enable = false;
                info = "出勤日が登録されました";
                ButtonText = "シフト入力";
                await Task.Delay(3000);
                Enable = true;
                info = "";
            }
        }

        public ICommand DateCommand => new Command((obj) =>
        {
            if (Check == 1)
            {
                DateTime date = (DateTime)obj;
                if (SpecialDate?.Count > 0 == false)
                {
                    SpecialDate.Add(date);
                }
                else
                {
                    if (!SpecialDate.Contains(date))
                    {
                        SpecialDate.Add(date);
                    }

                }
            }
        });
    }
}

