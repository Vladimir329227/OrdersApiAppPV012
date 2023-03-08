using Microsoft.CodeAnalysis;
using OrdersApiAppPV012.Model.BusinessLogic;
using OrdersApiAppPV012.Model.Entity;

namespace OrdersApiAppPV012.Service.OrderInfoService
{
    public interface IDaoOrderInfo
    {
        public Task<List<OrderProduct>> GetInfo(int Id);
    }
}
