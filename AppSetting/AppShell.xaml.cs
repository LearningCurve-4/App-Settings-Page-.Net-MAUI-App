namespace AppSetting;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		PlatformHandler.InitializeCustomHandler();
		InitializeRouting();
		InitializeAppFiles();
	}

	public void InitializeRouting()
	{
		Routing.RegisterRoute(nameof(AboutAppPage), typeof(AboutAppPage));
		Routing.RegisterRoute(nameof(AppSettingPage), typeof(AppSettingPage));
	}

	public static void InitializeAppFiles()
	{
		if (!File.Exists(AppSettingService.appSettingFile))
		{
			File.WriteAllText(AppSettingService.appSettingFile, AppSettingService.defaultSetting);
		}
	}
}
