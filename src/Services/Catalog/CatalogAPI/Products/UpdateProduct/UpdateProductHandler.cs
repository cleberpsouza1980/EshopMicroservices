using Microsoft.AspNetCore.Session;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name,List<string> Category ,decimal Price, string Description)
        :ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator:AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Length(2,150).WithMessage("Name between 2 and 150 characteres");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required").MaximumLength(1000);
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price dont 0");
        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession session): 
        ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {        
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command,CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
                
            if (product == null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            product.Name = command.Name;
            product.Category = command.Category;
            product.Price = command.Price;
            product.Description = command.Description;
            
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
