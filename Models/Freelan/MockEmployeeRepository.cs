namespace MvcFreelan.Models.Freelan
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;

        public MockEmployeeRepository()
        {
            _employees = new List<Employee> {
      new Employee { EmployeeId = 1, EmployeeName = "Alice Smith", EmployeeDepartment = "HR", EmployeeEmail = "as@mail.com" },
      new Employee { EmployeeId = 2, EmployeeName = "Byan Chavez", EmployeeDepartment = "IT", EmployeeEmail = "bc@mail.com" },
      new Employee { EmployeeId = 3, EmployeeName = "Pedro Almodovar", EmployeeDepartment = "IT", EmployeeEmail = "pa@mail.com" },
            };
        }
        public Employee Add(Employee employee)
        {
            _employees.Add(employee);
            return employee;
        }
        public Employee Delete(int id)
        {
            _employees.RemoveAll(e => e.EmployeeId == id);
            return new Employee();
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employees;
        }
        public Employee GetById(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                employee = new Employee() { EmployeeId = 0, EmployeeName = "No existe" };
            }
            return employee;
        }
        public Employee Update(Employee employee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.EmployeeName = employee.EmployeeName;
                existingEmployee.EmployeeDepartment = employee.EmployeeDepartment;
                existingEmployee.EmployeeEmail = employee.EmployeeEmail;
                existingEmployee.EmployeeSalary = employee.EmployeeSalary;
            }
            return employee;
        }
    }
}
