using UIFramework.Resources.Events;
using System;
using System.Text.RegularExpressions;

namespace UIFramework
{
	/// <summary>
	/// Integer down control.
	/// </summary>
	public class IntegerUpDown : NumericUpDown<int>
    {
      
        private const int DefaultIncrement = 1;
        private const int DefaultMaxValue = int.MaxValue;
        private const int DefaultMinValue = int.MinValue;
        private readonly Regex _validRegex;

        /// <summary>
        /// Instantiates an instance of the <see cref="IntegerUpDown"/> control.
        /// </summary>
        public IntegerUpDown()
        {
            DefaultStyleKey = typeof(IntegerUpDown);

            _validRegex = new Regex(@"^([-]?)(\d+)$");

            MinValue = DefaultMinValue;
            MaxValue = DefaultMaxValue;

            Increment = DefaultIncrement;
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.HasSign"/>/>
        protected override bool HasSign => MinValue < 0;

        /// <inheritdoc <see cref="NumericUpDown{T}.CanFormatText(string)"/>/>
        protected override bool CanFormatText(string text)
        {
            // Force a format to remove this
            if (!HasSign && Equals(text, "-"))
            {
                return true;
            }

            // This will return default value
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }

            // Check the value is valid
            return int.TryParse(text, out var _);
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.DecrementValue(T, T)"/>/>
        protected override int DecrementValue(int current, int amount)
        {
            return current - amount;
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.FormatValue(T)"/>/>
        protected override string FormatValue(int value)
        {
            return Parse(value);
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.IncrementValue(T, T)"/>/>
        protected override int IncrementValue(int current, int amount)
        {
            return current + amount;
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.OnValueChanged(T, T)"/>/>
        protected override void OnValueChanged(int oldValue, int newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            RaiseValueChanged(new ValueChangedEventArgs<int>
            {
                Value = newValue,
                Delta = newValue - oldValue
            });
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.Parse(string)"/>/>
        protected override int Parse(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch (Exception)
            {
                // Swallow exception and return default value
                return 0;
            }
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.ValidateText(string)"/>/>
        protected override bool ValidateText(string value)
        {
            return _validRegex.IsMatch(value);
        }


        /// <inheritdoc <see cref="NumericUpDown{T}.Parse(double)"/>/>
        protected override string Parse(int value)
        {
            return $"{value}";
        }
    }
}
