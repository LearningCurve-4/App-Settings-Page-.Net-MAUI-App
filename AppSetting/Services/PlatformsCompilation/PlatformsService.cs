#if ANDROID
using Android.Media;
using Android.OS;
using Android.Views;
#elif IOS
//
#endif

namespace AppSetting.Services.PlatformsCompilation;

public class PlatformsService
{
	public static async void SetStatusNavigationBarsColor(Color setStatusBarColor, bool appearanceLightStatusBar, Color setNavigationBarColor, bool appearanceLightNavigationBar)
	{
#if ANDROID
		if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop) { return; }

		var activity = await Platform.WaitForActivityAsync();
		var window = activity.Window!;

		//this may not be necessary (but maybe for older than M)
		window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
		window.ClearFlags(WindowManagerFlags.TranslucentStatus);

		if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
		{
			AndroidX.Core.View.WindowCompat.GetInsetsController(window, window.DecorView).AppearanceLightStatusBars = appearanceLightStatusBar;
			AndroidX.Core.View.WindowCompat.GetInsetsController(window, window.DecorView).AppearanceLightNavigationBars = appearanceLightNavigationBar;
		}

		window.SetStatusBarColor(new Android.Graphics.Color(setStatusBarColor.ToInt()));
		window.SetNavigationBarColor(new Android.Graphics.Color(setNavigationBarColor.ToInt()));
#elif IOS
//
#endif
		await Task.Delay(0);
	}

	public static void PlayAudioFile(string fileName)
	{
#if ANDROID
		var player = new MediaPlayer();
		var fd = Android.App.Application.Context.Assets!.OpenFd(fileName);
		player.Prepared += (s, e) =>
		{
			player.Start();
		};
		player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
		player.Prepare();
#elif IOS
		//
#endif
	}
}
