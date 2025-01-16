
using System.Threading;
using Discount.Grpc;

namespace BasketAPI.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator:AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required!!");
        }
    }
    public class StoreBasketCommandHandler
        (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {

            await DeductDiscount(command.Cart,cancellationToken);

            
            //TODO: store basket in database (use Marten upsert - if exist = update - if not= insert new record)
            //TODO: Update cache 
            await repository.StoreBasket(command.Cart,cancellationToken);

            return new StoreBasketResult(command.Cart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart cart,CancellationToken cancellationToken)
        {
            //TODO : Communicate with Discount.Grpc and calculate lastest prices of products into basket
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }

        }
    }

 


}
