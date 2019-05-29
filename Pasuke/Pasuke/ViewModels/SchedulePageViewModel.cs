using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Linq;
using System.Reactive.Linq;
using Pasuke.Model;
using Pasuke.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Realms;
using System.Collections.Generic;
using Prism.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Services;
using Xamarin.Forms.BehaviorsPack;


namespace Pasuke.ViewModel
{
    public class SchedulePageViewModel : BindableBase
    {
        //チェック変数
        int check = 0;
        //インフォメーション用ラベルのプロパティ宣言
        private string _info;
        public string Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }
        //ボタンの表示・非表示のプロパティ宣言
        //DB登録中などは操作できないようにするため
        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set => SetProperty(ref _enable, value);
        }
        //削除ボタンのテキスト変更用プロパティ宣言
        private string _deletetext;
        public string Deletetext
        {
            get => _deletetext;
            set => SetProperty(ref _deletetext, value);
        }
        //ソート機能搭載の残骸です
        //private DateTime _sortdate;
        //public DateTime SortDate
        //{
        //    get => _sortdate;
        //    set => SetProperty(ref _sortdate,value);
        //}

        //public DelegateCommand Sort { get; set; }
        //Delete Commandnのプロパティ宣言
        public DelegateCommand Delete { get; set; }
        //Modelのインスタンス生成
        private PasukeModel _model = new PasukeModel();
        //SelectedCommandのプロパティ宣言
        public ICommand SelectedCommand { get; set; }
        //ListViewのプロパティ宣言
        public ReactiveCollection<schedule> ListView { get;} = new ReactiveCollection<schedule>();
        
        public SchedulePageViewModel()
        {
           
            //ListView初期化
            ListView = new ReactiveCollection<schedule>();
            //ListViewにデータを追加
            dataset();
            //ListViewのアイテムをタップしたさいのコマンド
            this.SelectedCommand = new Command<schedule>(date =>
            {
                switch (check)
                {
                    //今後機能を追加するうえでswitch case文が有効だと思い現状switch case文にしています
                    case 0:

                        break;
                        //checkが1の場合ListViewで選択したシフトを削除する
                    case 1:   
                        //joinに勤務開始時間を連結したもの格納(日付+開始時間)
                        DateTime join = DateTime.Parse(date.Date + date.StartDate);
                        //Datetime→DatetimeOffsetに変換 Realmで使用可能な日付の型がDatetimeOffsetのみなので
                        DateTimeOffset joinoffset = new DateTimeOffset(join.Year, join.Month, join.Day, join.Hour, join.Minute, 0, TimeSpan.Zero);
                        //Realmインスタンス生成
                        Realm realm = Realm.GetInstance();
                        //searchにDBのデータからjoinoffsetと同等のものを取り出し
                        //仕様として同じ日付で同じ時間の物は追加されないので.First()で一件のみ取得
                        var search = realm.All<Shiftdata>().Where(x => x.StartDate == joinoffset).First();
                        //念のため確認
                        if (search != null)
                        {
                            realm.Write(() =>
                            {
                                //searchの削除
                                realm.Remove(search);
                            });

                        }
                        //ボタンの有効か
                        Enable = true;
                        //インフォメーション用ラベルの更新
                        Info = "削除が完了しました";
                        //ListViewのクリア(このあと更新を入れるため)
                        ListView.Clear();
                        //インフォメーション用ラベルが読めるよう3秒の空き時間を追加
                        Task.Delay(3000);
                        //ListViewにデータを追加
                        dataset();
                        //ラベル・ボタンなど・checkを初期状態に変更
                        Info = "出勤予定日";
                        Deletetext = "削除";
                        check = 0;
                        break;

                    case 2:

                        break;

                    default:
                        break;
                }
            });
           //インフォメーション用ラベル。ボタンの初期設定
            Info = "出勤予定日";
            Enable = true;
            Deletetext = "削除";
            //削除コマンドの登録
            Delete = new DelegateCommand(DeleteCommand);

        }
       
        public void DeleteCommand()
        {
            //checkが0の場合checkを1にしてswtich case文に移動
            if(check == 0)
            {
                //インフォメーション用ラベルの設定
                Info = "削除したいデータをタップしてください";
                Deletetext = "戻る";
                //check変数を削除用に変更
                check = 1;
            }
            //何もしないで戻る場合
            if(check == 1)
            {
                Info = "出勤予定日";
                Deletetext = "削除";
                check = 0;
            }
        }
    //ListViewにデータを登録する関数
    //引数、返り値無し
        public void dataset()
        {
            //Realmのインスタンス生成
            Realm realm = Realm.GetInstance();
            //ScheduleにDBのデータを代入
            var Schedule = new ObservableCollection<Shiftdata>(realm.All<Shiftdata>().OrderBy(x => x.StartDate));
            //ScheduleのデータをListViewに追加
            foreach (var a in Schedule)
            {
                {
                    ListView.Add(new schedule
                    {
                        //Dateに出勤日の日付を入力
                        Date = a.StartDate.ToString("M"),
                        //StartDateに出勤時間を入力
                        StartDate = a.StartDate.ToString("t"),
                        //EndDateに退勤時間を入力
                        EndDate = a.EndDate.ToString("t")
                    });
                }
            }
        }
    }   
}

       
    

 
  
