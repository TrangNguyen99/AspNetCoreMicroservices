using System.ComponentModel.DataAnnotations.Schema;

namespace Discount.Api.Entities;

public class Coupon
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string ProductId { get; set; } = null!;
    public int Amount { get; set; }
}
