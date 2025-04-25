using System.Xml.Serialization;

namespace EasyTesting.Core.Models.TestXmlModels
{
    public class QuestionXmlModel
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlElement]
        public required string Text { get; set; }

        [XmlArray("AnswerOptions")]
        [XmlArrayItem("AnswerOption")]
        public List<AnswerOptionXmlModel> AnswerOptions { get; set; } = new();
    }
}
