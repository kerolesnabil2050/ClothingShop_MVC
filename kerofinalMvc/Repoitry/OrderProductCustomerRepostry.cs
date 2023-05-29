using Project.Models;

namespace Project.Repoitry
{
    public class OrderProductCustomerRepostry : Repositry<OrderProdectSeller>, IOrderProductCustomerRepostry

    {
        public OrderProductCustomerRepostry(Context context) : base(context)
        {
        }
    }
}
