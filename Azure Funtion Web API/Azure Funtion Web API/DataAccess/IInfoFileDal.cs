using Azure_Funtion_Web_API.Models;

namespace Azure_Funtion_Web_API.DataAccess
{
    public interface IInfoFileDal : IEntityRepository<InfoFiles>
    {
        List<InfoFiles> Get();
    }
}
