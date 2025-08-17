using Microsoft.EntityFrameworkCore;

namespace MvcFreelan.Models.Freelan
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public SqlEmployeeRepository(AppDbContext context)
        {
            this._context = context;
        }
        public Employee Add(Employee employee)
        {
            _context.Add(employee);
            return employee;
        }
        public Employee Delete(int id)
        {
            Employee employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return employee;
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees;
        }
        public Employee GetById(int id)
        {
            return _context.Employees.Find(id);
        }
        public Employee Update(Employee employeeChanges)
        {
            var emp = _context.Employees.Attach(employeeChanges);
            emp.State = EntityState.Modified;
            _context.SaveChanges();
            return employeeChanges;
        }
    }
}
