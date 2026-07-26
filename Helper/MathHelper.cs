namespace GameBalance.Helper
{
    public static class MathHelper
    {
        #region Conversion
        private const long BytesPerKilobyte = 1024L;
        private const long BytesPerMegabyte = BytesPerKilobyte * BytesPerKilobyte;
        private const long BytesPerGigabyte = BytesPerMegabyte * BytesPerKilobyte;

        public static long GigabytesToBytes(double gigabytes)
        {
            if (double.IsNaN(gigabytes) || double.IsInfinity(gigabytes))
                throw new ArgumentOutOfRangeException(nameof(gigabytes), "Gigabytes must be a valid number");

            if (gigabytes < 0)
                throw new ArgumentOutOfRangeException(nameof(gigabytes), "Gigabytes cannot be negative");

            return checked((long)(gigabytes * BytesPerGigabyte));
        }

        public static long MegabytesToBytes(double megabytes)
        {
            if (double.IsNaN(megabytes) || double.IsInfinity(megabytes))
                throw new ArgumentOutOfRangeException(nameof(megabytes), "Megabytes must be a valid number");

            if (megabytes < 0)
                throw new ArgumentOutOfRangeException(nameof(megabytes), "Megabytes cannot be negative");

            return checked((long)(megabytes * BytesPerMegabyte));
        }
        #endregion

        #region FormatBytes
        public static string FormatBytes(ulong? bytes) => bytes is null ? "Unknown" : FormatBytes((double)bytes.Value);
        public static string FormatBytes(long bytes) => FormatBytes((float)bytes);
        public static string FormatBytes(double bytes)
        {
            string[] units = ["B", "KB", "MB", "GB", "TB"];

            var unitIndex = 0;

            if (bytes <= 0)
                return $"Empty";

            while (bytes >= 1024 && unitIndex < units.Length - 1)
            {
                bytes /= 1024;
                unitIndex++;
            }

            return $"{FormatUnitValue(bytes)} {units[unitIndex]}";
        }

        private static string FormatUnitValue(double value)
        {
            var nearestWholeNumber = Math.Round(value);
            var distanceFromWholeNumber = Math.Abs(value - nearestWholeNumber);

            // If the value is very close to a whole number, show it as whole.
            if (distanceFromWholeNumber <= 0.05)
                return nearestWholeNumber.ToString("0");

            return value.ToString("0.##");
        }
        #endregion

        public static int ToPercentageInt(double value, double max)
        {
            var percent = ToPercentage(value, max);

            return (int)Math.Round(percent);
        }
        public static double ToPercentage(double value, double max)
        {
            if (max == 0)
                return 0;

            var percent = (value / max) * 100.0;

            return Math.Clamp(percent, 0, 100);
        }
    }
}
