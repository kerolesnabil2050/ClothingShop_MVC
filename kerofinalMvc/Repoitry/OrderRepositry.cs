using Project.Models;

namespace Project.Repoitry
{
    public class OrderRepositry : Repositry<Order>, IOrderRepositry
    {
        public OrderRepositry(Context context) : base(context)
        {
        }
    }
}
