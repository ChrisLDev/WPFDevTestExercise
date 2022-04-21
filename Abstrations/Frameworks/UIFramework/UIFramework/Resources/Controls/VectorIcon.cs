using System.Windows;
using System.Windows.Controls;

namespace UIFramework
{
	public class VectorIcon : Control
	{
		public static readonly DependencyProperty IconProperty =
		   DependencyProperty.Register("Icon", typeof(string), typeof(VectorIcon),
			   new PropertyMetadata(""));

		static VectorIcon()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(VectorIcon),
				new FrameworkPropertyMetadata(typeof(VectorIcon)));
		}

		public string Icon
		{
			get { return (string)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}
	}
}
