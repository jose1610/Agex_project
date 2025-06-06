using AGEX.CORE.Dtos.Orders.Delete;
using AGEX.CORE.Dtos.Orders.Get;
using AGEX.CORE.Dtos.Orders.Register;
using AGEX.CORE.Dtos.Orders.Update;
using AGEX.CORE.Enumerations;
using AGEX.CORE.Interfaces.Repositories.Orders;
using AGEX.CORE.Interfaces.Services;

namespace AGEX.CORE.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogService _logService;
        private readonly IParseService _parseService;
        private readonly ICryptoService _cryptoService;

        public OrderService(IOrderRepository orderRepository, ILogService logService, IParseService parseService, ICryptoService cryptoService)
        {
            _orderRepository = orderRepository;
            _logService = logService;
            _parseService = parseService;
            _cryptoService = cryptoService;
        }

        public async Task<RegisterOrderResDto> RegisterOrder(RegisterOrderReqDto request)
        {
            RegisterOrderResDto response = new();

            _logService.SaveLogApp($"[{nameof(RegisterOrder)}]", $"[REQUEST][[{nameof(RegisterOrder)}{_parseService.Serialize(request)}]", LogType.Information);

            await _orderRepository.RegisterOrder(request);

            response.message = "Success";

            _logService.SaveLogApp($"[{nameof(RegisterOrder)}]", $"[REQUEST][[{nameof(RegisterOrder)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }

        public async Task<UpdateOrderResDto> UpdateOrder(UpdateOrderReqDto request)
        {
            UpdateOrderResDto response = new();

            _logService.SaveLogApp($"[{nameof(UpdateOrder)}]", $"[REQUEST][[{nameof(UpdateOrder)}{_parseService.Serialize(request)}]", LogType.Information);

            await _orderRepository.UpdateStatusOrder(request);

            response.message = "Success";

            _logService.SaveLogApp($"[{nameof(UpdateOrder)}]", $"[REQUEST][[{nameof(UpdateOrder)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }

        public async Task<List<GetOrdersResDto>> GetOrder(GetOrdersReqDto request)
        {
            List<GetOrdersResDto> response = new();

            _logService.SaveLogApp($"[{nameof(GetOrder)}]", $"[REQUEST][[{nameof(GetOrder)}{_parseService.Serialize(request)}]", LogType.Information);

            if (string.IsNullOrEmpty(request.Id))
                response = await _orderRepository.GetOrders(request);
            else
                response = await _orderRepository.GetStatusOrder(request);

                _logService.SaveLogApp($"[{nameof(GetOrder)}]", $"[REQUEST][[{nameof(GetOrder)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }

        public async Task<DeleteOrderResDto> DeleteOrder(DeleteOrderReqDto request)
        {
            DeleteOrderResDto response = new();

            _logService.SaveLogApp($"[{nameof(DeleteOrder)}]", $"[REQUEST][[{nameof(DeleteOrder)}{_parseService.Serialize(request)}]", LogType.Information);

            await _orderRepository.DeleteOrder(request);

            response.message = "Success";

            _logService.SaveLogApp($"[{nameof(DeleteOrder)}]", $"[REQUEST][[{nameof(DeleteOrder)}{_parseService.Serialize(response)}]", LogType.Information);

            return response;
        }
    }
}
