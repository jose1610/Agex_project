using AGEX.CORE.Dtos.Orders.Delete;
using AGEX.CORE.Dtos.Orders.Get;
using AGEX.CORE.Dtos.Orders.Register;
using AGEX.CORE.Dtos.Orders.Update;

namespace AGEX.CORE.Interfaces.Services
{
    public interface IOrderService
    {
        Task<DeleteOrderResDto> DeleteOrder(DeleteOrderReqDto request);
        Task<List<GetOrdersResDto>> GetOrder(GetOrdersReqDto request);
        Task<RegisterOrderResDto> RegisterOrder(RegisterOrderReqDto request);
        Task<UpdateOrderResDto> UpdateOrder(UpdateOrderReqDto request);
    }
}