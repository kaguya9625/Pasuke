using SQLite;

    public interface ISQLService
    {
        SQLiteConnection GetConnection();
    }
