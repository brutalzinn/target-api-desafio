using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace api_target_desafio.Swagger.Filters
{
	// THIS CLASS IS A FILTER FOR SWAGGER. THIS IS MY FIRST CONTACT WITH SWAGGER AND A ASPCORE API. 
	// THANKS TO ARTICLE: https://www.seeleycoder.com/blog/optional-route-parameters-with-swagger-asp-net-core/
	// TO EXPLAIN THE USES OF FILTER ON SWAGGER. THIS ARTICLE GIVES ME POSSIBILITY TO DO SWAGGER HEADER PARAM TO USE API KEY.
	// robertocpaes - 25/10/2021 
	public class ReApplyOptionalRouteParameter : IOperationFilter
	{
		const string captureName = "routeParameter";

		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var httpMethodAttributes = context.MethodInfo
				.GetCustomAttributes(true)
				.OfType<Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute>();

			var httpMethodWithOptional = httpMethodAttributes?.FirstOrDefault(m => m.Template?.Contains("?") ?? false);
			if (httpMethodWithOptional == null)
				return;

			string regex = $"{{(?<{captureName}>\\w+)\\?}}";

			var matches = System.Text.RegularExpressions.Regex.Matches(httpMethodWithOptional.Template, regex);

			foreach (System.Text.RegularExpressions.Match match in matches)
			{
				var name = match.Groups[captureName].Value;

				var parameter = operation.Parameters.FirstOrDefault(p => p.In == ParameterLocation.Path && p.Name == name);
				if (parameter != null)
				{
					parameter.AllowEmptyValue = true;
					parameter.Description = $"{name} is optional parameter";
					parameter.Required = false;
					//parameter.Schema.Default = new OpenApiString(string.Empty);
					parameter.Schema.Nullable = true;
				}
			}
		}
	}
}
