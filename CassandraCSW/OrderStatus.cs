using System;

namespace CassandraCSW
{
    public class OrderStatus
    {
        public Guid Id { get; set; }
        public string Status { get; set; }

        public OrderStatus(Guid id, string status)
        {
            this.Id = id;
            this.Status = status;
        }

        public void ParseEntity()
        {
            throw new NotImplementedException();
        }
    }
}
