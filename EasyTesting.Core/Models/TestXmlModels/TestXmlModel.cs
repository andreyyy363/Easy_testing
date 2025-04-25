using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EasyTesting.Core.Models.TestXmlModels
{
    [XmlRoot("Test")]
    public class TestXmlModel
    {
        public required string Name { get; set; }
        public int SubjectId { get; set; }

        [XmlArray("Questions")]
        [XmlArrayItem("Question")]
        public List<QuestionXmlModel> Questions { get; set; } = new();
    }
}
