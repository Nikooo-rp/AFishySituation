using System.Numerics;

namespace AFishySituation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ahh yes, the shoreline. The smell of salt in the air, the sound of waves crashing, and the promise of a good catch,
            Console.WriteLine("Ahh si, la costa, el olor de sal en el aire, el sonido de las olas en la arena, y la promesa de una buena pesca.");
            SaveManager saveManager = new SaveManager();
            Player player = new Player();
            if (saveManager.SaveExists())
            {
                string ?loadChoice = null;
                Console.WriteLine("Se ha encontrado un archivo de guardado, quieres cargarlo? s/n");
                try
                {
                    loadChoice = Console.ReadLine();

                }
                catch (Exception e)
                {

                }
                if (loadChoice.ToLower() == "s")
                {
                    Console.WriteLine("Cargando guardado...");
                    player = saveManager.Load();
                    Console.WriteLine("Cargado correctamente!");
                }
                else
                {
                    Console.WriteLine("Iniciando un juego nuevo...");
                    player.name = AssignName();
                    
                }
            }
            else
            {
                player.name = AssignName();
            }
            FishingEngine fishingEngine = new FishingEngine(player);
            Shop shop = new Shop(player);
            if (player.rod != null)
                shop.availableRods.RemoveAll(r => r.power < player.rod.power);
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Bienvenido a la costa, {player.name}! ¿Qué te gustaría hacer?");
                Console.WriteLine("1. Ir a pescar");
                Console.WriteLine("2. Visitar la tienda");
                Console.WriteLine("3. Revisar el inventario");
                Console.WriteLine("4. Revisar estadísticas");
                Console.WriteLine("5. Guardar y salir");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        if (player.rod == null)
                        {
                            Console.WriteLine("No tienes una caña, ve a la tienda a comprar una y vuelve entonces!");
                            Console.ReadKey();
                            break;
                        }
                        fishingEngine.StartFishing();
                        break;
                    case "2":
                        shop.Enter();
                        break;
                    case "3":
                        Console.Clear();
                        player.inventory.Display();
                        break;
                    case "4":
                        Console.Clear();
                        player.stats.Show();
                        break;
                    case "5":
                        saveManager.Save(player);
                        Console.WriteLine("Adiós!");
                        return;
                    default:
                        Console.WriteLine("Elección equivocada, por favor elige un número entre 1 y 5");
                        break;
                }
            }
        }

        static string AssignName()
        {
            Console.WriteLine("¿Cómo quieres llamarte?");
            string name;
            try
            {
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("El nombre no puede estar vacío.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Entrada inválida: {ex.Message}. Se asignará un nombre aleatorio.");
                name = "Pescador";
            }

            return name;
        }
    }
}
