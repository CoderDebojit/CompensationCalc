using CompensationCalc.Data;
using CompensationCalc.Model;
using System.Net;

namespace CompensationCalc.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly string _jsonDataFilePath = @".\Data\MockEmployeeTable.json";
        public EmployeeService() { 
            
        }

        public async Task<(Employee? Result, HttpStatusCode Status)> CreateEmployee(Employee employeeRequst)
        {
            var data = new DbAccess<Employee>();
            // Fetch Data            
            var list = data.ReadData(_jsonDataFilePath);
            var employee = list.FirstOrDefault(item => item.EmployeeCode == employeeRequst.EmployeeCode);
            if (employee == null)
            {
                list.Add(employeeRequst);
                data.WriteData(_jsonDataFilePath, list);
                return (employee, HttpStatusCode.Created);
            }

            return (null, HttpStatusCode.BadRequest);
        }

        public async Task<(EmployeeCompensation? Result, HttpStatusCode Status)> GetEmployeeCompensation(int employeeCode)
        {
            var data = new DbAccess<Employee>();
            // Fetch Data            
            var list = data.ReadData(@".\Data\MockEmployeeTable.json");
            // Filter the employee data based on employeeCode
            var employee = list.FirstOrDefault(item => item.EmployeeCode == employeeCode);
            if(employee != null)
            {
                // Main Logic
                var dearnessAllowance = (decimal)employee.BasicSalary * 0.4m;
                var conveyanceAllowance = Math.Min(dearnessAllowance * 0.1m, 250);

                var houseRentAllowance = Math.Max((decimal)employee.BasicSalary * 0.25m, 1500);
                var grossSalary = (decimal)employee.BasicSalary + dearnessAllowance + conveyanceAllowance + houseRentAllowance;

                var PT = grossSalary <= 3000 ? 100:
                    (grossSalary > 3000 && grossSalary <= 6000 ? 150: 200);

                return (
                    new EmployeeCompensation()
                    {
                        PT = PT,
                        TotalSalary = grossSalary - PT,
                        EmployeeCode = employee.EmployeeCode,
                        EmployeeName = employee.EmployeeName,
                        Gender = employee.Gender,
                        DateOfBirth = employee.DateOfBirth,
                        Department = employee.Department,
                        Designation = employee.Designation,
                        BasicSalary = employee.BasicSalary
                    },
                    HttpStatusCode.OK);
            }
            return (null, HttpStatusCode.NotFound);
        }

        public async Task<(IEnumerable<Employee>? Result, HttpStatusCode Status)> UpdateEmployee(Employee employeeRequst)
        {
            var data = new DbAccess<Employee>();
            // Fetch Data            
            var list = data.ReadData(_jsonDataFilePath);
            var employee = list.FirstOrDefault(item => item.EmployeeCode == employeeRequst.EmployeeCode);
            if (employee != null)
            {
                employee.EmployeeName = employeeRequst.EmployeeName;
                employee.Designation = employeeRequst.Designation;
                employee.BasicSalary = employeeRequst.BasicSalary;
                employee.Department = employeeRequst.Department;

                data.WriteData(_jsonDataFilePath, list);
                return (list, HttpStatusCode.OK);
            }

            return (Enumerable.Empty<Employee>(), HttpStatusCode.BadRequest);
        }
    }
}
