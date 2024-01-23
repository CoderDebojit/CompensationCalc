using CompensationCalc.Model;
using System.Net;

namespace CompensationCalc.Service
{
    public interface IEmployeeService
    {
        public Task<(EmployeeCompensation? Result, HttpStatusCode Status)> GetEmployeeCompensation(int employeeCode);

        public Task<(Employee? Result, HttpStatusCode Status)> CreateEmployee(Employee employeeRequst);
        public Task<(IEnumerable<Employee>? Result, HttpStatusCode Status)> UpdateEmployee(Employee employeeRequst);
    }
}
