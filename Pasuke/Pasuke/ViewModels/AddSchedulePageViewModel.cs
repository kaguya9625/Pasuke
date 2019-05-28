using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using Pasuke.Model;
using Prism.Mvvm;
using System.Linq;
using Realms;

namespace Pasuke.ViewModels
{
    public class AddSchedulePageViewModel : ViewModelBase
    {
        private PasukeModel _model = new PasukeModel();

        public string AddShift { get; private set; }

        public DelegateCommand Mode1Command { get; set; }


        public AddSchedulePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            
            ButtonText = "シフト入力";
            Enable = true; //ボタンの有効化
            StartTime = new TimeSpan(17, 00, 00);//業務開始時間初期設定
            EndTime = new TimeSpan(21, 30, 00);//業務終了時間初期設定
            Mode1Command = new DelegateCommand(Mode1);
        }
        List<DateTimeOffset> ShiftList = new List<DateTimeOffset>();
        
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
                _model.dbset(ShiftList, StartTime, EndTime);
                ShiftList.Clear();
                Enable = true;
                info = "";
                
            }
        }

        public ICommand DateCommand => new Command((obj) =>
        {

            if (Check == 1)
            {
                //リストが空、または既に追加されてないならリストに追加
                DateTime date = (DateTime)obj;
                if ((ShiftList?.Count > 0 == false) || !ShiftList.Contains(date))
                {
                    ShiftList.Add(date);
                }
            }
        });

    }
}

