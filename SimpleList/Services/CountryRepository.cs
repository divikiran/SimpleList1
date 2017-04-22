using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleList
{
	public class CountryRepository : Repository<CountryEntity>, ICountryRepository
	{
		public async Task<List<Country>> GetData(string extraParams = "")
		{
			var restClient = new DataService<List<Country>>();
			List<Country> list = await restClient.GetAsync(Constant.CountriesUrl);

			if (list == null)
				return null;

			var entities = ConvertToEntities(list);

			if (entities?.Count > 0)
			{
				DropAndCreateTable();
			}
			InsertAll(entities);
			return list;
		}

		public List<CountryEntity> ConvertToEntities(List<Country> models)
		{
			List<CountryEntity> entities = new List<CountryEntity>();
			foreach (var item in models)
			{
				var obj = new CountryEntity()
				{
					Code = item.Code,
					Name = item.Name,

				};
				entities.Add(obj);
			}
			return entities;
		}

		public List<Country> ConvertToModels(List<CountryEntity> entities)
		{
			List<Country> models = new List<Country>();
			foreach (var item in entities)
			{
				var obj = new Country()
				{
					Code = item.Code,
					Name = item.Name,
				};
				models.Add(obj);
			}
			return models;
		}
	}
}
