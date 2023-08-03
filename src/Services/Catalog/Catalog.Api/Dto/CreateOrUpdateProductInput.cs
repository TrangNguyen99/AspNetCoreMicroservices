namespace Catalog.Api.Dto;

public class CreateOrUpdateProductInput
{
    public string Name { get; set; } = null!;
    public int Price { get; set; }
}
