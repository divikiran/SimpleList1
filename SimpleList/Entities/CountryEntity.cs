using System;
using SQLite.Net.Attributes;

namespace SimpleList
{
	public class CountryEntity : Country, IEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id
		{
			get; set;
		}
	}
}
