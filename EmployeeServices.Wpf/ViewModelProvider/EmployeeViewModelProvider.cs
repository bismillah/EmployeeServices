using EmployeeServices.Domain.Enum;
using EmployeeServices.Domain.Model;
using EmployeeServices.Domain.ModelInterface;
using EmployeeServices.Domain.Services;
using EmployeeServices.Wpf.Interface;
using EmployeeServices.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeServices.Wpf.ViewModelProvider
{
    public class EmployeeViewModelProvider : IEmployeeViewModelProvider
    {
        private readonly IDataService dataService;

        public EmployeeViewModelProvider(IDataService dataService)
        {
            if (dataService == null)
                throw new ArgumentNullException(nameof(dataService));

            this.dataService = dataService;
        }

        public void DeleteEmployee(int id)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?", "Employee", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    this.dataService.Delete(id);
                    MessageBox.Show("Record successfully deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    //ReadAll();
                }
            }
        }

        public void SaveData(Employee Employee)
        {
            EmployeeModel _employeeEntityModel = new EmployeeModel();
            if (Employee != null)
            {
                _employeeEntityModel.Name = Employee.Name;
                _employeeEntityModel.Gender = Employee.Gender;
                _employeeEntityModel.Email = Employee.Email;
                _employeeEntityModel.Status = Employee.Status;

                try
                {
                    if (Employee.Id <= 0)
                    {
                        dataService.Add(_employeeEntityModel);
                        MessageBox.Show("New record successfully saved.");
                    }
                    else
                    {
                        _employeeEntityModel.Id = Employee.Id;
                        dataService.Update(Employee.Id, _employeeEntityModel);
                        MessageBox.Show("Record successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    //ReadAll();
                    //ResetData(Employee);
                }
            }
        }

        public async Task<Employee> EditData(int id)
        {
            var model = await dataService.Get(id);

            return new Employee
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Gender = model.Gender,
                Status = model.Status
            };
        }

        public async Task<List<IEmployeeModel>> ReadAll()
            => await dataService.ReadAll();


        public async Task<List<IEmployeeModel>> Search(string name)
            => await dataService.Search(name);
    }
}