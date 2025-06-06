using AGEX.CORE.Dtos.Orders.Delete;
using AGEX.CORE.Dtos.Orders.Get;
using AGEX.CORE.Dtos.Orders.Register;
using AGEX.CORE.Dtos.Orders.Update;
using AGEX.CORE.Enumerations;
using AGEX.CORE.Exceptions;
using AGEX.CORE.Interfaces.Repositories;
using AGEX.CORE.Interfaces.Repositories.Orders;
using System.Data;

namespace AGEX.INFRAESTRUCTURE.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IBaseAgexRepository _baseRepository;
        private readonly string _sp = "sp_orders";

        public OrderRepository(IBaseAgexRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<int> RegisterOrder(RegisterOrderReqDto request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "REGISTER_ORDER" },
                {"@i_description", request.Description},
                {"@i_weight_order", Convert.ToDouble(request.WeightOrder) },
                {"@i_number_products", Convert.ToInt32(request.NumberProducts) },
                {"@i_amount_total", Convert.ToDouble(request.Amount_total) }
            });

            return ResponseCode.Success;
        }

        public async Task<int> UpdateStatusOrder(UpdateOrderReqDto request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "UPDATE_STATUS_ORDER" },
                {"@i_status", request.Status},
                {"@i_order_id", Convert.ToInt32(request.Id) }
            });

            return ResponseCode.Success;
        }

        public async Task<int> DeleteOrder(DeleteOrderReqDto request)
        {
            await _baseRepository.ExecSpAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "DELETE_ORDER" },
                {"@i_order_id", Convert.ToDouble(request.Id)}
            });

            return ResponseCode.Success;
        }

        public async Task<List<GetOrdersResDto>> GetOrders(GetOrdersReqDto request)
        {
            List<GetOrdersResDto> orderModels = new();

            var dt = await _baseRepository.ExecSpDataAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "GET_ORDERS" }
            });

            if (dt.Rows.Count == 0) throw new CustomException($"No data {nameof(GetOrders)} (c).");

            foreach (DataRow dr in dt.Rows)
            {
                orderModels.Add(new GetOrdersResDto
                {
                    Id = dr["ORDER_ID"].ToString(),
                    Description = dr["DESCRIPTION"].ToString(),
                    WeightOrder = dr["WEIGHT_ORDER"].ToString(),
                    NumberProducts = dr["NUMBER_PRODUCTS"].ToString(),
                    AmountTotal = dr["AMOUNT_TOTAL"].ToString(),
                    Status = dr["STATUS"].ToString(),
                    message = "Success"
                });
            }

            return orderModels;
        }

        public async Task<List<GetOrdersResDto>> GetStatusOrder(GetOrdersReqDto request)
        {
            List<GetOrdersResDto> orderModels = new();

            var dt = await _baseRepository.ExecSpDataAsync(_sp, new Dictionary<string, dynamic>
            {
                {"@i_operation_type", "GET_STATUS_ORDER" },
                {"@i_order_id", request.Id}
            });

            if (dt.Rows.Count == 0) throw new CustomException($"No data {nameof(GetStatusOrder)} (c).");

            foreach (DataRow dr in dt.Rows)
            {
                orderModels.Add(new GetOrdersResDto
                {
                    Id = dr["ORDER_ID"].ToString(),
                    Description = dr["DESCRIPTION"].ToString(),
                    WeightOrder = dr["WEIGHT_ORDER"].ToString(),
                    NumberProducts = dr["NUMBER_PRODUCTS"].ToString(),
                    AmountTotal = dr["AMOUNT_TOTAL"].ToString(),
                    Status = dr["STATUS"].ToString(),
                    message = "Success"
                });
            }

            return orderModels;
        }
    }
}
