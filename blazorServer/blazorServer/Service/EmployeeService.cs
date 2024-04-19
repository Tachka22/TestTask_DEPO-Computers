using blazorServer.Models;
using Microsoft.EntityFrameworkCore;

namespace blazorServer.Service
{
    /// <summary>
    /// Сервисы работы с таблицей "Сотрудники"
    /// </summary>
    public class EmployeeService
    {
        private readonly AppDbContext _context;
        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Создать одного сотрудника.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Вывести всех сотрудников.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.Include(u => u.Company).ToListAsync();
        }
        /// <summary>
        /// Создать несколько сотрудников.
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        public async Task<bool> CreateRangeAsync(List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                var com = await _context.Companies.FirstOrDefaultAsync(x => x.Name == employee.Company.Name.ToString());
                var emp = await _context.Employees.FirstOrDefaultAsync(x => x.PassportNumber == employee.PassportNumber.ToString());
                //проверка существования компании в которую требуется занести сотрудника
                //проверка отсутсвия нового сотрудника(по паспорту) в БД
                if (com != null && (emp == null ||
                    emp!.PassportSeries != employee.PassportSeries))
                {
                    employee.Company = com;
                    await _context.Employees.AddAsync(employee);
                }
            }
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
