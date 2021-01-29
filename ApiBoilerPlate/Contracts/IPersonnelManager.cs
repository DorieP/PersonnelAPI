using ApiBoilerPlate.Data;
using ApiBoilerPlate.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Contracts
{
    public interface IPersonnelManager : IRepository<Personnel>
    {
        Task<(IEnumerable<Personnel> Personnel, Pagination Pagination)> GetPersonnelAsync(UrlQueryParameters urlQueryParameters);
        
        //Add more class specific methods here when neccessary
    }
}
