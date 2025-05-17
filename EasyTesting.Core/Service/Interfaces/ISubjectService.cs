using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;
using Microsoft.AspNetCore.Mvc;

namespace EasyTesting.Core.Service
{
    public interface ISubjectService
    {
        Task<PagedResult<SubjectDTO>> GetAllSubjectsAsync(QueryParameters parameters, int teacherId);
        Task<SubjectDTO?> FindSubjectByIdAsync(int teacherId, int id);
        Task AddSubjectAsync(int teacherId, CreateSubjectDTO subject);
        Task DeleteSubjectAsync(int teacherId, int id);
    }
}
