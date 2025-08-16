namespace Catalog_API.Products.CreateProduct
{
    public record CreateProductCommand(string Name,List<string> Category,string Description,string ImageFile,decimal price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommndHandler(IDocumentSession session) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create Product entity from command object
            //save to database
            //return CreateProductResult result

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description=command.Description,
                ImageFile=command.ImageFile,
                Price=command.price
            };
            //save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            //retunr result
            return new CreateProductResult(product.Id);
        }
    }
}
