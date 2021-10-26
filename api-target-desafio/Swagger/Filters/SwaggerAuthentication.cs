using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace api_target_desafio.Swagger.Filters
{
    public class SwaggerAuthentication : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
          
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "api-key",
                    In = ParameterLocation.Header,
                    Description = "API-KEY",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        
                        Default = new OpenApiString("48236d8ec201df516d0f6472d516d72b")
                    }
                });
            
        }
    }
}
