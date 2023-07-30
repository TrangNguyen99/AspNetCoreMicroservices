using Cart.Api.Entities;

namespace Cart.Api.Dto
{
    public class UpdateCartInput
    {
        public List<CartItem> CartItems { get; set; } = null!;
    }
}
