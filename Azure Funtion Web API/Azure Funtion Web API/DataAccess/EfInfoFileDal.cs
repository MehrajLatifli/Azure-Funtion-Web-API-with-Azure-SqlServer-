using Azure_Funtion_Web_API.Models;

namespace Azure_Funtion_Web_API.DataAccess
{
    public class EfInfoFileDal : EF_EntityRepositoryBase<InfoFiles, InfoFileContext>, IInfoFileDal
    {
        public List<InfoFiles> Get()
        {
            using (var context = new InfoFileContext())
            {
                var infofile = context.InfoFiles;


                return infofile.ToList();
            }
        }
    }
}
