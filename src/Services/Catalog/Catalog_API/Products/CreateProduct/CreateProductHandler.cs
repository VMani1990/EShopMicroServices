namespace Catalog_API.Products.CreateProduct
{
    public record CreateProductCommand(string Name,List<string> Category,string Description,string ImageFile,decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.")
                                .Length(2, 150).WithMessage("Product name must be between 2 and 150 characters.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required.")
                                 .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
    internal class CreateProductCommndHandler
        (IDocumentSession session, ILogger<CreateProductCommndHandler> logger) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create Product entity from command object
            //save to database
            //return CreateProductResult result
            logger.LogInformation("CreateProductHandler.Handler called with {@Command}", command);
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description=command.Description,
                ImageFile=command.ImageFile,
                Price=command.Price
            };
            //save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            //retunr result
            return new CreateProductResult(product.Id);
        }
    }
}
