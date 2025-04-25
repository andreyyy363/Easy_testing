
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.TestXmlModels;
using System.Xml.Serialization;

namespace EasyTesting.Core.Service
{
    public class XmlTestGenerator : ITestGenerator
    {
        public string GenerateTest(Test test)
        {

            var xmlSerializer = new XmlSerializer(typeof(TestXmlModel));
            var stringWriter = new StringWriter();

            var xmlModel = new TestXmlModel
            {
                Name = test.Name,
                SubjectId = test.SubjectId,
                Questions = test.Questions.Select(q => new QuestionXmlModel
                {
                    Id = q.Id,
                    Text = q.Text,
                    AnswerOptions = GetAnswerOptions(q.AnswerOptions)

                }).ToList()
            };

            xmlSerializer.Serialize(stringWriter, xmlModel);
            return stringWriter.ToString();
        }
        private List<AnswerOptionXmlModel> GetAnswerOptions(IEnumerable<AnswerOption> answerOptions)
        {
            return answerOptions.Select(a => new AnswerOptionXmlModel { Text = a.Text, Id = a.Id }).ToList();
        }
    }
}
