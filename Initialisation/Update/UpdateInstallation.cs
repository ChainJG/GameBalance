using GameBalance.Core.Results;
using GameBalance.Helper;
using GameBalance.Infrastructure.Http;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace GameBalance.Initialisation.Update
{
    public class UpdateInstallation(UpdateReleaseInfo releaseInfo)
    {
        private readonly UpdateReleaseInfo _releaseInfo = releaseInfo;
        public async Task DownloadAndInstallAsync(IProgress<ProgressResult>? progress = default, CancellationToken cancellationToken = default)
        {
            string tempFile = Path.Combine(
                Path.GetTempPath(),
                $"GameBalance_Update_{_releaseInfo.Version}.exe");

            using (var response = await HttpClientProvider.Client.GetAsync(
                _releaseInfo.DownloadUrl,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                response.EnsureSuccessStatusCode();

                long? totalBytes = response.Content.Headers.ContentLength;

                await using var contentStream =
                    await response.Content.ReadAsStreamAsync(cancellationToken);

                await using var fileStream = new FileStream(
                    tempFile,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    8192,
                    useAsync: true);

                var buffer = new byte[8192];

                long totalRead = 0;
                int bytesRead;

                while ((bytesRead = await contentStream.ReadAsync(buffer, cancellationToken)) > 0)
                {
                    await fileStream.WriteAsync(
                        buffer.AsMemory(0, bytesRead),
                        cancellationToken);

                    totalRead += bytesRead;

                    if (totalBytes.HasValue && totalBytes.Value > 0)
                    {
                        //int percent = (int)((totalRead * 100L) / totalBytes.Value);
                        var percent = MathHelper.ToPercentageInt(totalRead, totalBytes.Value);

                        progress?.Report(
                            new ProgressResult(
                                $"Downloading update... {percent}%",
                                percent));
                    }
                }

                await fileStream.FlushAsync(cancellationToken);
            }

            progress?.Report(
                new ProgressResult(
                    "Launching installer...",
                    100));

            Process.Start(new ProcessStartInfo
            {
                FileName = tempFile,
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(tempFile)
            });

            System.Windows.Application.Current.Shutdown();
        }
    }
}
