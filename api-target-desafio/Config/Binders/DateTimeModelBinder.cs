using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace api_target_desafio.Binders
{
    // THIS IS A DATETIME BINDER TO FORMAT DATETIME IN BRAZILIAN FORMAT.
    // THIS WILL BE USED FOR ALL CONTROLLERS THAT HANDLE DATETIME OBJECTS.
    // THANKS TO THIS ARTICLE: https://www.ti-enxame.com/pt/c%23/alterar-o-formato-padrao-para-analise-de-datetime-no-asp.net-core/830480188/
    // - robertocpaes
    public class DateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            // Try to fetch the value of the argument by name
            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var dateStr = valueProviderResult.FirstValue;
            // Here you define your custom parsing logic, i.e. using "de-DE" culture
            if (!DateTime.TryParse(dateStr, new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime date))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "DateTime should be in format pt-BR culture 'dd-MM-yyyy HH:mm:ss'");
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(date);
            return Task.CompletedTask;
        }
    }
}
