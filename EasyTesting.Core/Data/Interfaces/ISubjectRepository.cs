using EasyTesting.Core.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTesting.Core.Data
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllSubjectsAsync(int teacherId);
        Task<Subject?> FindSubjectByIdAsync(int teacherId, int id);
        Task AddSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(int teacherId, int id);
    }
}
