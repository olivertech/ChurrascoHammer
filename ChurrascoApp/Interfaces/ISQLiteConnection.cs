using SQLite;

namespace ChurrascoApp.Interfaces
{
    public interface ISQLiteConnection
    {
        SQLiteConnection GetConnection();
    }
}
