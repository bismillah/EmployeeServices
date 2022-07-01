using EmployeeServices.Domain.Services;
using EmployeeServices.Wpf.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.Wpf.ViewModels
{
    public class MainViewModel
    {
        IEmployeeViewModelProvider employeeViewModelProvider;
        public MainViewModel(IEmployeeViewModelProvider employeeViewModelProvider)
        {
            if(employeeViewModelProvider == null)
                throw new ArgumentNullException(nameof(employeeViewModelProvider));

            this.employeeViewModelProvider = employeeViewModelProvider;  
        }


    }
}
