namespace AppSetting.ViewModels;

public class AppSettingViewModel : BaseViewModel
{
	public AppSettingViewModel()
	{
		ReadCommand.Execute(null);
	}

	string? appThemes;
	public string? AppThemes
	{
		get => appThemes;
		set
		{
			if (appThemes == value) { return; }
			appThemes = value;
			OnPropertyChanged();
			UpdateCommand.Execute(new string[] { nameof(AppThemes), value ?? GlobalVariables.defaultTheme });
		}
	}

	bool playNotifSound;
	public bool PlayNotifSound
	{
		get { return playNotifSound; }
		set
		{
			if (playNotifSound == value) { return; }
			playNotifSound = value;
			OnPropertyChanged();
			string cPlayNotifSound = value ? "0" : "1"; // ?? GlobalVariables.defaultPlayNotifSound;
			UpdateCommand.Execute(new string[] { nameof(PlayNotifSound), cPlayNotifSound });
		}
	}

	string? notifAudioFile;
	public string? NotifAudioFile
	{
		get { return notifAudioFile; }
		set
		{
			if (notifAudioFile == value) { return; }
			notifAudioFile = value;
			OnPropertyChanged();
			UpdateCommand.Execute(new string[] { nameof(NotifAudioFile), value ?? GlobalVariables.defaultNotifAudioFile });
		}
	}

	string? notifDisplayDuration;
	public string? NotifDisplayDuration
	{
		get { return notifDisplayDuration; }
		set
		{
			if (notifDisplayDuration == value) { return; }
			notifDisplayDuration = value;
			OnPropertyChanged();
			UpdateCommand.Execute(new string[] { nameof(NotifDisplayDuration), value ?? GlobalVariables.defaultNotifDisplayDuration });
		}
	}

	public Command ReadCommand => new(async () =>
	{
		try
		{
			IsBusy = true;
			var list = await AppSettingService.ReadAllSetting();
			foreach (AppSettingModel item in list)
			{
				switch (item.SettingKey)
				{
					case nameof(AppThemes):
						AppThemes = item.SettingValue;
						break;
					case nameof(PlayNotifSound):
						PlayNotifSound = item.SettingValue == "0";
						break;
					case nameof(NotifAudioFile):
						NotifAudioFile = item.SettingValue;
						break;
					case nameof(NotifDisplayDuration):
						NotifDisplayDuration = item.SettingValue;
						break;
				}
			}
		}
		finally
		{
			IsBusy = false;
		}
	}, () => IsNotBusy);

	public Command UpdateCommand => new Command<string[]>(async (str) =>
	{
		try
		{
			IsBusy = true;
			AppSettingService.WriteSettingValueByKey(str[0], str[1]);
			var list = await AppSettingService.ReadAllSetting();
			if (str[0] == nameof(AppThemes)) { AppThemeService.LoadTheme(AppThemes); }
		}
		finally
		{
			IsBusy = false;
		}
	}, (str) => IsNotBusy);

	public Command ResetCommand => new(async () =>
	{
		try
		{
			IsBusy = true;
			bool userResponse = await Shell.Current.DisplayAlert("Alert:", "Reset app setting to default cannot be reversed.\nDo you want to continue?", "Yes", "No");
			if (userResponse)
			{
				File.WriteAllText(AppSettingService.appSettingFile, AppSettingService.defaultSetting);
				ReadCommand.Execute(null);
				ShowNotificationCommand.Execute("Reset to default app setting completed.");
			}
		}
		finally
		{
			IsBusy = false;
		}
	}, () => IsNotBusy);
}
