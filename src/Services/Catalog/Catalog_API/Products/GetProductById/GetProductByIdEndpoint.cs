
using Catalog_API.Products.GetProducts;

namespace Catalog_API.Products.GetProductById
{
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}",async(Guid Id, ISender sender) =>
            {
                var results = await sender.Send(new GetProductByIdQuery(Id));
                var response = results.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProductsById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products By Id")
                .WithDescription("Get Products By Id");
        }
    }
}
