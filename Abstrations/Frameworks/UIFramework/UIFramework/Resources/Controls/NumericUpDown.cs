using UIFramework.Resources.Events;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace UIFramework
{
	/// <summary>
	/// Generic number up down control
	/// </summary>
	/// <typeparam name="T">The type of numeric up down control</typeparam>
	public abstract class NumericUpDown<T> : TextBox
	{
		/// <summary>
		/// The auto format dependency property.
		/// </summary>
		public static readonly DependencyProperty AutoFormatProperty =
			DependencyProperty.Register(nameof(AutoFormat), typeof(bool), typeof(NumericUpDown<T>),
				new FrameworkPropertyMetadata(true));

		/// <summary>
		/// The increment dependency property.
		/// </summary>
		public static readonly DependencyProperty IncrementProperty =
			DependencyProperty.Register(nameof(Increment), typeof(T), typeof(NumericUpDown<T>),
				new PropertyMetadata(default(T)));

		/// <summary>
		/// The maximum value dependency property.
		/// </summary>
		public static readonly DependencyProperty MaxValueProperty =
			DependencyProperty.Register(nameof(MaxValue), typeof(T), typeof(NumericUpDown<T>),
				new PropertyMetadata(default(T)));

		/// <summary>
		/// The minimum value dependency property.
		/// </summary>
		public static readonly DependencyProperty MinValueProperty =
			DependencyProperty.Register(nameof(MinValue), typeof(T), typeof(NumericUpDown<T>),
				new PropertyMetadata(default(T)));

		/// <summary>
		/// The value dependency property.
		/// </summary>
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register(nameof(Value), typeof(T), typeof(NumericUpDown<T>),
				new FrameworkPropertyMetadata(default(T), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
					ValuePropertyChanged, CoerceValue));

		/// <summary>
		/// The mouse wheel enabled dependency property.
		/// </summary>
		public static readonly DependencyProperty MouseWheelEnabledProperty =
			DependencyProperty.Register(nameof(MouseWheelEnabled), typeof(bool), typeof(NumericUpDown<T>),
				new FrameworkPropertyMetadata(true));

		private RepeatButton _decrement;
		private RepeatButton _increment;
		private string _previousText;
		public event ValueChangedEventHandler<T> ValueChanged;

		/// <summary>
		/// Gets or sets the AutoFormat.
		/// </summary>
		public bool AutoFormat
		{
			get => (bool)GetValue(AutoFormatProperty);
			set => SetValue(AutoFormatProperty, value);
		}

		/// <summary>
		/// Gets or sets the Increment value.
		/// The amount the value should be incremented or decremented by each event.
		/// </summary>
		public T Increment
		{
			get => (T)GetValue(IncrementProperty);
			set => SetValue(IncrementProperty, value);
		}

		/// <summary>
		/// Gets or sets the MaxValue.
		/// The maximum defined boundary the value should be coerced to.
		/// </summary>
		public T MaxValue
		{
			get => (T)GetValue(MaxValueProperty);
			set => SetValue(MaxValueProperty, value);
		}

		/// <summary>
		/// Gets or sets the MinValue.
		/// The minimum defined boundary the value should be coerced to.
		/// </summary>
		public T MinValue
		{
			get => (T)GetValue(MinValueProperty);
			set => SetValue(MinValueProperty, value);
		}

		/// <summary>
		/// Gets or sets the MouseWheelEnabled flag.
		/// Indicating whether the control reacts to mouse wheel input or not.
		/// </summary>
		public bool MouseWheelEnabled
		{
			get => (bool)GetValue(MouseWheelEnabledProperty);
			set => SetValue(MouseWheelEnabledProperty, value);
		}

		/// <summary>
		///  Gets or sets the value of the <see cref="NumericUpDown{T}"/> control.
		/// </summary>
		public T Value
		{
			get => (T)GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}

		/// <summary>
		/// Gets the HasSign flag.
		/// Indicates whether the negative sign should be included if the <see cref="MinValue"/>
		/// is below 0.
		/// </summary>
		protected abstract bool HasSign
		{
			get;
		}

		/// <summary>
		/// Visual tree has been constructed so can grab templates and attach to events.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			
			MouseWheel += (s, e) =>
					OnMouseWheel(e.Delta);
				LostFocus += (s, e) =>
					OnLostFocus(Text);
				TextChanged += (s, e) =>
					OnTextChanged();

			_increment = GetTemplateChild("PART_Increment") as RepeatButton;
			if (_increment != null)
			{
				_increment.Click += (s, e) =>
					OnIncrement();
			}

			_decrement = GetTemplateChild("PART_Decrement") as RepeatButton;
			if (_decrement != null)
			{
				_decrement.Click += (s, e) =>
					OnDecrement();
			}

			SetText(Value.ToString(), AutoFormat);

			UpdateRepeatButtons(Value);
		}

		/// <summary>
		/// TextChanged handler for the TextBox part of the NumericUpDown control.<br/>
		/// If the text does not represent a valid number, do nothing (the string
		/// may be a work in progress by the user).<br/>
		/// If the text represents a number outside the minimum/maximum range, do nothing
		/// (again, the string may be a work in progress by the user).<br/>
		/// Finally, if the string represents an acceptable number, use it.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// FYI the TextChanged event used to be handled (it always called SetText()) but
		/// that caused confusing inconveniences when typing a value and the minimum was 
		/// greater than zero ... so it was removed. See bugs 58655 & 58667, and check-in
		/// 10902 in 5.4.0.<br/>
		/// The do-nothing paths of this method represent no change from current behaviour;
		/// this method only adds 'do something useful, if the value is usable'.
		/// </remarks>
		private void OnTextChanged()
		{
			var index = CaretIndex;

			if (!ValidateText(Text))
			{
				return;
			}

			var value = Parse(Text);
			if (Compare(value, MaxValue) <= 0 &&
				Compare(value, MinValue) >= 0)
			{
				SetText(Text, true);
			}

			CaretIndex = index;
		}

		/// <summary>
		/// Override if there are conditions where the text value should not be formatted
		/// right away.
		/// </summary>
		/// <param name="text">The current text.</param>
		/// <returns>True if the text can be formatted.</returns>
		protected virtual bool CanFormatText(string text)
		{
			return true;
		}

		/// <summary>
		/// Decrements the specified value by the specified increment and
		/// returns the updated value.
		/// </summary>
		/// <param name="current">The current value.</param>
		/// <param name="amount">The specified amount to increment by.</param>
		/// <returns>The updated value.</returns>
		protected abstract T DecrementValue(T current, T amount);

		/// <summary>
		/// This method is invoked when the value is required to be formatted
		/// to be displayed.
		/// </summary>
		/// <param name="value">The value to format.</param>
		/// <returns>The formatted value as text.</returns>
		protected abstract string FormatValue(T value);

		/// <summary>
		/// Increments the specified value by the specified increment and
		/// returns the updated value.
		/// </summary>
		/// <param name="current">The current value.</param>
		/// <param name="amount">The specified amount to increment by.</param>
		/// <returns>The updated value.</returns>
		protected abstract T IncrementValue(T current, T amount);

		/// <summary>
		/// Called when the value has changed.
		/// </summary>
		/// <param name="value">The new value.</param>
		protected virtual void OnValueChanged(T oldValue, T newValue)
		{
			UpdateText(_previousText, newValue.ToString());
		}

		/// <summary>
		/// Converts the text value to the type of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="value">The value to parse</param>
		/// <returns>The returned value as type of <typeparamref name="T"/></returns>
		protected abstract T Parse(string value);

		/// <summary>
		/// Converts the <typeparamref name="T"/> value to string.
		/// </summary>
		/// <param name="value">The value to parse</param>
		/// <returns>The returned value as type of <typeparamref name="T"/></returns>
		protected abstract string Parse(T value);

		/// <summary>
		/// Raises the value changed event with the specified <see cref="ValueChangedEventArgs{T}"/>
		/// </summary>
		/// <param name="args">The event arguments to raise the event with.</param>
		protected void RaiseValueChanged(ValueChangedEventArgs<T> args)
		{
			ValueChanged?.Invoke(this, args);
		}

		/// <summary>
		/// Checks to see if the text value should be reverted to the old value. If the check
		/// is invalid then the old value is restored in the TextBox template. If the check
		/// is valid the new value is updated in the TextBox template.
		/// </summary>
		/// <param name="value">The text value to check.</param>
		/// <returns>True if the text value is valid.</returns>
		protected abstract bool ValidateText(string value);

		/// <summary>
		/// This method is invoked when the value has changed and calls the <see cref="CoerceMinMax(T)"/>
		/// method on the <see cref="NumericUpDown{T}"/> with the specified baseValue.
		/// </summary>
		/// <param name="d">The <see cref="NumericUpDown{T}"/></param>
		/// <param name="basevalue">The value to coerce.</param>
		/// <returns>The updated value.</returns>
		private static object CoerceValue(DependencyObject d, object basevalue)
		{
			if (d is NumericUpDown<T> control)
			{
				return control.CoerceMinMax((T)basevalue);
			}

			return basevalue;
		}

		/// <summary>
		///  This method is invoked when the value has changed and calls the <see cref="OnValueChanged(T)"/>
		///  method on the <see cref="NumericUpDown{T}"/> control.
		/// </summary>
		/// <param name="d">The <see cref="NumericUpDown{T}"/></param>
		/// <param name="e">The event arguments.</param>
		private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is NumericUpDown<T> control)
			{
				control.OnValuePropertyChanged(e.OldValue, e.NewValue);
			}
		}

		/// <summary>
		/// Contains the specified <typeparamref name="T"/> value to be within the
		/// bounds of <see cref="MinValue"/> and <see cref="MaxValue"/>.
		/// </summary>
		/// <param name="value">The value to constrain.</param>
		/// <returns>The updated value.</returns>
		private T CoerceMinMax(T value)
		{
			if (!IsLoaded) return value;

			if (Compare(value, MaxValue) > 0)
			{
				return MaxValue;
			}
			if (Compare(value, MinValue) < 0)
			{
				return MinValue;
			}
			return value;
		}

		/// <summary>
		/// Performs a comparison between two objects of the same type and returns
		/// a value indicating whether one is less than, equal to, or greater than
		/// the other.
		/// </summary>
		/// <param name="a">The first object to compare.</param>
		/// <param name="b">The second object to compare.</param>
		/// <returns>The value indicating difference between the two objects.</returns>
		private int Compare(T a, T b) =>
			Comparer<T>.Default.Compare(a, b);

		/// <summary>
		/// This method is invoked whenever the value should be decremented.
		/// </summary>
		private void OnDecrement()
		{
			var current = Parse(Text);

			var value = DecrementValue(current, Increment);

			UpdateText(_previousText, Parse(value));
		}

		/// <summary>
		/// This method is invoked whenever the value should be incremented.
		/// </summary>
		private void OnIncrement()
		{
			var current = Parse(Text);

			var value = IncrementValue(current, Increment);
			
			UpdateText(_previousText, Parse(value));
		}

		/// <summary>
		/// This method is invoked when the focus is lost on the TextBox template.
		/// </summary>
		/// <param name="value">The text value of the text box.</param>
		private void OnLostFocus(string value)
		{
			SetText(value, true);
		}

		/// <summary>
		/// Invoked when the mouse wheel has changed position.
		/// </summary>
		/// <param name="sender">The dependency object.</param>
		/// <param name="e">The event arguments</param>
		private void OnMouseWheel(object sender, MouseWheelEventArgs e)
		{
			OnMouseWheel(e.Delta);
		}

		/// <summary>
		/// This method is invoked when the mouse wheel has moved while the cursor
		/// is positioned over the TextBox template.
		/// </summary>
		/// <param name="delta">Delta that indicates which way the mouse wheel was scrolled.</param>
		private void OnMouseWheel(int delta)
		{
			if (MouseWheelEnabled)
			{
				if (delta > 0)
				{
					OnIncrement();
				}
				else
				{
					OnDecrement();
				}
			}
		}

		/// <summary>
		///  Called when the value property has changed.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void OnValuePropertyChanged(object oldValue, object newValue)
		{
			if (!Equals(oldValue, newValue))
			{
				OnValueChanged((T)oldValue, (T)newValue);

				UpdateRepeatButtons((T)newValue);
			}
		}

		private void UpdateRepeatButtons(T newValue)
		{
			// Updates repeat button IsEnabled state based upon the Min/Max values.

			if (_increment != null)
			{
				_increment.IsEnabled = Compare(newValue, MaxValue) != 0;
			}

			if (_decrement != null)
			{
				_decrement.IsEnabled = Compare(newValue, MinValue) != 0;
			}
		}

		/// <summary>
		/// Parses the text value and ensures that its within the boundaries of
		/// <see cref="MinValue"/> and <see cref="MaxValue"/> then formats and sets the
		/// text value and updates the previous text value.
		/// </summary>
		/// <param name="text">The text value to set.</param>
		/// <param name="format">Whether the text value should be formatted.</param>
		private void SetText(string text, bool format)
		{
			if (text == null)
			{
				return;
			}

			var value = CoerceMinMax(Parse(text));

			if (format)
			{
				text = FormatValue(value);
			}

			_previousText = text;

			Text = text;

			Value = value;
		}

		/// <summary>
		/// Performs validation on the new value and updates the text value
		/// if valid otherwise restores the old value.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private void UpdateText(string oldValue, string newValue)
		{
		

			// If the values are not equal
			if (!Equals(_previousText, newValue))
			{
				var index = CaretIndex;

				// Check that new value is in a valid format
				if (ValidateText(newValue))
				{
					var canFormat = CanFormatText(newValue) && AutoFormat;

					// Set and format text
					SetText(newValue, canFormat);
				}
				else
				{
					// If false then undo changes
					SetText(oldValue, true);
				}

				CaretIndex = index;
			}
		}

	}
}
