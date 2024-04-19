using blazorServer.Models;
using Microsoft.EntityFrameworkCore;

namespace blazorServer.Service
{
    /// <summary>
    /// Сервисы работы с таблицей "Company"
    /// </summary>
    public class CompanyService
    {
        private readonly AppDbContext _context;
        public CompanyService(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Добавить одну компанию.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Company company)
        {
            //Если добавляемая компания уже есть в БД то она НЕ будет повторно добавлена.
            var findCompany = _context.Companies.FirstOrDefaultAsync(x => x.Inn == company.Inn.ToString());

            if (findCompany == null)
                await _context.Companies.AddAsync(company);

            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Добавить несколько компаний.
        /// </summary>
        /// <param name="companies"></param>
        /// <returns></returns>
        public async Task<bool> CreateRangeAsync(IList<Company> companies)
        {
            //Если добавляемая компания уже есть в БД то она НЕ будет повторно добавлена.
            foreach (var item in companies)
            {
                var company = _context.Companies.FirstOrDefaultAsync(x => x.Inn == item.Inn.ToString());
                if (company == null)
                    await _context.Companies.AddAsync(item);
            }
            await _context.SaveChangesAsync();

            return true;
        }
        /// <summary>
        /// Получить список компаний.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }
    }
}
