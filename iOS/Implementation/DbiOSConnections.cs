using System;
using System.IO;
using SimpleList.iOS;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbiOSConnections))]
namespace SimpleList.iOS
{
	public class DbiOSConnections : ISQLite
	{
		private SQLiteConnectionWithLock _conn;

		SQLitePlatformIOS _sQLitePlatformIOS = new SQLitePlatformIOS();
		public SQLitePlatformIOS SQLitePlatformIOS
		{
			get
			{
				return _sQLitePlatformIOS;
			}
			set
			{
				_sQLitePlatformIOS = value;
			}
		}

		private static string GetDatabasePath()
		{
			var sqliteFilename = "SimpleList.db3";
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string libraryPath = Path.Combine(documentsPath, "..", "Documents");
			var path = Path.Combine(libraryPath, sqliteFilename);
			return path;
		}

		public SQLiteConnection GetConnection()
		{
			var dbPath = GetDatabasePath();
			// Return the synchronous database connection 
			return new SQLiteConnection(SQLitePlatformIOS, dbPath);
		}

		public SQLiteAsyncConnection GetAsyncConnection()
		{
			var dbPath = GetDatabasePath();
			var connectionFactory = new Func<SQLiteConnectionWithLock>(
				() =>
				{
					if (_conn == null)
					{
						_conn =
							new SQLiteConnectionWithLock(SQLitePlatformIOS,
								new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: true));
					}
					return _conn;
				});
			var asyncConnection = new SQLiteAsyncConnection(connectionFactory);
			return asyncConnection;
		}

		public void DeleteDatabase()
		{
			try
			{
				var path = GetDatabasePath();

				try
				{
					if (_conn != null)
					{
						_conn.Close();

					}
				}
				catch
				{
					// Best effort close. No need to worry if throws an exception
				}

				if (File.Exists(path))
				{
					File.Delete(path);
				}

				_conn = null;

			}
			catch
			{
				throw;
			}
		}

		public void CloseConnection()
		{
			if (_conn != null)
			{
				_conn.Close();
				_conn.Dispose();
				_conn = null;

				// Must be called as the disposal of the connection is not released until the GC runs.
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
		}
	}
}
