namespace AppSetting.Services.PlatformsCompilation;

public class PlatformHandler
{
	public static void InitializeCustomHandler()
	{
		Color transparentColor = Colors.Transparent;

		Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
		{
#if __ANDROID__
			handler.PlatformView.SetPadding(20, 0, 0, 0);
			handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf((new Android.Graphics.Color(transparentColor.ToInt())));
#elif IOS
			//
#endif
		});

		Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(Editor), (handler, view) =>
		{
#if __ANDROID__
			handler.PlatformView.SetPadding(20, 0, 0, 0);
			handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf((new Android.Graphics.Color(transparentColor.ToInt())));
#elif IOS
			//
#endif
		});

		Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(Picker), (handler, view) =>
		{
#if __ANDROID__
			handler.PlatformView.SetPadding(0, 0, 0, 0);
			handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf((new Android.Graphics.Color(transparentColor.ToInt())));
#elif IOS
			//
#endif
		});

		Microsoft.Maui.Handlers.SwitchHandler.Mapper.AppendToMapping(nameof(Switch), (handler, view) =>
		{
#if __ANDROID__
			handler.PlatformView.SetPadding(0, 0, 0, 0);
#elif IOS
			//
#endif
		});

		Microsoft.Maui.Handlers.ToolbarHandler.Mapper.AppendToMapping(nameof(Toolbar), (handler, view) =>
		{
#if __ANDROID__
			handler.PlatformView.SetContentInsetsRelative(0, 0);
#elif IOS
			//
#endif
		});
	}
}
