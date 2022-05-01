using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIFramework.Resources.Events
{
    /// <summary>
    ///     The value changed event handler. Invoked when a value has changed to notify attached
    ///     listeners of the changed value.
    /// </summary>
    /// <typeparam name="T">The type of value.</typeparam>
    /// <param name="sender">The object that this is invoked from.</param>
    /// <param name="e">The associated event args.</param>
    public delegate void ValueChangedEventHandler<T>(object sender, ValueChangedEventArgs<T> e);

    /// <summary>
    ///     The event args sent with the <see cref="ValueChangedEventHandler{T}"/> notifying
    ///     attached listeners of a changed value.
    /// </summary>
    /// <typeparam name="T">The type of value.</typeparam>
    public class ValueChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        ///     The changed value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        ///     The amount the value was changed by.
        /// </summary>
        public T Delta { get; set; }
    }
}
