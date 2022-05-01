using UIFramework.Resources.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace UIFramework
{
    /// <summary>
    /// Double Up Down control.
    /// </summary>
    public class DoubleUpDown : NumericUpDown<double>
    {
        /// <summary>
        /// The minimum precision dependency property.
        /// </summary>
        public static readonly DependencyProperty MaxPrecisionProperty =
            DependencyProperty.Register(nameof(MaxPrecision), typeof(int), typeof(DoubleUpDown),
                new PropertyMetadata(DefaultMaxPrecision));

        /// <summary>
        /// The minimum precision dependency property.
        /// </summary>
        public static readonly DependencyProperty MinPrecisionProperty =
            DependencyProperty.Register(nameof(MinPrecision), typeof(int), typeof(DoubleUpDown),
                new PropertyMetadata(DefaultMinPrecision));

        public const int DefaultMaxPrecision = 3;
        public const int DefaultMinPrecision = 2;
        private const double DefaultIncrement = 1d;
        private const double DefaultMaxValue = double.MaxValue;
        private const double DefaultMinValue = double.MinValue;
        private readonly Regex _validRegex;
        private int _precision;

        /// <summary>
        /// Instantiates an instance of the <see cref="DoubleUpDown"/> control.
        /// </summary>
        public DoubleUpDown()
        {
            DefaultStyleKey = typeof(DoubleUpDown);

            _validRegex = new Regex(@"^([-]?)(\d*)([,.]?)(\d*)$");

            MinValue = DefaultMinValue;
            MaxValue = DefaultMaxValue;

            Increment = DefaultIncrement;
        }

        /// <summary>
        /// The maximum number of digits following the decimal place.
        /// </summary>
        public int MaxPrecision
        {
            get => (int)GetValue(MaxPrecisionProperty);
            set => SetValue(MaxPrecisionProperty, value);
        }

        /// <summary>
        /// The minimum number of digits following the decimal place.
        /// </summary>
        public int MinPrecision
        {
            get => (int)GetValue(MinPrecisionProperty);
            set => SetValue(MinPrecisionProperty, value);
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

            // Conditions where the value shouldn't be formatted yet
            if (HasSign && Equals(text, "-", "-0", "-0.", "-."))
            {
                return false;
            }

            // This will return default value
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }

            // Check the value is valid
            return Double.TryParse(text, out var _);
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.DecrementValue(T, T)"/>/>
        protected override double DecrementValue(double current, double amount)
        {
            return current - amount;
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.FormatValue(T)"/>/>
        protected override string FormatValue(double value)
        {
            return string.Format(new NumberFormatInfo
            {
                NumberDecimalDigits = _precision
            }, "{0:F}", new decimal(value));
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.IncrementValue(T, T)"/>/>
        protected override double IncrementValue(double current, double amount)
        {
            return current + amount;
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.OnValueChanged(T, T)"/>/>
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            RaiseValueChanged(new ValueChangedEventArgs<double>
            {
                Value = newValue,
                Delta = newValue - oldValue
            });
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.Parse(string)"/>/>
        protected override double Parse(string value)
        {
            try
            {
                var index = value.IndexOf(".") + 1;

                if (index > 0)
                {
                    _precision = value.Length - index;

                    if (_precision < MinPrecision)
                    {
                        _precision = MinPrecision;
                    }
                    if (_precision > MaxPrecision)
                    {
                        // Truncate text value before parsing to prevent rounding
                        value = value.Substring(0, index + MaxPrecision);

                        _precision = MaxPrecision;
                    }
                }
                else
                {
                    _precision = MinPrecision;
                }

                return double.Parse(value);
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

        /// <summary>
        /// Performs an OR EQUALS check on the specified value against the
        /// specified arguments and returns true if it matches any one.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="args">The arguments to check the value against.</param>
        /// <returns>The result of the check.</returns>
        private static bool Equals(string value, params string[] args)
        {
            var result = false;

            foreach (var arg in args)
            {
                result |= object.Equals(value, arg);
            }

            return result;
        }

        /// <inheritdoc <see cref="NumericUpDown{T}.Parse(double)"/>/>
        protected override string Parse(double value)
        {
            return string.Format(new NumberFormatInfo
            {
                NumberDecimalDigits = MaxPrecision
            }, "{0:F}", new decimal(value));
        }
    }
}
