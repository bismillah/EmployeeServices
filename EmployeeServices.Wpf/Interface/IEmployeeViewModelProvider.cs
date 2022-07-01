using EmployeeServices.Domain.Model;
using EmployeeServices.Domain.ModelInterface;
using EmployeeServices.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.Wpf.Interface
{
    public interface IEmployeeViewModelProvider
    {
        void DeleteEmployee(int id);

        void SaveData(Employee Employee);

        Task<Employee> EditData(int id);

        Task<List<IEmployeeModel>> ReadAll();

        Task<List<IEmployeeModel>> Search(string name);
    }
}
