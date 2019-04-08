using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraCSW
{
    public class Repository
    {
        private static ISession _session { get; set; }

        public Repository()
        {
            var cluster = Cluster.Builder()
                     .AddContactPoints("127.0.0.1")
                     .Build();
            _session = cluster.Connect("cswkeyspace");
        }

        public bool CreateOrderStatus(string name)
        {
            try
            {
                var result = _session.Execute($"INSERT INTO cswkeyspace.order_status(id, status) VALUES({Guid.NewGuid()}, '{name}')");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
          
            return true;
        }

        public RowSet ListOrderStatus()
        {
            return _session.Execute("SELECT * FROM order_status");
        }

        private RowSet GetOrderStatusByName(string name)
        {
            var selectCommand = _session.Execute($"SELECT * FROM order_status WHERE status = '{name}' ALLOW FILTERING");
            return selectCommand;
        }

        public bool UpdateOrderStatus(string oldName, string newName)
        {
            try
            {
                var oldOrder = GetOrderStatusByName(oldName);
                var result = oldOrder.GetRows().FirstOrDefault();
                if (result != null)
                {
                    var updateCommand = _session.Execute($"UPDATE cswkeyspace.order_status SET status = '{newName}' WHERE id = {result.GetValue<Guid>("id")}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;

            }
            return false;

        }

        public bool DeleteOrderStatus(string status)
        {
            try
            {
                var oldOrder = GetOrderStatusByName(status);
                var result = oldOrder.GetRows().FirstOrDefault();
                if (result != null)
                {
                    var deleteCommand = _session.Execute($"DELETE FROM cswkeyspace.order_status WHERE id = {result.GetValue<Guid>("id")}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return false;

        }

    }
}
