namespace AppSetting.Views.Pages;

public partial class AppSettingPage : ContentPage
{
	public AppSettingPage()
	{
		InitializeComponent();
	}

	protected override bool OnBackButtonPressed()
	{
		return true;
	}
}