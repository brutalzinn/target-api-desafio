using api_target_desafio.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace api_target_desafio.Providers
{
    // THIS IS A DATETIME BINDER TO FORMAT DATETIME IN BRAZILIAN FORMAT.
    // THIS WILL BE USED FOR ALL CONTROLLERS THAT HANDLE DATETIME OBJECTS.
    // THANKS TO THIS ARTICLE: https://www.ti-enxame.com/pt/c%23/alterar-o-formato-padrao-para-analise-de-datetime-no-asp.net-core/830480188/
    // - robertocpaes
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(DateTime) ||
                context.Metadata.ModelType == typeof(DateTime?))
            {
                return new DateTimeModelBinder();
            }

            return null;
        }
    }
}
