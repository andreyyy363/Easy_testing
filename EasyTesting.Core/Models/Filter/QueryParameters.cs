namespace EasyTesting.Core.Models.Filter
{
    /// <summary>
    /// Query parameters for filtering, sorting and paginating results.
    /// </summary>
    public class QueryParameters
    {

        /// <summary>
        /// Number of records to skip from the start of the result set. Used for pagination.
        /// </summary>
        public int skip { get; set; } = 0;

        /// <summary>
        /// Maximum number of records to return in the result set. Default is 100.
        /// </summary>
        public int limit { get; set; } = 100;

        /// <summary>
        /// Field name by which to sort the results.
        /// </summary>
        // public string? SortBy { get; set; }

        /// <summary>
        /// Search query to filter results based on matching fields.
        /// </summary>
        // public string? Search { get; set; }
    }

}
