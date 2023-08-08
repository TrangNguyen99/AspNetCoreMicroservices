namespace Order.Application.Contracts;

public interface ICurrentUserProvider<TUserId>
{
    TUserId? GetCurrentUserId();
}
