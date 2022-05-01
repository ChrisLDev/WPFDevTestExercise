using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace UIFramework
{
    public class DateRangeRule : ValidationRule
    {
		/// <summary>
		/// </summary>
		/// <param name="value"></param>
		/// <param name="cultureInfo"></param>
		/// <returns></returns>
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			var dateTime = new DateTime();

			try
			{
				if (((string)value).Length > 0)
				{
					dateTime = DateTime.Parse((string)value);
				}
			}
			catch (Exception)
			{
				return new ValidationResult(false, null);
			}

			if ((dateTime < DatePicker.SqlMinDateTime) || (dateTime > DatePicker.SqlMaxDateTime))
			{
				return new ValidationResult(false, null);
			}

			return new ValidationResult(true, null);
		}
	}

	/// <summary>
	/// Interaction logic for DatePicker.xaml
	/// </summary>
	public partial class DatePicker : UserControl
	{
		/// <summary>
		/// The dependency property for date time
		/// </summary>
		public static readonly DependencyProperty DateTimeProperty =
			 DependencyProperty.Register("DateTime", typeof(DateTime), typeof(DatePicker), new PropertyMetadata(DateTime.Today));

		/// <summary>
		/// The dependency property for max date time
		/// </summary>
		public static readonly DependencyProperty MaxDateTimeProperty =
			 DependencyProperty.Register("MaxDateTime", typeof(DateTime?), typeof(DatePicker), new PropertyMetadata(null));

		/// <summary>
		/// The dependency property for min date time
		/// </summary>
		public static readonly DependencyProperty MinDateTimeProperty =
			 DependencyProperty.Register("MinDateTime", typeof(DateTime?), typeof(DatePicker), new PropertyMetadata(null));

		/// <summary>
		/// Represents the maximum valid date value for a SqlDateTime structure.
		/// </summary>
		internal static readonly DateTime SqlMaxDateTime = Convert.ToDateTime("December 31, 9999");

		/// <summary>
		/// Represents the minimum valid date value for a SqlDateTime structure.
		/// </summary>
		internal static readonly DateTime SqlMinDateTime = Convert.ToDateTime("January 1, 1753");

		/// <summary>
		/// DateTimePicker control
		/// </summary>
		public DatePicker()
		{
			InitializeComponent();

			CalendarButton.ToolTip = "@@Date";
		}

		/// <summary>
		/// The date time property used for binding
		/// </summary>
		public DateTime DateTime
		{
			get { return (DateTime)GetValue(DateTimeProperty); }
			set { SetValue(DateTimeProperty, value); }
		}

		/// <summary>
		/// The maximum date time property used for binding
		/// </summary>
		public DateTime? MaxDateTime
		{
			get
			{
				var maxDateTimeValue = (DateTime?)GetValue(MaxDateTimeProperty);

				if (!maxDateTimeValue.HasValue)
				{
					return SqlMaxDateTime;
				}

				return maxDateTimeValue > SqlMaxDateTime ? SqlMaxDateTime : maxDateTimeValue;
			}
			set { SetValue(MaxDateTimeProperty, value); }
		}

		/// <summary>
		/// The minimum date time property used for binding
		/// </summary>
		public DateTime? MinDateTime
		{
			get
			{
				var minDateTimeValue = (DateTime?)GetValue(MinDateTimeProperty);

				if (!minDateTimeValue.HasValue)
				{
					return SqlMinDateTime;
				}

				return minDateTimeValue < SqlMinDateTime ? SqlMinDateTime : minDateTimeValue;
			}
			set { SetValue(MinDateTimeProperty, value); }
		}

		private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
		{
			if (CalendarPopup.IsOpen)
			{
				var newdate = (DateTime)e.AddedItems[0];
				if (MinDateTime.HasValue && newdate < MinDateTime.Value)
				{
					DateTime = MinDateTime.Value;
				}
				else
				{
					if (MaxDateTime.HasValue && newdate.Add(DateTime.TimeOfDay) > MaxDateTime.Value)
					{
						DateTime = MaxDateTime.Value;
					}
					else
					{
						DateTime removedDate = (DateTime)e.RemovedItems[0];
						DateTime = new DateTime(newdate.Year, newdate.Month, newdate.Day, DateTime.Hour, DateTime.Minute,
							DateTime.Second);
					}
				}
				CalendarPopup.IsOpen = false;
			}
		}

		private void CalendarButton_Click(object sender, RoutedEventArgs e)
		{
			PopupCalender.DisplayDate = DateTime;
			CalendarPopup.IsOpen = true;
			
		}
	}
}
