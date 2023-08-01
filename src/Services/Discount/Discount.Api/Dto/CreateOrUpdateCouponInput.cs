namespace Discount.Api.Dto
{
    public class CreateOrUpdateCouponInput
    {
        public string ProductId { get; set; } = null!;
        public int Amount { get; set; }
    }
}
