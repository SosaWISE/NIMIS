﻿namespace SOS.Lib.RazorEngine.Templating
{
    using System;

    /// <summary>
    /// Defines an activator that uses a delegate to create an instance of the type.
    /// </summary>
    internal class DelegateActivator : IActivator
    {
        #region Fields
        private readonly Func<Type, ITemplate> _activator;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="DelegateActivator"/>.
        /// </summary>
        /// <param name="activator">The activator delegate.</param>
        public DelegateActivator(Func<Type, ITemplate> activator)
        {
            if (activator == null)
                throw new ArgumentNullException("activator");

            _activator = activator;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <param name="type">The type to create an instance.</param>
        /// <returns>The <see cref="ITemplate"/> instance.</returns>
        public ITemplate CreateInstance(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return _activator(type);
        }
        #endregion
    }
}