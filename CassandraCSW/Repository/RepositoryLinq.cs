using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraCSW.Repository
{
    public class RepositoryLinq : IRepository
    {
        public bool CreateOrderStatus(string name)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrderStatus(string status)
        {
            throw new NotImplementedException();
        }

        public List<OrderStatus> ListOrderStatus()
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrderStatus(string oldName, string newName)
        {
            throw new NotImplementedException();
        }
    }

}
