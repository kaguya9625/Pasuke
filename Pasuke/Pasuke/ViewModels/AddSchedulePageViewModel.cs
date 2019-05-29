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

        //modelのインスタンスを生成
        private PasukeModel _model = new PasukeModel();

        //ボタンのコマンドの設定
        public DelegateCommand Mode1Command { get; set; }

       //DB登録前の格納用リストの宣言
        List<DateTimeOffset> ShiftList = new List<DateTimeOffset>();
        
        //Timepickerの値取得用プロパティ宣言
        private TimeSpan _starttime;
        public TimeSpan StartTime
        {
            get => _starttime;
            set => SetProperty(ref _starttime, value);
        }

        //Timepickerの値取得用プロパティ宣言
        private TimeSpan _endtime;
        public TimeSpan EndTime
        {
            get => _endtime;
            set => SetProperty(ref _endtime, value);
        }
        //インフォメーション用ラベルのプロパティ宣言
        private string _info;
        public string info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }
        //ボタンテキストのプロパティ宣言
        private string _buttontext;
        public string ButtonText
        {
            get => _buttontext;
            set => SetProperty(ref _buttontext, value);
        }
        //ボタンの有効化・無効化のプロパティ宣言
        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set => SetProperty(ref _enable, value);
        }
        //Timepickernの有効化・無効化のプロパティ宣言
        private bool _timeenable;
        public bool TimeEnable
        {
            get => _timeenable;
            set => SetProperty(ref _timeenable, value);
        }
        public AddSchedulePageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            //インフォメーション用ラベルの初期設定
            ButtonText = "シフト入力";
            //TimePickerの初期設定
            TimeEnable = true;
            //ボタンの有効化
            Enable = true; 
            //業務開始時間初期設定
            StartTime = new TimeSpan(17, 00, 00);
            //業務終了時間初期設定
            EndTime = new TimeSpan(21, 30, 00);
            //ボタンのコマンドの登録
            Mode1Command = new DelegateCommand(Mode1);
        }
        //check変数宣言
        int Check = 0;
        private async void Mode1()
        {
            //Checkが0の時ボタンタップでシフト入力モードに
            //シフト入力モードから再度ボタンタップでデータを登録
            switch (Check)
            {
                case 0:
                    //TimePickerを無効化
                    TimeEnable = false;
                    //Checkを2に設定
                    Check = 2;
                    //ラベル・ボタンテキストの設定
                    info = "出勤日を選択してください";
                    ButtonText = "終了";
                    break;

                case 1:  
                    //データをDBに登録する
                    _model.dbset(ShiftList, StartTime, EndTime);
                    //リストをクリア
                    ShiftList.Clear();
                    //ボタンを無効化
                    Enable = false;
                    //テキストの設定
                    ButtonText = "シフト入力";
                    info = "出勤日が登録されました";
                    //2秒待機用に設定
                    await Task.Delay(2000);
                    //ボタンやTimePickerの有効化
                    Enable = true;
                    TimeEnable = true;
                    info = "";
                    Check = 0;
                    break;

                case 2:
                    //シフトを登録せずにボタンをタップした際の処理
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
                //シフトリストが空または同じデータが含まれていない場合リストに追加
                if ((ShiftList?.Count > 0 == false) || (!ShiftList.Contains(date)))
                {
                    ShiftList.Add(date);
                    Check = 1;
                    //含まれていた際はリストから削除する
                }else if(ShiftList.Contains(date))
                {
                    ShiftList.Remove(date);
                    Check = 1;
                }    
            }
        });

    }
}

