using System;

namespace CassandraCSW
{
    class Program
    {
        public static Repository repository { get; set; }

        static void Main(string[] args)
        {
            repository = new Repository();
            var showOptions = true;
            
            while (showOptions)
            {
                ShowOptions();
                var option = Console.ReadLine();

                switch (option.ToUpper())
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
                    case "S":
                        showOptions = false;
                        break;
                    default:
                        showOptions = false;
                        break;
                }
            }
        }

        public static void ShowOptions()
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            Console.WriteLine("Digite o comando desejado:");
            Console.WriteLine();
            Console.WriteLine("C = Criar um novo status");
            Console.WriteLine("R = Listar status");
            Console.WriteLine("U = Alterar um status");
            Console.WriteLine("D = Remover um status");
            Console.WriteLine("S = Sair");
            Console.WriteLine("--------------------------");
            Console.WriteLine();   
        }

        public static void CreateOrderStatus()
        {
            Console.WriteLine("Digite o nome do novo status:");
            var name = Console.ReadLine();
            var result = repository.CreateOrderStatus(name);
            if (result)
                Console.WriteLine("Criado com sucesso");
            else
                Console.WriteLine("Não foi possível criar o novo status");
        }

        public static void ListOrderStatus()
        {
            var rs = repository.ListOrderStatus();
            foreach (var row in rs)
            {
                Console.WriteLine($"Id:{row.GetValue<Guid>("id")}| Name:{row.GetValue<string>("status")}");
            }
        }

        public static void UpdateOrderStatus()
        {
            Console.WriteLine("Digite o nome do status que deseja alterar:");
            var oldStatus = Console.ReadLine();
            Console.WriteLine("Digite o novo nome que deseja alterar:");
            var newStatus = Console.ReadLine();

            var result = repository.UpdateOrderStatus(oldStatus, newStatus);
            if (result)
                Console.WriteLine("Atualizado com sucesso");
            else
                Console.WriteLine("Não foi possível atualizar o status");
        }

        public static void DeleteOrderStatus()
        {
            Console.WriteLine("Digite o nome do status que deseja remover:");
            var status = Console.ReadLine();

            var result = repository.DeleteOrderStatus(status);
            if (result)
                Console.WriteLine("Removido com sucesso");
            else
                Console.WriteLine("Não foi possível remover o status");
        }
    }
}
