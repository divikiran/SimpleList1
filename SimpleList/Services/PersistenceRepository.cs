using System;
using System.Linq;

namespace SimpleList
{
public class PersistenceRepository : Repository<PersistenceEntity>, IPersistenceRepository
{
	public PersistenceEntity GetByKey(string Key)
	{
		var result = Connection.Query<PersistenceEntity>("select * from PersistenceEntity where Key = '" + Key + "'");
		return result?.FirstOrDefault();
	}	}
}
