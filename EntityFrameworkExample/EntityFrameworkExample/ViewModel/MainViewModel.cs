using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using EntityFrameworkExample.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;


namespace EntityFrameworkExample.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		public RelayCommand AddDepartmentCommand { get; private set; }
		public RelayCommand RemoveDepartmentCommand { get; private set; }
		public RelayCommand AddEmployeeCommand { get; private set; }
		public RelayCommand RemoveEmployeeCommand { get; private set; }

		private Department _SelectedDept;
		private Employee _SelectedEmp;
		private string _DeptName;
		private string _EmptName;

		public ObservableCollection<Department> Depts
		{
			get;
			private set;
		}

		public Department SelectedDept
		{
			get
			{
				return _SelectedDept;
			}
			set
			{
				_SelectedDept = value;
				RaisePropertyChanged();
			}
		}

		public Employee SelectedEmp
		{
			get
			{
				return _SelectedEmp;
			}
			set
			{
				_SelectedEmp = value;
				RaisePropertyChanged();
			}
		}

		public string DeptName
		{
			get { return _DeptName; }
			set
			{
				_DeptName = value;
				RaisePropertyChanged();
			}
		}

		public string EmpName
		{
			get { return _EmptName; }
			set
			{
				_EmptName = value;
				RaisePropertyChanged();
			}
		}

		public MainViewModel()
		{
			AddDepartmentCommand = new RelayCommand(this.AddDepartment);
			RemoveDepartmentCommand = new RelayCommand(this.RemoveDepartment);
			AddEmployeeCommand = new RelayCommand(this.AddEmployee);
			RemoveEmployeeCommand = new RelayCommand(this.RemoveEmployee);

			using (var db = new CompanyContext())
			{
				db.Departments.Load();
				db.Employees.Load();
				Depts = new ObservableCollection<Department>(db.Departments);
			}
		}

		private void AddDepartment()
		{
			using (var db = new CompanyContext())
			{
				var Dept = new Department()
				{
					DeptName = DeptName
				};

				Depts.Add(Dept);
				db.Departments.Add(Dept);
				db.SaveChanges();
			}
		}

		private void RemoveDepartment()
		{
			using (var db = new CompanyContext())
			{
				var SelectedDept = this.SelectedDept;
				var query = from item in db.Departments
							where SelectedDept.DepartmentId == item.DepartmentId
							select item;

				var c = query.First();

				Depts.Remove(SelectedDept);
				db.Departments.Remove(c);
				db.SaveChanges();
			}
		}

		private void AddEmployee()
		{
			using (var db = new CompanyContext())
			{
				var Emp = new Employee()
				{
					Name = EmpName,
					Department = SelectedDept
				};

				SelectedDept.Employees.Add(Emp);
				db.Employees.Add(Emp);
				db.SaveChanges();
			}
		}

		private void RemoveEmployee()
		{
			using (var db = new CompanyContext())
			{
				var a = SelectedEmp;
				var query = from item in db.Employees
							where a.EmployeeId == item.EmployeeId
							select item;

				var c = query.First();

				SelectedDept.Employees.Remove(a);
				db.Employees.Remove(c);
				db.SaveChanges();

			}
		}			
	}
}
