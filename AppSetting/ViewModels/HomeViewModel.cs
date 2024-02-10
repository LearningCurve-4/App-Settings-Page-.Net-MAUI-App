namespace AppSetting.ViewModels;

public class HomeViewModel : BaseViewModel
{
	public HomeViewModel()
	{
		LoadThemeCommand.Execute(null);
	}

	bool isSignin = false;
	public bool IsSignin
	{
		get => isSignin;
		set
		{
			if (isSignin == value) { return; }
			isSignin = value;
			OnPropertyChanged();
		}
	}

	public Command LoadThemeCommand => new(async () =>
	{
		try
		{
			IsBusy = true;
			AppThemeService.LoadTheme(await AppSettingService.ReadSettingValueByKey("AppThemes") ?? GlobalVariables.defaultTheme);
		}
		finally
		{
			IsBusy = false;
		}
	}, () => IsNotBusy);

	public Command SigninCommand => new(() =>
	{
		try
		{
			IsBusy = true;
			IsSignin = !IsSignin;

			if (IsSignin)
			{
				ShowNotificationCommand.Execute("You have Signed in.");
			}
			else
			{
				ShowNotificationCommand.Execute("You have Signed out.");
			}
		}
		finally
		{
			IsBusy = false;
		}
	}, () => IsNotBusy);
}
