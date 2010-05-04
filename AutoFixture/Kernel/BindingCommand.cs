﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Globalization;

namespace Ploeh.AutoFixture.Kernel
{
    /// <summary>
    /// Encapsulates a command that binds a property or a field to a value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the specimn on which the property or value will be set.
    /// </typeparam>
    /// <typeparam name="TProperty">The type of property or field.</typeparam>
    public class BindingCommand<T, TProperty> : ISpecifiedSpecimenCommand<T>
    {
        private readonly MemberInfo member;
        private readonly Func<ISpecimenContainer, TProperty> createBindingValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingCommand{T, TProperty}"/> class with
        /// the supplied property picker expression.
        /// </summary>
        /// <param name="propertyPicker">An expression that identifies a property or field.</param>
        /// <remarks>
        /// <para>
        /// This constructor implies that an anonymous value will be assigned to the property or
        /// field identified by <paramref name="propertyPicker"/>.
        /// </para>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This follows the same coding idiom as LINQ to SQL and LINQ to Entities. Since Funcs have implicit conversions into Expressions, usage is not as bad as it could have been. In any case, the desired functionality cannot be implemented in any other way while preserving static type checking.")]
        public BindingCommand(Expression<Func<T, TProperty>> propertyPicker)
        {
            if (propertyPicker == null)
            {
                throw new ArgumentNullException("propertyPicker");
            }

            this.member = propertyPicker.GetWritableMember().Member;
            this.createBindingValue = this.CreateAnonymousValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingCommand{T, TProperty}"/> class with
        /// the supplied property picker expression and the value to be assigned to that property
        /// or field.
        /// </summary>
        /// <param name="propertyPicker">An expression that identifies a property or field.</param>
        /// <param name="propertyValue">
        /// The value to assign to the property or field identified by
        /// <paramref name="propertyPicker"/>.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This follows the same coding idiom as LINQ to SQL and LINQ to Entities. Since Funcs have implicit conversions into Expressions, usage is not as bad as it could have been. In any case, the desired functionality cannot be implemented in any other way while preserving static type checking.")]
        public BindingCommand(Expression<Func<T, TProperty>> propertyPicker, TProperty propertyValue)
        {
            if (propertyPicker == null)
            {
                throw new ArgumentNullException("propertyPicker");
            }
            if (propertyValue == null)
            {
                throw new ArgumentNullException("propertyValue");
            }

            this.member = propertyPicker.GetWritableMember().Member;
            this.createBindingValue = c => propertyValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingCommand{T, TProperty}"/> class with
        /// the supplied property picker expression and a function that creates a value to be
        /// assigned to that property or field.
        /// </summary>
        /// <param name="propertyPicker">An expression that identifies a property or field.</param>
        /// <param name="valueCreator">
        /// A function that creates a value that will be assigned to the property or field
        /// identified by <paramref name="propertyPicker"/>.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This follows the same coding idiom as LINQ to SQL and LINQ to Entities. Since Funcs have implicit conversions into Expressions, usage is not as bad as it could have been. In any case, the desired functionality cannot be implemented in any other way while preserving static type checking.")]
        public BindingCommand(Expression<Func<T, TProperty>> propertyPicker, Func<ISpecimenContainer, TProperty> valueCreator)
        {
            if (propertyPicker == null)
            {
                throw new ArgumentNullException("propertyPicker");
            }
            if (valueCreator == null)
            {
                throw new ArgumentNullException("valueCreator");
            }

            this.member = propertyPicker.GetWritableMember().Member;
            this.createBindingValue = valueCreator;
        }


        #region ISpecifiedSpecimenCommand<T> Members

        /// <summary>
        /// Executes the command on the supplied specimen by assigning the property of field the
        /// correct value.
        /// </summary>
        /// <param name="specimen">
        /// A specimen that should have its property or field assigned.
        /// </param>
        /// <param name="container">
        /// An <see cref="ISpecimenContainer"/> which can supply an anonymous value for the
        /// property or field.
        /// </param>
        /// <remarks>
        /// <para>
        /// This method assigns a value to the property or field identified by the expression
        /// supplied to the class' constructor. If no value (or creator) was supplied to the
        /// constructor, <paramref name="container"/> will be used to create the value.
        /// </para>
        /// </remarks>
        public void Execute(T specimen, ISpecimenContainer container)
        {
            if (specimen == null)
            {
                throw new ArgumentNullException("specimen");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            var bindingValue = this.createBindingValue(container);

            var pi = this.member as PropertyInfo;
            if (pi != null)
            {
                pi.SetValue(specimen, bindingValue, null);
            }

            var fi = this.member as FieldInfo;
            if (fi != null)
            {
                fi.SetValue(specimen, bindingValue);
            }
        }

        #endregion

        #region IRequestSpecification Members

        /// <summary>
        /// Evaluates whether a request matches the property or field affected by this command.
        /// </summary>
        /// <param name="request">The specimen request.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="request"/> is a <see cref="PropertyInfo"/>
        /// or <see cref="FieldInfo"/> that identifies the property or field affected by this
        /// <see cref="BindingCommand{T, TProperty}"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsSatisfiedBy(object request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            return this.member.Equals(request);
        }

        #endregion

        private TProperty CreateAnonymousValue(ISpecimenContainer container)
        {
            var bindingValue = container.Create(this.member);
            if (!(bindingValue is TProperty))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                    "The specimen created for assignment is not compatible with {0}.", typeof(TProperty)));
            }
            return (TProperty)bindingValue;
        }
    }
}
