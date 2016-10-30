using System;
using System.ComponentModel;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Contracts.Presentation.Rest
{
    public class CommaDelimitedCollectionModelBinder : IModelBinder
    {
        readonly string[] delimeters = {","};

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var val = bindingContext.ValueProvider.GetValue(key);

            if (val == null) return false;
            var s = val.AttemptedValue;
            if (s != null)
            {
                var elementType = bindingContext.ModelType.GetElementType();
                var converter = TypeDescriptor.GetConverter(elementType);
                var values = Array.ConvertAll(s.Split(delimeters, StringSplitOptions.RemoveEmptyEntries),
                    x => converter.ConvertFromString(x?.Trim()));

                var typedValues = Array.CreateInstance(elementType, values.Length);

                values.CopyTo(typedValues, 0);

                bindingContext.Model = typedValues;
            }
            else
            {
                bindingContext.Model = Array.CreateInstance(bindingContext.ModelType.GetElementType(), 0);
            }
            return true;
        }
    }
}
