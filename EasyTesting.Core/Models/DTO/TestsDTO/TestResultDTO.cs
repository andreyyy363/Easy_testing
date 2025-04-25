namespace EasyTesting.Core.Models.DTO
{
    /// <summary>
    /// Data Transfer Object representing the result of a test attempt.
    /// </summary>
    public class TestResultDTO
    {
        /// <summary>
        /// The score achieved by the user on the test.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The total possible score for the test.
        /// </summary>
        public int Total { get; set; }
    }
}
