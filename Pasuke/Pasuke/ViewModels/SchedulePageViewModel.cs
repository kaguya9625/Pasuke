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

namespace Pasuke.ViewModel
{
    public class SchedulePageViewModel : BindableBase
    {
        public ReactiveCollection<schedule> ListView { get; set; } = new ReactiveCollection<schedule>();   
        public SchedulePageViewModel()
        {
            Realm realm = Realm.GetInstance();
            var Schedule = new ObservableCollection<Shiftdata>(realm.All<Shiftdata>().OrderBy(x => x.StartDate));
            ListView = new ReactiveCollection<schedule>();

            foreach (var a in Schedule)
            {
                Console.WriteLine(a.StartDate);
               {
                    ListView.Add(new schedule { startdate = a.StartDate, enddate = a.EndDate });
               }
            }
            

        }
    }
}
 
  
