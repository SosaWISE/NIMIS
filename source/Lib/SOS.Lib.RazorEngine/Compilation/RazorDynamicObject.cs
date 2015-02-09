﻿namespace SOS.Lib.RazorEngine.Compilation
{
    using System;
    using System.Diagnostics;
    using System.Dynamic;

    /// <summary>
    /// Defines a dynamic object.
    /// </summary>
    internal class RazorDynamicObject : DynamicObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        public object Model { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the value of the specified member.
        /// </summary>
        /// <param name="binder">The current binder.</param>
        /// <param name="result">The member result.</param>
        /// <returns>True.</returns>
        [DebuggerStepThrough]
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var dynamicObject = Model as RazorDynamicObject;
            if (dynamicObject != null)
                return dynamicObject.TryGetMember(binder, out result);

            Type modelType = Model.GetType();
            var prop = modelType.GetProperty(binder.Name);
            if (prop == null)
            {
                result = null;
                return false;
            }

            object value = prop.GetValue(Model, null);
            if (value == null)
            {
                result = value;
                return true;
            }

            Type valueType = value.GetType();
            result = (CompilerServices.IsAnonymousType(valueType))
                         ? new RazorDynamicObject { Model = value }
                         : value;
            return true;
        }
        #endregion
    }
}
