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
        int check = 0;
        private string _info;
        public string Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set => SetProperty(ref _enable, value);
        }

        private DateTime _sortdate;
        public DateTime SortDate
        {
            get => _sortdate;
            set => SetProperty(ref _sortdate,value);
        }
        public DelegateCommand Sort { get; set; }
        public DelegateCommand Delete { get; set; }

        private PasukeModel _model = new PasukeModel();

        public ICommand SelectedCommand { get; set; }
        public ReactiveCollection<schedule> ListView { get;} = new ReactiveCollection<schedule>();
        
        public SchedulePageViewModel()
        {
           
            
            ListView = new ReactiveCollection<schedule>();
            dataset();
            this.SelectedCommand = new Command<schedule>(date =>
            {
                switch (check)
                {
                    case 0:

                        break;

                    case 1:   
                        DateTime join = DateTime.Parse(date.Date + date.StartDate);
                        DateTimeOffset joinoffset = new DateTimeOffset(join.Year, join.Month, join.Day, join.Hour, join.Minute, 0, TimeSpan.Zero);
                        Realm realm = Realm.GetInstance();
                        var search = realm.All<Shiftdata>().Where(x => x.StartDate == joinoffset).First();
                        if (search != null)
                        {
                            realm.Write(() =>
                            {
                                realm.Remove(search);
                            });

                        }
                        Enable = true;
                        Info = "削除が完了しました";
                        ListView.Clear();
                        Task.Delay(1000);
                        dataset();
                        Info = "出勤予定日";
                        check = 0;
                        break;

                    case 2:

                        break;

                    default:
                        break;
                }
            });
            SortDate = DateTime.Now;
            Info = "出勤予定日";
            Enable = true;
            Delete = new DelegateCommand(DeleteCommand);
            Sort = new DelegateCommand(dataset);
        }
       
        public void DeleteCommand()
        {
            if(check == 0)
            {
                Info = "削除したいデータをタップしてください";
                Enable = false;
                check = 1;
            }
        }
  
        public void dataset()
        {
            Realm realm = Realm.GetInstance();
            int sort = SortDate.Month;
            var Schedule = new ObservableCollection<Shiftdata>(realm.All<Shiftdata>().OrderBy(x => x.StartDate));

            foreach (var a in Schedule)
            {
                {
                    ListView.Add(new schedule
                    {
                        Date = a.StartDate.ToString("M"),
                        StartDate = a.StartDate.ToString("t"),
                        EndDate = a.EndDate.ToString("t")
                    });
                }
            }
        }
    }   
}

       
    

 
  
