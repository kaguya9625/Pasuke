using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;
using PublicHoliday;
using Realms;
using Pasuke.Model;
using System.Collections.ObjectModel;

namespace Pasuke.Views
{
    public partial class AddSchedulePage : ContentPage
    {
        public AddSchedulePage()
        {
            InitializeComponent();
           
            _calendar.SpecialDates = new List<SpecialDate>
            {
                new SpecialDate(DateTime.Now)
                {
                    BorderColor = Color.Red,
                    BorderWidth = 3,
                    Selectable = true
                }
            };    

            Today();
        }
        private void Today()
        {
            DateTime dt = DateTime.Now;
            int year = dt.Year;

            SetWeekend(year);
        }
        //土日、祝日をカレンダーにセット
        private void SetWeekend(int year)
        {
            DateTime startDate = new DateTime(year, 1, 1);
            DateTime endDate = new DateTime(year, 12, 31);

            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
            {
                if (DayOfWeek.Saturday == day.DayOfWeek)
                {
                    _calendar.SpecialDates.Add(new SpecialDate(day)
                    {
                        TextColor = Color.Blue,
                        Selectable = true
                    });
                }
                else if (DayOfWeek.Sunday == day.DayOfWeek)
                {
                    _calendar.SpecialDates.Add(new SpecialDate(day)
                    {
                        TextColor = Color.Red,
                        Selectable = true
                    });
                }
            }
            IList<DateTime> result = new JapanPublicHoliday().PublicHolidays(year);

            foreach (var holiday in result)
            {
                _calendar.SpecialDates.Add(new SpecialDate(holiday)
                {
                    TextColor = Color.Red,
                    Selectable = true
                });
            }
        }
    }
}