namespace AppSetting.Services;

public class AppSettingService
{
	public static readonly string appSettingFile = Path.Combine(FileSystem.AppDataDirectory, "appsetting.txt");
	public static readonly string defaultSetting = "AppThemes|Default,PlayNotifSound|0,NotifAudioFile|notify,NotifDisplayDuration|2";

	public AppSettingService()
	{
		if (!File.Exists(appSettingFile))
		{
			File.WriteAllText(appSettingFile, defaultSetting);
		}
	}

	public static Task<ObservableCollection<AppSettingModel>> ReadAllSetting()
	{
		var list = new ObservableCollection<AppSettingModel>(); list.Clear();
		string contents = File.ReadAllText(appSettingFile);
		string[] r = contents.Split(',');
		foreach (string i in r)
		{
			string[] v = i.Split('|');
			if (string.IsNullOrEmpty(v[0])) { continue; }
			list.Add(new AppSettingModel { SettingKey = v[0], SettingValue = v[1] });
		}
		return Task.FromResult(list);
	}

	public static Task<string> ReadSettingValueByKey(string settingKey)
	{
		string contents = File.ReadAllText(appSettingFile);
		string[] r = contents.Split(',');
		foreach (string i in r)
		{
			string[] v = i.Split('|');
			if (string.IsNullOrEmpty(v[0])) { continue; }
			if (v[0] == settingKey) { return Task.FromResult(v[1]); }
		}
		return Task.FromResult(string.Empty);
	}

	public static void WriteSettingValueByKey(string settingKey, string settingValue)
	{
		string contents = File.ReadAllText(appSettingFile);
		string[] r = contents.Split(',');
		var nr = new StringBuilder();

		foreach (string i in r)
		{
			string[] v = i.Split('|');
			if (string.IsNullOrEmpty(v[0])) { continue; }
			if (v[0] == settingKey) { v[1] = settingValue; }

			string? x;
			if (i == r.Last())
			{
				x = $"{v[0]}|{v[1]}";
			}
			else
			{
				x = $"{v[0]}|{v[1]},";
			}
			nr.Append(x);
		}

		File.WriteAllText(appSettingFile, nr.ToString());
	}

	public static List<string> AppThemeList { get; private set; } =
	[
		"Default",
		"Dark",
		"Light",
		"Orange",
		"Purple"
	];
}
