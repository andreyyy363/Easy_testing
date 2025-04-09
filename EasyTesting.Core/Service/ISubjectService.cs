﻿using EasyTesting.Core.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTesting.Core.Service
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
        Task<Subject?> FindSubjectByIdAsync(int id);
        Task AddSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(int id);
    }
}
