using System;
using System.Collections.Generic;
using System.Text;

namespace Pasuke.Model
{
   public class Models
    {
        public DateTime Parse(DateTime date, TimeSpan time)
        {
            date = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
            return date;
        }
    }
}