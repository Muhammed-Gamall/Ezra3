namespace Graduation.Services
{

    public interface IOrderService
    {

    }
    public class OrderService(ApplicationDbContext context) : IOrderService
    {
        private readonly ApplicationDbContext context = context;


       
    }
}
