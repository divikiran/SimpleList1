using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleList
{
	public interface ICountryRepository : IRepository<CountryEntity>
	{
		List<Country> ConvertToModels(List<CountryEntity> countriesEntity);
		List<CountryEntity> ConvertToEntities(List<Country> countriesModel);
		Task<List<Country>> GetData(string extraParams = "");
	}
}
