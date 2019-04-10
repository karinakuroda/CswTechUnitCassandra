using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CassandraCSW.Repository
{
    public class RepositoryCql : IRepository
    {
        private static ISession _session { get; set; }

        public RepositoryCql()
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

        public List<OrderStatus> ListOrderStatus()
        {
            var rows = _session.Execute($"SELECT * FROM order_status");
            var orders = ParseToModel(rows);
            return orders;
        }

        public bool UpdateOrderStatus(string oldName, string newName)
        {
            try
            {
                var oldOrder = GetOrderStatusByName(oldName);
                var result = oldOrder.FirstOrDefault();
                if (result != null)
                {
                    var updateCommand = _session.Execute($"UPDATE cswkeyspace.order_status SET status = '{newName}' WHERE id = {result.Id}");
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
                var result = oldOrder.FirstOrDefault();
                if (result != null)
                {
                    var deleteCommand = _session.Execute($"DELETE FROM cswkeyspace.order_status WHERE id = {result.Id}");
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

        #region Private

        private List<OrderStatus> GetOrderStatusByName(string name)
        {
            var rows = _session.Execute($"SELECT * FROM order_status WHERE status = '{name}' ALLOW FILTERING");
            var orders = ParseToModel(rows);
            return orders;
        }

        private List<OrderStatus> ParseToModel(RowSet rows)
        {
            return rows.GetRows().Select(row => new OrderStatus(row.GetValue<Guid>("id"), row.GetValue<string>("status"))).ToList();
        }

        #endregion
    }
}
