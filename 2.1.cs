using System;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Estacionamiento estacionamiento = new Estacionamiento();

        while (true)
        {
            Console.WriteLine("Menú:");
            Console.WriteLine("1. Listar todos los vehículos");
            Console.WriteLine("2. Agregar un nuevo vehículo");
            Console.WriteLine("3. Remover un vehículo por matrícula");
            Console.WriteLine("4. Remover un vehículo por DNI del dueño");
            Console.WriteLine("5. Remover una cantidad aleatoria de vehículos");
            Console.WriteLine("6. Optimizar el espacio");
            Console.WriteLine("7. Salir");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    estacionamiento.listarTodos();
                    break;
                case "2":
                    estacionamiento.añadirVehiculoRandom();
                    break;
                case "3":
                    Console.Write("Ingrese la matrícula del vehículo a remover: ");
                    string patente = Console.ReadLine();
                    estacionamiento.eliminarPorPatente(patente);
                    break;
                case "4":
                    Console.Write("Ingrese el DNI del dueño del vehículo a remover: ");
                    string dni = Console.ReadLine();
                    estacionamiento.eliminarPorDNI(dni);
                    break;
                case "5":
                    Console.Write("Ingrese la cantidad de vehículos a remover: ");
                    int cantidad = int.Parse(Console.ReadLine());
                    estacionamiento.eliminarRandom(cantidad);
                    break;
                case "6":
                    estacionamiento.optimizar();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }
    }
}

class Vehiculo
{
    public string Modelo { get; set; }
    public string DueñoDNI { get; set; }
    public string Patente { get; set; }
    public double Largo { get; set; }
    public double Ancho { get; set; }
}

class Estacionamiento
{
    private List<Vehiculo> regular = new List<Vehiculo>();
    private List<Vehiculo> quantum = new List<Vehiculo>();
    private Random random = new Random();

    public void listarTodos()
    {
        Console.WriteLine("Vehículos en el estacionamiento regular:");
        foreach (var vehiculo in regular)
        {
            Console.WriteLine($"Modelo: {vehiculo.Modelo}, Matrícula: {vehiculo.Patente}, Dueño: {vehiculo.DueñoDNI}");
        }

        Console.WriteLine("Vehículos en el estacionamiento cuántico:");
        foreach (var vehiculo in quantum)
        {
            Console.WriteLine($"Modelo: {vehiculo.Modelo}, Matrícula: {vehiculo.Patente}, Dueño: {vehiculo.DueñoDNI}");
        }
    }

    public void añadirVehiculoRandom()
    {
        string[] modelos = { "Modelo1", "Modelo2", "Modelo3" };
        string[] dueños = { "DNI1", "DNI2", "DNI3" };
        double randomLargo = random.NextDouble() * 5 + 1;
        double randomAncho = random.NextDouble() * 0.5 + 1.5;

        Vehiculo vehicle = new Vehiculo
        {
            Modelo = modelos[random.Next(modelos.Length)],
            DueñoDNI = dueños[random.Next(dueños.Length)],
            Patente = Guid.NewGuid().ToString().Substring(0, 8),
            Largo = randomLargo,
            Ancho = randomAncho
        };

        if (random.Next(2) == 0)
        {
            regular.Add(vehicle);
        }
        else
        {
            quantum.Add(vehicle);
        }

        Console.WriteLine("Vehículo agregado con éxito.");
    }

    public void eliminarPorPatente(string vehiculoPatente)
    {
        Vehiculo vehicleToRemove = regular.Find(v => v.Patente == vehiculoPatente);
        if (vehicleToRemove != null)
        {
            regular.Remove(vehicleToRemove);
            Console.WriteLine($"Vehículo con matrícula {vehiculoPatente} removido del estacionamiento regular.");
            return;
        }

        vehicleToRemove = quantum.Find(v => v.Patente == vehiculoPatente);
        if (vehicleToRemove != null)
        {
            quantum.Remove(vehicleToRemove);
            Console.WriteLine($"Vehículo con matrícula {vehiculoPatente} removido del estacionamiento cuántico.");
        }
        else
        {
            Console.WriteLine($"Vehículo con matrícula {vehiculoPatente} no encontrado.");
        }
    }

    public void eliminarPorDNI(string dni)
    {
        Vehiculo vehicleToRemove = regular.Find(v => v.DueñoDNI == dni);
        if (vehicleToRemove != null)
        {
            regular.Remove(vehicleToRemove);
            Console.WriteLine($"Vehículo del dueño con DNI {dni} removido del estacionamiento regular.");
            return;
        }

        vehicleToRemove = quantum.Find(v => v.DueñoDNI == dni);
        if (vehicleToRemove != null)
        {
            quantum.Remove(vehicleToRemove);
            Console.WriteLine($"Vehículo del dueño con DNI {dni} removido del estacionamiento cuántico.");
        }
        else
        {
            Console.WriteLine($"Vehículo del dueño con DNI {dni} no encontrado.");
        }
    }

    public void eliminarRandom(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            if (regular.Count > 0 && random.Next(2) == 0)
            {
                regular.RemoveAt(random.Next(regular.Count));
            }
            else if (quantum.Count > 0)
            {
                quantum.RemoveAt(random.Next(quantum.Count));
            }
        }

        Console.WriteLine($"{cantidad} vehículos removidos aleatoriamente.");
    }

    public void optimizar()
    {
        List<Vehiculo> noOptimizados = new List<Vehiculo>();
        noOptimizados.AddRange(regular);
        noOptimizados.AddRange(quantum);

        regular.Clear();
        quantum.Clear();

        foreach (var vehiculo in noOptimizados)
        {
            if (vehiculo.Largo <= 5 && vehiculo.Ancho <= 2)
            {
                regular.Add(vehiculo);
            }
            else
            {
                quantum.Add(vehiculo);
            }
        }

        Console.WriteLine("Espacio optimizado con éxito.");
    }
}

