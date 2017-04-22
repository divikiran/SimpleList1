using System;
using SQLite.Net.Attributes;

namespace SimpleList
{
	public class PersistenceEntity : IEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id
		{
			get; set;
		}

		//Persistence Property Key
		public string Key
		{
			get;
			set;
		}

		//Persistence Property Value
		public string Value
		{
			get;
			set;
		}
	}
}
