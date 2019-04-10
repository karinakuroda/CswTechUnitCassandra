using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraCSW
{
    public interface IRepository
    {
        bool CreateOrderStatus(string name);

        List<OrderStatus> ListOrderStatus();

        bool UpdateOrderStatus(string oldName, string newName);

        bool DeleteOrderStatus(string status);

    }
}
