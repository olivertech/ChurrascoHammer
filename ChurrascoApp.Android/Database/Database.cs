using Android.App;
using ChurrascoApp.Droid.Database;
using ChurrascoApp.Interfaces;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(Database))]
namespace ChurrascoApp.Droid.Database
{
    /// <summary>
    /// Classe responsável por recuperar o banco de dados SQLite Android
    /// </summary>
    public class Database : ISQLiteConnection
    {
        private readonly string dataBaseName = "AppDatabase.db3";

        public Database()
        {
        }

        public SQLiteConnection GetConnection()
        {
            var databasePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dataBaseName);

            CopyDatabaseIfNotExists(databasePath);

            return new SQLiteConnection(databasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
        }

        private void CopyDatabaseIfNotExists(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                using (var br = new BinaryReader(Application.Context.Assets.Open(dataBaseName)))
                {
                    using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }
                }
            }
        }
    }
}