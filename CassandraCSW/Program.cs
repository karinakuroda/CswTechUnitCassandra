using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraCSW
{
    class Program
    {
        private static ISession _session { get; set; }
        static void Main(string[] args)
        {
            var showOptions = true;

            var cluster = Cluster.Builder()
                     .AddContactPoints("127.0.0.1")
                     .Build();
            _session = cluster.Connect("cswkeyspace");
            while (showOptions)
            {
                ShowOptions();
                var option = Console.ReadLine();

                switch (option)
                {
                    case "C":
                        CreateOrderStatus();
                        break;
                    case "R":
                        ListOrderStatus();
                        break;
                    case "U":
                        UpdateOrderStatus();
                        break;
                    case "D":
                        DeleteOrderStatus();
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Deseja continuar?(Y/N)");
                if (Console.ReadLine() != "Y")
                    showOptions = false;
            }


        }
        public static void ShowOptions()
        {
            Console.WriteLine("Digite o comando desejado:");
            Console.WriteLine("C = Criar um novo status");
            Console.WriteLine("R = Listar status");
            Console.WriteLine("U = Alterar um status");
            Console.WriteLine("D = Remover um status");
        }

        public static void CreateOrderStatus()
        {
            Console.WriteLine("Digite o nome do novo status:");
            var name = Console.ReadLine();
            var rs = _session.Execute($"INSERT INTO cswkeyspace.order_status(id, status) VALUES({Guid.NewGuid()}, '{name}')");
            Console.WriteLine($"Response: {rs}");
        }

        public static void ListOrderStatus()
        {
            var rs = _session.Execute("SELECT * FROM order_status");
            foreach (var row in rs)
            {
                Console.WriteLine($"Id:{row.GetValue<Guid>("id")}| Name:{row.GetValue<string>("status")}");
            }
        }

        public static void UpdateOrderStatus()
        {
            Console.WriteLine("Digite o nome do status que deseja alterar:");
            var status = Console.ReadLine();
            var selectCommand = _session.Execute($"SELECT * FROM order_status WHERE status = '{status}' ALLOW FILTERING");
            var result = selectCommand.GetRows().First().GetValue<Guid>("id");
            if (result!=null)
            {
                Console.WriteLine("Digite o novo nome que deseja alterar:");
                var newName = Console.ReadLine();
    
                var updateCommand = _session.Execute($"UPDATE cswkeyspace.order_status SET status = '{newName}' WHERE id = {result}");
                Console.WriteLine($"Response: {updateCommand}");

            }
            else
                Console.WriteLine("Não foi possível localizar o status");

        }

        public static void DeleteOrderStatus()
        {
            Console.WriteLine("Digite o nome do status que deseja remover:");
            var status = Console.ReadLine();
            var selectCommand = _session.Execute($"SELECT * FROM order_status WHERE status = '{status}' ALLOW FILTERING");
            if (selectCommand.SingleOrDefault() != null)
            {
                var id = selectCommand.SingleOrDefault().GetValue<Guid>("id");
                var deleteCommand = _session.Execute($"DELETE FROM cswkeyspace.order_status WHERE id = {id}");
                Console.WriteLine($"Response: {deleteCommand}");
            }
            else
                Console.WriteLine("Não foi possível localizar o status");

        }
    }
}
