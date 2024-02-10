namespace AppSetting.Services;

public class AppThemeService
{
	public static void LoadTheme(string? theme)
	{
		if (string.IsNullOrEmpty(theme)) { return; }
		Collection<ResourceDictionary> mergedDictionaries = (Collection<ResourceDictionary>)Shell.Current.Resources.MergedDictionaries;
		if (mergedDictionaries != null)
		{
			mergedDictionaries.Clear();
			mergedDictionaries.Add(new CommonStyle());

			switch (theme)
			{
				case "Dark":
					mergedDictionaries.Add(new DarkTheme());
					break;
				case "Light":
					mergedDictionaries.Add(new LightTheme());
					break;
				case "Orange":
					mergedDictionaries.Add(new OrangeTheme());
					break;
				case "Purple":
					mergedDictionaries.Add(new PurpleTheme());
					break;
				default:
					mergedDictionaries.Add(new DefaultTheme());
					break;
			}

			Shell.Current.Resources.TryGetValue(GlobalVariables.hdrResourceKey, out object hdrBgColor); Color statusBarColor = (Color)hdrBgColor;
			Shell.Current.Resources.TryGetValue(GlobalVariables.ftrResourceKey, out object ftrBgColor); Color navigationBarColor = (Color)ftrBgColor;
			double brightnessStatusBarColor = statusBarColor.Red * .3 + statusBarColor.Green * .59 + statusBarColor.Blue * .11;
			double brightnessNavigationBarColor = navigationBarColor.Red * .3 + navigationBarColor.Green * .59 + navigationBarColor.Blue * .11;
			PlatformsService.SetStatusNavigationBarsColor(statusBarColor, brightnessStatusBarColor > 0.5, navigationBarColor, brightnessNavigationBarColor > 0.5);
		}
	}
}
