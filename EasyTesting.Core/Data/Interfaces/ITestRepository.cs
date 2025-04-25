using EasyTesting.Core.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTesting.Core.Data
{
    public interface ITestRepository
    {
        Task AddTestAsync(Test test);
        Task<IEnumerable<Test>> GetAllTestAsync(int teacherId);
        Task<IEnumerable<Test>> GetTestsBySubjectIdAsync(int teacherId, int subjectId);
        Task<Test?> FindTestByIdAsync(int teacherId, int id);
        Task<Test?> FindTestByIdAsync(int id);
        Task DeleteTestAsync(int teacherId, int id);
    }
}
