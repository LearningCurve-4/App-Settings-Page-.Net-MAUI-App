namespace AppSetting.Helpers.Converters;

public class ToggleConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value == null && parameter == null) { return null; }
		string s = (string?)parameter!;
		string[] substrings = s.Split('_');
		if (substrings.Length != 3) { return null; }

		Shell.Current.Resources.TryGetValue(substrings[0], out object on);
		Shell.Current.Resources.TryGetValue(substrings[1], out object off);

		switch (substrings[2])
		{
			case "C":  //Toggle Color by Resource key
				return (bool)value! ? (Color)on : (Color)off;
			case "S":  //Toggle Image Source
				return (bool)value! ? (string)on : (string)off;
			case "T":  //Toggle Text
				return (bool)value! ? substrings[0] : substrings[1];
			default:
				break;
		}
		return null;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return null;
	}
}
