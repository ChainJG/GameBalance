using GameBalance.Framework.Core;
using System.IO;
using System.Text.Json;

namespace GameBalance.Framework.Settings
{
    public static class GameBalanceSettingsService
    {
        private static readonly string Path =
            System.IO.Path.Combine(
                Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
                    $"{GameBalanceContext.AppName}",
                    "setting.json");

        public static GameBalanceSettings Load()
        {
            if (!File.Exists(Path))
                return new GameBalanceSettings();

            return JsonSerializer.Deserialize<GameBalanceSettings>(File.ReadAllText(Path)) ?? new GameBalanceSettings();
        }

        public static void Save(GameBalanceSettings state)
        {
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Path)!);

            File.WriteAllText(
             Path,
             JsonSerializer.Serialize(state,
                 options: new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
