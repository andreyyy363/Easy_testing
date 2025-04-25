using EasyTesting.Core.Models.Entity;

namespace EasyTesting.Core.Service
{
    public interface ITestGenerator
    {
        string GenerateTest(Test test);
    }
}
