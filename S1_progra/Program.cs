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
            Game game = new Game();
            game.Typewriter("bienvenido a este juego \n");
            game.Typewriter("Crea tu personaje: \n");
            game.Typewriter("introduce tu nombre: \n");

            string p1 = (Console.ReadLine());

            game.Typewriter("introduce tu vida: \n");
            int p2 = int.Parse(Console.ReadLine());
            if (p2 > 100) p2 = 0;

            game.Typewriter("introduce tu daño: \n");
            int p3 = int.Parse(Console.ReadLine());
            if (p3 > 100) p3 = 0;

            game.CreateCharacter(p1, p2, p3); // p1 = 5

            game.Typewriter("Eres un caballero y estas en un viaje buscando un tesoro. Y tienes 10 opciones de calabozos: \n");
            game.Route();
        }
    }
}
