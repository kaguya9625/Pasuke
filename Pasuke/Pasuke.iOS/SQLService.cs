using SQLite;
using Xamarin.Forms;
[assembly: Dependency(typeof(SQLService))]
public class SQLService : ISQLService
{
    public SQLiteConnection GetConnection()
    {
        var personalPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        var libraryPath = System.IO.Path.Combine(personalPath, "..", "Library");
        var path = System.IO.Path.Combine(libraryPath, "Pasukedb");
        return new SQLiteConnection(path);
    }
}