namespace Catalog.Api.Dto
{
    public class CreateOrUpdateProductInput
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
