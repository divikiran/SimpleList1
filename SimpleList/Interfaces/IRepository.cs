using System;
using System.Collections.Generic;
using SQLite.Net;

namespace SimpleList
{
	public interface IRepository<T> where T : class, IEntity
	{
		SQLiteConnection Connection { get; set; }
		int Insert(T item);
		void Update(T item);
		void Delete(T item);
		List<T> GetAll();
		void DeleteAll();
		void InsertAll(List<T> items);
		void UpdateAll(List<T> items);
		int InsertOrReplaceAll(List<T> items);
		void Dispose();
		int GetTableRowsCount();
		int InsertOrUpdate(T Item);
		T GetById();
	}
}
