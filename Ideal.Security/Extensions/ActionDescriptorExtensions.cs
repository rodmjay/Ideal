#region credits
// ***********************************************************************
// Assembly	: Ideal.Security
// Author	: Rod Johnson
// Created	: 03-27-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Web.Mvc;

namespace Ideal.Security.Extensions
{
    #region

    

    #endregion

    public static class ActionDescriptorExtensions
    {
        public static bool HasAttribute<T>(this ActionDescriptor actionDescriptor) where T : Attribute
        {
            var attrType = typeof(T);
            return (actionDescriptor.IsDefined(attrType, true) ||
                    actionDescriptor.ControllerDescriptor.IsDefined(attrType, true));
        }
    }
}
