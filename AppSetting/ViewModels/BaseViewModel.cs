namespace AppSetting.ViewModels;

public class BaseViewModel : NotifyPropertyChanged
{
	bool isBusy = false;
	public bool IsBusy
	{
		get => isBusy;
		set
		{
			if (isBusy == value) { return; }
			isBusy = value;
			OnPropertyChanged();
			OnPropertyChanged(nameof(IsNotBusy));
		}
	}
	public bool IsNotBusy => !IsBusy;

	public static string CurrentPage { get; set; } = string.Empty;
	public Command GoToPageCommand => new Command<string>(async (page) =>
	{
		try
		{
			IsBusy = true;
			if (CurrentPage != page)
			{
				var pageType = Type.GetType(GlobalVariables.pageFolder + page);
				if (pageType != null)
				{
					await Shell.Current.GoToAsync(page, false);
					CurrentPage = page;
				}
				else
				{
					await Shell.Current.DisplayAlert("Error:", $"{page} - Not available", "OK");
				}
			}
		}
		finally
		{
			IsBusy = false;
		}
	}, (page) => IsNotBusy);

	public Command GoBackToPageCommand => new Command<string>(async (page) =>
	{
		try
		{
			IsBusy = true;
			await Shell.Current.GoToAsync(page, true);
			CurrentPage = string.Empty;
		}
		finally
		{
			IsBusy = false;
		}
	}, (page) => IsNotBusy);

	#region Notification
	CancellationTokenSource? cts;

	string notifText = string.Empty;
	public string NotifText
	{
		get => notifText;
		set
		{
			if (notifText == value) { return; }
			notifText = value;
			OnPropertyChanged();
			OnPropertyChanged(nameof(IsNotifVisible));
		}
	}
	public bool IsNotifVisible => !string.IsNullOrWhiteSpace(NotifText);

	public Command ShowNotificationCommand => new Command<string>(async (str) =>
	{
		try
		{
			IsBusy = true;
			cts = new CancellationTokenSource();

			if (!string.IsNullOrWhiteSpace(str))
			{
				NotifText = str;
				var list = await AppSettingService.ReadAllSetting();
				if (list.Count > 0)
				{
					AppSettingModel? isPlaySound = list.FirstOrDefault(a => a.SettingKey == "PlayNotifSound");
					AppSettingModel? soundFile = list.FirstOrDefault(a => a.SettingKey == "NotifAudioFile");
					if (isPlaySound?.SettingValue == "0")
					{
						PlatformsService.PlayAudioFile(soundFile?.SettingValue + ".wav");  //Play sound
					}
				}

				AppSettingModel? soundDuration = list.FirstOrDefault(a => a.SettingKey == "NotifDisplayDuration");
				var b = int.TryParse(soundDuration?.SettingValue, out int sec); //check value is numeric
				await DelayTask(sec, cts.Token);
			}
		}
		finally
		{
			NotifText = string.Empty;
			cts = null;
			IsBusy = false;
		}
	}, (str) => IsNotBusy);

	public async Task DelayTask(int sec, CancellationToken token = default)
	{
		try
		{
			token.ThrowIfCancellationRequested();
			await Task.Delay(TimeSpan.FromSeconds(sec), token); //wait for n seconds
		}
		catch (TaskCanceledException) { }
		finally
		{
			HideNotificationCommand.Execute(null);
		}
	}

	public Command HideNotificationCommand => new(() =>
	{
		cts?.Cancel();
		//cts?.Dispose();
	});
	#endregion
}
