namespace GameBalance.Core.Results
{
    public sealed class ProgressResult(string StatusText, int Percentage)
    {
        public string Status => StatusText;
        public int Percent => Percentage;
    }
}
