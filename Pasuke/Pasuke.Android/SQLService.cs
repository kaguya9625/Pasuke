using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLService))]
public class SQLService : ISQLService
{
    public SQLiteConnection GetConnection()
    {
        var personalPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        var path = System.IO.Path.Combine(personalPath, "Pasukedb");
        return new SQLiteConnection(path);
    }
}