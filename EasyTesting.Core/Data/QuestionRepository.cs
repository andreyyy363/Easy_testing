using EasyTesting.Core.Models.Entity;
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

        public async Task<Question?> FindQuestionByIdAsync(int id)
        {
            return await _context.Questions.Include(q => q.Subject).Include(q => q.AnswerOptions)
                                           .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Question>> GetQuestionsBySubjectIdAsync(int subjectId)
        {
            return await _context.Questions.Include(q => q.Subject).Include(q => q.AnswerOptions)
                                            .Where(q => q.SubjectId == subjectId).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _context.Questions.Include(q => q.Subject).Include(q => q.AnswerOptions).ToListAsync();
        }

        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

    }
}
