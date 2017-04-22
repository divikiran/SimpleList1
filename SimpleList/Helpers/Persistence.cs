using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimpleList
{
	sealed class Persistence
	{
		private static readonly object _lockObj = new object();
		private static Persistence _instance = new Persistence();

		public static Persistence Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObj)
					{
						if (_instance == null)
						{
							_instance = new Persistence();
						}
					}
				}
				return _instance;
			}
		}

		public PersistenceRepository PersistenceRepository
		{
			get;
			set;
		} = new PersistenceRepository();

		string GetPersistenceValue(string key)
		{
			PersistenceEntity result = PersistenceRepository.GetByKey(key);
			return result?.Value;
		}

		void SetPersistenceValue(string key, string value)
		{
			var result = PersistenceRepository.GetByKey(key);
			if (result == null)
			{
				PersistenceRepository.Insert(new PersistenceEntity()
				{
					Key = key,
					Value = value
				});
			}
			else
			{
				result.Value = value;
				PersistenceRepository.Update(result);
			}
		}

		string _lastAppUsed;
		public string LastAppUsed
		{
			get
			{
				if (string.IsNullOrEmpty(_lastAppUsed))
				{
					var value = GetPersistenceValue(nameof(LastAppUsed));
					_lastAppUsed = value;
				}
				return _lastAppUsed;
			}
			set
			{
				_lastAppUsed = value;
				SetPersistenceValue(nameof(LastAppUsed), value);
			}
		}


		public List<Country> CountryList { get; internal set; }

		bool _firstTimeLoad = true;
		public bool FirstTimeLoad
		{
			get
			{
				var value = GetPersistenceValue(nameof(FirstTimeLoad));
				_firstTimeLoad = value == null ? true : Convert.ToBoolean(value);
				return _firstTimeLoad;
			}
			set
			{
				_firstTimeLoad = value;
				SetPersistenceValue(nameof(FirstTimeLoad), Convert.ToString(value));
			}
		}
	}
}
