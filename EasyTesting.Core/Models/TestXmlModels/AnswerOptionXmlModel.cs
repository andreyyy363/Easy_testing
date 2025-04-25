using System.Xml.Serialization;

namespace EasyTesting.Core.Models.TestXmlModels
{
    public class AnswerOptionXmlModel
    {
        [XmlAttribute]
        public int Id { get; set; }
        public required string Text { get; set; }
    }

}
