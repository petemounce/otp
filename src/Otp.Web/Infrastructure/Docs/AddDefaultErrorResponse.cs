using System.Web.Http.Description;
using JE.ApiValidation.DTOs;
using Swashbuckle.Swagger;

namespace Otp.Web.Infrastructure.Docs
{
    public class AddDefaultErrorResponse : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var errorSchema = schemaRegistry.GetOrRegister(typeof(StandardErrorResponse));

            operation.responses.Add("default", new Response
            {
                description = "Error",
                schema = errorSchema
            });
        }
    }
}