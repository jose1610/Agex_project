using AGEX.CORE.Dtos.Orders.Delete;
using AGEX.CORE.Dtos.Orders.Get;
using AGEX.CORE.Dtos.Orders.Register;
using AGEX.CORE.Dtos.Orders.Update;
using AGEX.CORE.Models.Orders;

namespace AGEX.CORE.Interfaces.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<int> DeleteOrder(DeleteOrderReqDto request);
        Task<List<GetOrdersResDto>> GetOrders(GetOrdersReqDto request);
        Task<List<GetOrdersResDto>> GetStatusOrder(GetOrdersReqDto request);
        Task<int> RegisterOrder(RegisterOrderReqDto request);
        Task<int> UpdateStatusOrder(UpdateOrderReqDto request);
    }
}