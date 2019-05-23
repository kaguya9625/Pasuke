using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

public class DbUtility
{
    static readonly object Locker = new object();
    static SQLiteConnection _con;

    public DbUtility()
    {
        //コネクションを取得
        _con = DependencyService.Get<ISQLService>().GetConnection();
        //テーブルを更新する
        _con.CreateTable<Shiftdata>();
    }
    public IEnumerable<Shiftdata> GetItems()
    {
        lock (Locker)
        {
            return _con.Table<Shiftdata>().Where(m => m.Delete == false);
        }

    }
    public int SaveItem(Shiftdata item)
    {
        lock (Locker)
        {
            return _con.Insert(item);
        }
    }
}