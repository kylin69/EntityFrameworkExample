using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace EntityFrameworkExample.Model
{
	public class Department : ObservableObject
	{
		public int DepartmentId { get; set; }
		public string DeptName { get; set; }

		public ObservableCollection<Employee> Employees
		{
			get { return _Employees; }
			set
			{
				_Employees = value;
				RaisePropertyChanged();
			}
		}

		private ObservableCollection<Employee> _Employees;

		public Department() { Employees = new ObservableCollection<Employee>(); }
	}
}
