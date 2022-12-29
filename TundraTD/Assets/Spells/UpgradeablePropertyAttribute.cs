using System;
using System.Reflection;

namespace Assets.Spells
{
    /// <summary>
    /// Represents the property which value should be increased by fixed value every time you use an element to upgrade the spell.
    /// </summary>
    public class IncreasablePropertyAttribute : UpgradeablePropertyAttribute
    {
        /// <summary>
        /// Amount by which to increase the value of the property.
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Creates the new instance of a <see cref="IncreasablePropertyAttribute"/> class.
        /// </summary>
        /// <param name="trigger">Elements to trigger the property.</param>
        /// <param name="amount">Amount to increase.</param>
        public IncreasablePropertyAttribute(BasicElement trigger, float amount)
        {
            ConfirmableElements = trigger;
            Amount = amount;
        }

        /// <inheritdoc/>
        public override void UpgradeProperty(PropertyInfo property, object obj)
        {
            if (property.PropertyType == typeof(float))
            {
                property.SetValue(obj, (float)property.GetValue(obj) + Amount);
            }
            if (property.PropertyType == typeof(double))
            {
                property.SetValue(obj, (double)property.GetValue(obj) + Amount);
            }
            if (property.PropertyType == typeof(int))
            {
                property.SetValue(obj, (int)property.GetValue(obj) + (int)Amount);
            }
        }
    }

    /// <summary>
    /// Represents the property which value should be multiplied by fixed value every time you use the element to upgrade it.
    /// </summary>
    public class MultiplictablePropertyAttribute : UpgradeablePropertyAttribute
    {
        /// <summary>
        /// Amount to multiply.
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Creates the new instance of a <see cref="MultiplictablePropertyAttribute"/> class.
        /// </summary>
        /// <param name="trigger">Elements to trigger the property.</param>
        /// <param name="amount">Amount to increase.</param>
        public MultiplictablePropertyAttribute(BasicElement trigger, float amount)
        {
            ConfirmableElements = trigger;
            Amount = amount;
        }

        /// <inheritdoc/>
        public override void UpgradeProperty(PropertyInfo property, object obj)
        {
            if (property.PropertyType == typeof(float))
            {
                property.SetValue(obj, (float)property.GetValue(obj) * Amount);
            }
            if (property.PropertyType == typeof(double))
            {
                property.SetValue(obj, (double)property.GetValue(obj) * Amount);
            }
            if (property.PropertyType == typeof(int))
            {
                property.SetValue(obj, (int)property.GetValue(obj) * (int)Amount);
            }
        }
    }

    /// <summary>
    /// Represents an attribute for any upgradeable properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class UpgradeablePropertyAttribute : Attribute
    {
        /// <summary>
        /// Elements that activates the trigger to upgrade property value.
        /// </summary>
        public BasicElement ConfirmableElements { get; set; }

        /// <summary>
        /// Tries to upgrade the property using given element.
        /// </summary>
        /// <param name="element">Element that was used to upgrade the property.</param>
        /// <param name="property">Property to upgrade.</param>
        /// <param name="obj">Object to upgrade.</param>
        /// <returns><see langword="true"/> if property was upgraded, <see langword="false"/> otherwise.</returns>
        public virtual bool TryUpgradeProperty(BasicElement element, PropertyInfo property, object obj)
        {
            if (!ConfirmableElements.HasFlag(element)) return false;
            UpgradeProperty(property, obj);
            return true;
        }

        /// <summary>
        /// Upgrades the property without checking.
        /// </summary>
        /// <param name="property">Property to upgrade.</param>
        /// <param name="obj">Object to upgrade.</param>
        public abstract void UpgradeProperty(PropertyInfo property, object obj);
    }
}