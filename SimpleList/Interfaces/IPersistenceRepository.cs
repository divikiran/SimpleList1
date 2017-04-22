using System;
namespace SimpleList
{
	public interface IPersistenceRepository : IRepository<PersistenceEntity>
	{
		PersistenceEntity GetByKey(string Key);
	}
}
