﻿using System;
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
        public void dbset(List<DateTimeOffset> shiftlist, TimeSpan start, TimeSpan end)
        {
            var realm = Realm.GetInstance();

            foreach (DateTimeOffset item in shiftlist)
            {
                DateTimeOffset startdate = new DateTimeOffset(item.Year, item.Month, item.Day, start.Hours, start.Minutes, 0, TimeSpan.Zero);
                DateTimeOffset enddate = new DateTimeOffset(item.Year, item.Month, item.Day, end.Hours, end.Minutes, 0, TimeSpan.Zero);
               
                bool existdata = realm.All<Shiftdata>().Where(x => x.StartDate == startdate 
                                                              || x.EndDate == enddate ).Any();

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
        public void deletedata(string startdate)
        {
            DateTimeOffset todate = DateTimeOffset.Parse(startdate);
            Console.WriteLine(todate);
            Realm realm = Realm.GetInstance();
            var removedata = realm.All<Shiftdata>().Where(r => r.StartDate == todate).First();

            realm.Write(() =>
            {
                realm.Remove(removedata);
            });
        }
        public void deletedb()
        {
            var config = new RealmConfiguration();
            Realm.DeleteRealm(config);
        }
    }

        public class Shiftdata : RealmObject
        {
            [PrimaryKey]
            public string id { get; set; } = Guid.NewGuid().ToString();
            public DateTimeOffset StartDate { get; set; }
            public DateTimeOffset EndDate { get; set; }
         }

    public class schedule
    {
        public string Date { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
