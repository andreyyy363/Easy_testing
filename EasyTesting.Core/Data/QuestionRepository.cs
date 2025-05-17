using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;
using EasyTesting.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace EasyTesting.Core.Data
{
    public class QuestionRepository: IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddQuestionAsync(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task<Question?> FindQuestionByIdAsync(int teacherId, int id)
        {
            return await _context.Questions.Include(q => q.Subject).Include(q => q.AnswerOptions)
                                           .FirstOrDefaultAsync(q => q.Id == id && q.CreatedById == teacherId);
        }

        public async Task<(IEnumerable<Question>, int Total)> GetQuestionsBySubjectIdAsync(QueryParameters parameters, int teacherId, int subjectId)
        {
            var query = _context.Questions.Where(q => q.SubjectId == subjectId && q.CreatedById == teacherId);
            var total = await query.CountAsync();
            return (await query.Include(q => q.Subject).Include(q => q.AnswerOptions) 
                                           .ApplyPaging(parameters)
                                           .ToListAsync(), total);
        }

        public async Task<IEnumerable<Question>> GetQuestionsBySubjectIdAsync(int teacherId, int subjectId)
        {
            return await _context.Questions.Include(q => q.Subject).Include(q => q.AnswerOptions)
                                           .Where(q => q.SubjectId == subjectId && q.CreatedById == teacherId)
                                           .ToListAsync();
        }

        public async Task<(IEnumerable<Question>, int Total)> GetAllQuestionsAsync(QueryParameters parameters, int teacherId)
        {
            var query = _context.Questions.Where(q => q.CreatedById == teacherId);
            var total = await query.CountAsync();
            return (await query.Include(q => q.Subject).Include(q => q.AnswerOptions)     
                                           .ApplyPaging(parameters)
                                           .ToListAsync(), total);
        }

        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task DeleteQuestionAsync(int teacherId, int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id && q.CreatedById == teacherId);
            
            if (question == null || question.TestId != null)
            {
                throw new ArgumentException("Question can not be deleted. Either not found or used in a test.");
            }
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}
