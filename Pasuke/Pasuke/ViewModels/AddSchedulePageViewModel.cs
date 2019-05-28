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

        private bool _timeenable;
        public bool TimeEnable
        {
            get => _timeenable;
            set => SetProperty(ref _timeenable, value);
        }
        public AddSchedulePageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            ButtonText = "シフト入力";
            TimeEnable = true;
            Enable = true; //ボタンの有効化
            StartTime = new TimeSpan(17, 00, 00);//業務開始時間初期設定
            EndTime = new TimeSpan(21, 30, 00);//業務終了時間初期設定
            Mode1Command = new DelegateCommand(Mode1);
        }
        int Check = 0;
        private async void Mode1()
        {
            switch (Check)
            {
                case 0:
                    TimeEnable = false;
                    Check = 2;
                    info = "出勤日を選択してください";
                    ButtonText = "終了";
                    break;

                case 1:  
                    _model.dbset(ShiftList, StartTime, EndTime);
                    ShiftList.Clear();
                    Enable = false;
                    ButtonText = "シフト入力";
                    info = "出勤日が登録されました";
                    await Task.Delay(2000);
                    Enable = true;
                    TimeEnable = true;
                    info = "";
                    Check = 0;
                    break;

                case 2:
                    ButtonText = "シフト入力";
                    Enable = true;
                    TimeEnable = true;
                    info = "";
                    Check = 0;
                    break;

                default:
                    info = "error";
                    break;
            }
        }

        public ICommand DateCommand => new Command((obj) =>
        {

            if (Check != 0)
            {
                //リストが空、または既に追加されてないならリストに追加
                DateTime date = (DateTime)obj;
                if ((ShiftList?.Count > 0 == false) || (!ShiftList.Contains(date)))
                {
                    ShiftList.Add(date);
                    Check = 1;
                }else if(ShiftList.Contains(date))
                {
                    ShiftList.Remove(date);
                    Check = 1;
                }    
            }
            foreach(var a in ShiftList)
            {
                Console.WriteLine(a);
            }
        });

    }
}

