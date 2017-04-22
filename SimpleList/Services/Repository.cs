using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SQLite.Net;
using Xamarin.Forms;

namespace SimpleList
{
	public class Repository<T> : IRepository<T> where T : class, IEntity, new()
	{
		protected static readonly object _locker = new object();

		public SQLiteConnection Connection
		{
			get; set;
		}

		public Repository()
		{
			try
			{
				lock (_locker)
				{
					if (this.Connection == null)
					{
						this.Connection = DependencyService.Get<ISQLite>().GetConnection();
						Debug.WriteLine("Sqlite Connection : " + Connection?.DatabasePath);
					}

					//string tableName = typeof(T).Name;
					//var tableFound = Connection.GetTableInfo(tableName);
					//if (tableFound != null && !tableFound.Any())
					//{
					Connection.CreateTable<T>();
					//}
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}


		public virtual int Insert(T item)
		{
			try
			{
				if (item != null)
				{
					lock (_locker)
					{
						return Connection.Insert(item);
					}
				}
				return -1;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return -1;
			}
		}


		public virtual void Update(T item)
		{
			try
			{
				if (item != null)
				{
					lock (_locker)
					{
						Connection.Update(item);
					}
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}


		public virtual void Delete(T item)
		{
			try
			{
				if (item != null)
				{
					lock (_locker)
					{
						Connection.Delete(item);
					}
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}


		public virtual void DeleteAll()
		{
			try
			{
				lock (_locker)
				{
					Connection.DeleteAll<T>();
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}


		public virtual List<T> GetAll()
		{
			try
			{
				lock (_locker)
				{
					return Connection.Table<T>().ToList();
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
			return null;
		}


		public virtual void InsertAll(List<T> items)
		{
			try
			{
				if (items != null)
				{
					lock (_locker)
					{
						Connection.InsertAll(items);
					}
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}


		public virtual void UpdateAll(List<T> items)
		{
			try
			{
				if (items != null)
				{
					lock (_locker)
					{
						Connection.UpdateAll(items);
					}
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}

		public virtual int InsertOrReplaceAll(List<T> items)
		{
			try
			{
				lock (_locker)
				{
					if (items != null)
					{
						return Connection.InsertOrReplaceAll(items);
					}
					return -1;
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return -1;
			}
		}

		public void Dispose()
		{
			try
			{
				lock (_locker)
				{
					Connection.Dispose();
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}

		public virtual int GetTableRowsCount()
		{
			try
			{
				lock (_locker)
				{
					return Connection.Table<T>().Count();
				}
			}
			catch (Exception e)
			{
				throw e;
			}
		}


		public virtual void DropAndCreateTable()
		{
			try
			{
				lock (_locker)
				{
					Connection.DropTable<T>();
					Connection.CreateTable<T>();
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				throw e;
			}
		}

		List<SQLiteConnection.ColumnInfo> CreateTableSqlite(string tableName)
		{
			var tableFound = Connection.GetTableInfo(tableName);
			if (tableFound != null && !tableFound.Any())
			{
				Connection.CreateTable<T>();
			}

			return tableFound;
		}

		public int InsertOrUpdate(T Item)
		{
			try
			{
				if (Item.Id == 0)
				{
					Connection.Insert(Item);
				}
				else
				{
					Connection.Update(Item);
				}
				return 1;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return -1;
			}
		}

		public T GetById(T Item)
		{
			try
			{
				return Connection.Get<T>(Item.Id);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return null;
			}
		}

		//TODO: if required
		public T GetById()
		{
			throw new NotImplementedException();
		}
	}
}
