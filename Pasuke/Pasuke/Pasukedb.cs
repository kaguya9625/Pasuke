using SQLite;
using System;

public class Shiftdata
{
    [PrimaryKey,AutoIncrement]
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Worktime { get; set; }
    public bool Delete { get; set; }

}