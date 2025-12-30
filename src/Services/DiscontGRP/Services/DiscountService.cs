
namespace DiscontGRP.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon == null)
            coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount", };

        logger.LogInformation("Discount is product name : {ProductName}, Description: {Description}", coupon.ProductName, coupon.Description);

        return coupon.Adapt<CouponModel>();
    }
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));

        try
        {
            dbContext.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount created. ProductName :{ProductName}, Description: {Description}", coupon.ProductName, coupon.Description);

        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));


        logger.LogInformation("Discount successfully update. ProductName :{ProductName}, Description: {Description}", coupon.ProductName, coupon.Description);

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Product {request.ProductName} not found"));

        dbContext.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Product removed successfuly.");

        return new DeleteDiscountResponse { Success = true };

    }
}
