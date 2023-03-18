using OrdersApiAppPV012.Model.BusinessLogic;

namespace OrdersApiAppPV012.Service.BillService
{
    public interface IDaoBill
    {
        public Bill GetBill(int Id);
    }
}
