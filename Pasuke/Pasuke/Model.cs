using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Reactive.Bindings;
using Realms;
using System.Diagnostics;

namespace Pasuke.Model
{
    public class PasukeModel : BindableBase
    {
        //データベースに情報を追加する関数
        //引数(シフトのリスト、出勤時間、退勤時間)
        public void dbset(List<DateTimeOffset> shiftlist, TimeSpan start, TimeSpan end)
        {
            //Realmのインスタンスを生成
            var realm = Realm.GetInstance();
            //出勤日+開始時間　出勤日+退勤時間の DatetimOffset型の変数の宣言
            foreach (DateTimeOffset item in shiftlist)
            {
                DateTimeOffset startdate = new DateTimeOffset(item.Year, item.Month, item.Day, start.Hours, start.Minutes, 0, TimeSpan.Zero);
                DateTimeOffset enddate = new DateTimeOffset(item.Year, item.Month, item.Day, end.Hours, end.Minutes, 0, TimeSpan.Zero);
               //上記二つの変数がデータベースに登録されているかどうかbool変数で確認
                bool existdata = realm.All<Shiftdata>().Where(x => x.StartDate == startdate 
                                                              || x.EndDate == enddate ).Any();
                //含まれていない場合データベースに登録
                if (!existdata)
                {
                    realm.Write(() =>
                    {
                        realm.Add(new Shiftdata { StartDate = startdate, EndDate = enddate });
                        Console.WriteLine("追加");
                    });
                }
            }
        }
      //DB削除用関数
        public void deletedb()
        {
            var config = new RealmConfiguration();
            Realm.DeleteRealm(config);
        }
    }
    //DB構造
        public class Shiftdata : RealmObject
        {
            [PrimaryKey]
            
            public string id { get; set; } = Guid.NewGuid().ToString();
            public DateTimeOffset StartDate { get; set; }
            public DateTimeOffset EndDate { get; set; }
         }
    //ListView用クラス
    public class schedule
    {
        public string Date { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
