using Promedio1.scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1_progra
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("bienvenido a este juego");
            Console.WriteLine("Crea tu personaje:");
            Console.WriteLine("introduce tu nombre:");

            string p1 = Console.ReadLine();

            Console.WriteLine("introduce tu vida (1-100):");
            int p2 = LeerNumero();

            while (p2 < 1 || p2 > 100)
            {
                Console.WriteLine("La vida debe estar entre 1 y 100. Intenta nuevamente:");
                p2 = LeerNumero();
            }

            Console.WriteLine("introduce tu daño (1-100):");
            int p3 = LeerNumero();

            while (p3 < 1 || p3 > 100)
            {
                Console.WriteLine("El daño debe estar entre 1 y 100. Intenta nuevamente:");
                p3 = LeerNumero();
            }

            Player player = new Player(p1, p2, p3);

            Game game = new Game(player);

            game.Typewriter("Eres un caballero y estas en un viaje buscando un tesoro. Y tienes 10 opciones de calabozos: \n");

            game.Route();
        }

        static int LeerNumero()
        {
            while (true)
            {
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entrada inválida. Debes escribir un número.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("El número es demasiado grande.");
                }
            }
        }
    }
}