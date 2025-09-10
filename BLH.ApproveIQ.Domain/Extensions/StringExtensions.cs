namespace BLH.ApproveIQ.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string TrimAndRemoveRepeatedIntermediateBlanks(this string stringToTrim)
        {
            var array = stringToTrim.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return string.Join(' ', array);
        }

        public static string AppendToURL(this string baseURL, params string[] segments)
        {
            return string.Join("/", new[] { baseURL.TrimEnd('/') }
                .Concat(segments.Select(s => s.Trim('/'))));
        }
    }
}
