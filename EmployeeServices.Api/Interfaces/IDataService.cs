using EmployeeServices.Api.Model;
using System.Threading.Tasks;

namespace EmployeeServices.Api.Interfaces
{
    public interface IDataService
    {
        Task<EmployeeModel> Get(int id);

        Task<EmployeeList> ReadAll();

        Task<EmployeeModel> Add(EmployeeModel entity);

        Task<bool> Delete(int id);

        Task<EmployeeModel> Update(int id, EmployeeModel entity);

        Task<EmployeeList> Search(string name);

        Task<EmployeeList> Search(int page);
    }
}
