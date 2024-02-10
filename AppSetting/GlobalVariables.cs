namespace AppSetting;

public class GlobalVariables : NotifyPropertyChanged
{
	public static string pageFolder = $"{AppInfo.Current.Name.Replace(" ",string.Empty)}.Views.Pages.";
	public const string hdrResourceKey = "HeaderBarFillColor";
	public const string ftrResourceKey = "FooterBarFillColor";
	public const string defaultTheme = "Default";
	public const string defaultPlayNotifSound = "0";
	public const string defaultNotifAudioFile = "notify";
	public const string defaultNotifDisplayDuration = "2";
	public const string aboutAppWebPage = "https://en.wikipedia.org/wiki/Mobile_app";
}
