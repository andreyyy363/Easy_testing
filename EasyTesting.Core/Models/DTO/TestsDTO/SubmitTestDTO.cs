namespace EasyTesting.Core.Models.DTO
{
    /// <summary>
    /// DTO for submitting a test result.
    /// </summary>
    public class SubmitTestDTO
    {
        /// <summary>
        /// The ID of the test being submitted.
        /// </summary>
        public required int TestId { get; set; }

        /// <summary>
        /// The ID of the student who is submitting the test.
        /// </summary>
        public required int StudentId { get; set; }

        /// <summary>
        /// A dictionary of answers where the key is the question ID and the value is the selected answer option ID.
        /// </summary>
        public Dictionary<int, int> Answers { get; set; } = new();
    }
}
