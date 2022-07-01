using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EmployeeServices.Wpf.ViewModels
{
	public class Employee : ViewModelBase, IDataErrorInfo
	{
		const string theEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
							   + "@"
							   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
		private int _id;
		public int Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		}

		private string _name;


		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		private string _email;


		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				_email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		private string _gender;
		public string Gender
		{
			get
			{
				return _gender;
			}
			set
			{
				_gender = value;
				OnPropertyChanged(nameof(Gender));
			}
		}

		private string _status;
		public string Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
				OnPropertyChanged(nameof(Status));
			}
		}

		private string _textTofilter;
		public string TextToFilter
		{
			get
			{
				return _textTofilter;
			}
			set
			{
				_textTofilter = value;
				OnPropertyChanged(nameof(TextToFilter));
			}
		}

		public bool IsValid
		{
			get
			{
				return (!string.IsNullOrEmpty(Name.Trim())) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Gender) && !string.IsNullOrEmpty(Status) && Regex.IsMatch(Email, theEmailPattern);
			}
		}

		private Pagination _pagination;
        public Pagination Pagination { get { return _pagination; } set { _pagination = value; OnPropertyChanged(nameof(Pagination)); } }

        
		private ObservableCollection<Employee> _employeeRecords;
		public ObservableCollection<Employee> EmployeeRecords
		{
			get
			{
				return _employeeRecords;
			}
			set
			{
				_employeeRecords = value;
				OnPropertyChanged(nameof(EmployeeRecords));
			}
		}

		public string Error
		{
			get
			{
				return null;
			}
		}

		public string this[string columnName]
		{
			get
			{
				string result = string.Empty;

				switch (columnName)
				{
					case "Name":
						if (string.IsNullOrEmpty(Name))
							result = "Name is required";
						break;

					case "Email":
						if (string.IsNullOrEmpty(Email))
							result = "Email is required";
						else if (!Regex.IsMatch(Email, theEmailPattern))
							result = "Enter a valid email";
						break;

				}
				return result;
			}
		}
	}
}
