namespace Basket.Api.Repositories
{
    public interface IBacketRepository
    {
        Task GetBasket(string userName);
    }
}