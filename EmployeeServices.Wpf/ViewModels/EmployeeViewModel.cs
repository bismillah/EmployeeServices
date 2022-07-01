using EmployeeServices.Api.Interfaces;
using EmployeeServices.Api.Model;
using EmployeeServices.Domain.Enum;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmployeeServices.Wpf.ViewModels
{
    public class EmployeeViewModel
    {
        private EmployeeModel employeeModel = null;
        public Employee Employee { get; set; }
        public Pagination Pagination { get; set; }

        private readonly IDataService dataService;

        public EmployeeViewModel(IDataService dataService)
        {
            if (dataService == null)
                throw new ArgumentNullException(nameof(dataService));

            this.dataService = dataService;
            this.InitalizeData();
        }

        #region Commands

        private ICommand _saveCommand;
        private ICommand _resetCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private ICommand _searchCommand;
        private ICommand _clearCommand;

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(param => ResetData(), null);

                return _resetCommand;
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                    _clearCommand = new RelayCommand(param => ClearData(), null);

                return _clearCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(param => SaveData(), param => Employee.IsValid);

                return _saveCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand(param => EditData((int)param), null);

                return _editCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(param => DeleteEmployee((int)param), null);

                return _deleteCommand;
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                    _searchCommand = new RelayCommand(param => Search(), null);

                return _searchCommand;
            }
        }

        private ICommand _previous;
        private ICommand _next;
        private ICommand _first;
        private ICommand _last;

        public ICommand PreviousCommand
        {
            get
            {
                if (_previous == null)
                    _previous = new RelayCommand(param => NewPage(Employee.Pagination.PageCount - 1), null);

                return _previous;
            }
        }

        public ICommand NextCommand
        {
            get
            {
                if (_next == null)
                    _next = new RelayCommand(param => NewPage(Employee.Pagination.PageCount + 1), null);

                return _next;
            }
        }

        public ICommand FirstCommand
        {
            get
            {
                if (_first == null)
                    _first = new RelayCommand(param => NewPage(1), null);

                return _first;
            }
        }

        public ICommand LastCommand
        {
            get
            {
                if (_last == null)
                    _last = new RelayCommand(param => NewPage(Employee.Pagination.TotalPages), null);

                return _last;
            }
        }

        private void NewPage(int? currentPage)
        {
            if (currentPage < 0)
                currentPage = 0;

            if (currentPage > Employee.Pagination.TotalPages)
                currentPage = Employee.Pagination.TotalPages;

            Employee.Pagination.CurrentPage = currentPage ?? default(int);
            Employee.Pagination.PageCount = Employee.Pagination.CurrentPage;

            this.Search(Employee.Pagination.PageCount);
        }

        #endregion

        private void InitalizeData()
        {
            employeeModel = new EmployeeModel();
            Employee = new Employee();
            this.ResetData();
            ReadAll();
        }

        private void ResetData()
        {
            Employee.Name = string.Empty;
            Employee.Id = 0;
            Employee.Email = string.Empty;
            Employee.Gender = Gender.male.ToString();
            Employee.Status = Status.active.ToString();
            Employee.TextToFilter = string.Empty;
        }

        private void ClearData()
        {
            this.ResetData();
            ReadAll();
        }

        private async void DeleteEmployee(int id)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?", "Employee", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    bool hasDeleted = await dataService.Delete(id);
                    if (hasDeleted)
                        MessageBox.Show("Record successfully deleted.");
                    else
                        MessageBox.Show("Record not deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    ResetData();
                    ReadAll();
                }
            }
        }

        private void SaveData()
        {
            if (Employee != null)
            {
                employeeModel.Name = Employee.Name;
                employeeModel.Gender = Employee.Gender;
                employeeModel.Email = Employee.Email;
                employeeModel.Status = Employee.Status;

                try
                {
                    if (Employee.Id <= 0)
                    {
                       var result= dataService.Add(employeeModel);
                        if(result!= null && result.Id >0)
                            MessageBox.Show("New record successfully saved.");
                    }
                    else
                    {
                        employeeModel.Id = Employee.Id;
                        dataService.Update(Employee.Id, employeeModel);
                        MessageBox.Show("Record successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    ReadAll();
                    ResetData();
                }
            }
        }

        private async void EditData(int id)
        {
            try
            {
                var model = await dataService.Get(id);
                if (model == null)
                    MessageBox.Show("Employee does not exists");

                Employee.Id = model.Id;
                Employee.Name = model.Name;
                Employee.Email = model.Email;
                Employee.Gender = model.Gender;
                Employee.Status = model.Status;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on fetching employee. " + ex.InnerException);
            }
        }

        private async void ReadAll()
        {
            Employee.EmployeeRecords = new ObservableCollection<Employee>();
            try
            {
                var employeeList = await dataService.ReadAll();
                this.SetData(employeeList);               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on reading all employees. " + ex.InnerException);           
            }
        }

        private async void Search()
        {
            if (string.IsNullOrEmpty(Employee.TextToFilter))
                MessageBox.Show("Please enter full name to search");

            Employee.EmployeeRecords = new ObservableCollection<Employee>();
            try
            {
                var employeeList = await dataService.Search(Employee.TextToFilter);
                this.SetData(employeeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on searcing employees records. " + ex.InnerException);
            }
        }

        private async void Search(int pageNumber)
        {
            Employee.EmployeeRecords = new ObservableCollection<Employee>();
            try
            {
                var employeeList = await dataService.Search(pageNumber);
                this.SetData(employeeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on searcing employees records. "+ ex.InnerException);
            }
        }

        private void SetData(EmployeeList employeeList)
        {
            foreach (EmployeeModel employee in employeeList.data)
                Employee.EmployeeRecords.Add(new Employee { Name = employee.Name, Email = employee.Email, Id = employee.Id, Gender = employee.Gender, Status = employee.Status });

            Employee.Pagination = new Pagination { CurrentPage = employeeList.meta.pagination.page, Limit = employeeList.meta.pagination.limit, TotalPages = employeeList.meta.pagination.pages, Total = employeeList.meta.pagination.total, PageCount = employeeList.meta.pagination.page };
        }
    }
}
