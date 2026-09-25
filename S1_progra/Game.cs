using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Promedio1.scripts
{
    internal class Game
    {
        Jugador _player;
        Random rand = new Random();
        List<Enemy> EnemyList = new List<Enemy>();
        Enemy test = new Enemy("", 0, 0);
        public void CreateCharacter(string p1, int p2, int p3)
        {
            _player = new Jugador(p1, p2, p3);
        }
        public void CreateEnemy(int x, int p1, int p2)
        {
            for (int i = 0; i < x; i++)
            {
                EnemyList.Add(test);
                EnemyList[i] = new Enemy("enemigo " + i, p1, p2);
            }
        }

        public bool Combat()
        {
            bool continueCombat = true;

            while (continueCombat)
            {
                if (EnemyList.Count == 0)
                {
                    Typewriter("\n¡Derrotaste a todos los enemigos!\n");
                    return true;
                }
                Typewriter($"\nEnemigos actuales: {EnemyList.Count}\n");
                Typewriter("¿Qué quieres hacer?\n");
                Typewriter("1.- Huir\n");
                Typewriter("2.- Atacar\n");
                Typewriter("3.- Ver tus stats\n");

                int answers = int.Parse(Console.ReadLine());

                switch (answers)
                {
                    case 1:
                        Typewriter("¡Escapaste del combate!\n");
                        //FinishGame();
                        return false;

                    case 2:
                        int indexObjetivo = rand.Next(0, EnemyList.Count);
                        Enemy target = EnemyList[indexObjetivo];

                        int playerDamage = _player.PerformAttack(target);

                        Typewriter("--------------------------------------------------------------\n");
                        Typewriter($"[JUGADOR] {_player.name} atacó a {target.name} e infligió {playerDamage} de daño.\n");
                        Typewriter($"  Vida restante de {target.name}: {target.life}\n");
                        Typewriter("--------------------------------------------------------------\n");

                        RemoveDeadEnemy();

                        if (EnemyList.Count == 0)
                        {
                            Typewriter("\n¡Derrotaste a todos los enemigos!\n");
                            return true;
                        }

                        EnemyCombat();

                        if (!_player.Dead())
                        {
                            Typewriter("\n¡Has muerto!\n");
                            FinishGame();
                            return false;
                        }
                        break;

                    case 3:
                        Show_PlayerStats();
                        break;
                }
            }

            return false;
        }

        void EnemyCombat()
        {
            Typewriter("\n--- Turno de los enemigos ---\n");
            foreach (Enemy enemy in EnemyList)
            {
                int playerHurt = enemy.PerformAttack(_player);

                Typewriter("--------------------------------------------------------------\n");
                Typewriter($"[ENEMIGO] {enemy.name} atacó a {_player.name} e infligió {playerHurt} de daño.\n");
                Typewriter($"  Tu vida actual: {_player.life}\n");
                Typewriter("--------------------------------------------------------------\n");

                if (!_player.Dead()) break;
            }
        }
        void RemoveDeadEnemy()
        {
            for (int i = EnemyList.Count - 1; i >= 0; i--)
            {
                if (!EnemyList[i].Dead())
                {
                    Typewriter($"  ☠ {EnemyList[i].name} ha sido derrotado!\n");
                    EnemyList.RemoveAt(i);
                }
            }
        }

        public void Show_PlayerStats()
        {
            Typewriter("-------------------------------------------------------------- \n");
            Typewriter($"Player..... Vida: {_player.life} / Daño: {_player.damage}\n");
            Typewriter("-------------------------------------------------------------- \n");
        }
        public void Show_Enemystats(Enemy enemy)
        {
            Typewriter("-------------------------------------------------------------- \n");
            Typewriter($"El enemigo..... recibió {_player.damage}\n");

            Typewriter($"La vida actual del enemigo es... {enemy.life}\n");
            Typewriter("-------------------------------------------------------------- \n");
            for (int i = 0; i < EnemyList.Count; i++) // revisamos si tenemos un enemigo muerto
            {
                if (EnemyList[i].Dead() == false)
                {
                    EnemyList.RemoveAt(i);
                    i -= 1;
                }
            }
        }
        public void Typewriter(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                Thread.Sleep(15);
            }
        }
        public void Route()
        {
            while (true)
            {
                Typewriter("A donde quieres ir? \n");

                Console.WriteLine("1.- Ir al castillo abandonado");
                Console.WriteLine("2.- Ir al bosque embrujado");
                Console.WriteLine("3.- Ir a la tierra de fuego");
                Console.WriteLine("4.- Ir al desierto");
                Console.WriteLine("5.- Ir a la ciudad");
                Console.WriteLine("6.- Ir a la caberna de mercenarios");
                Console.WriteLine("7.- Ir al muelle");
                Console.WriteLine("8.- Ir a la armeria");
                Console.WriteLine("9.- Ir a la fuente de poder");
                Console.WriteLine("10.- Ir a tu casa");

                int answer = int.Parse(Console.ReadLine());

                switch (answer)
                {
                    case 1:
                        Castle();
                        break;
                    case 2:
                        Woods();
                        break;
                    case 3:
                        Fire();
                        break;
                    case 4:
                        Desert();
                        break;
                    case 5:
                        City();
                        break;
                    case 6:
                        Bar();
                        break;
                    case 7:
                        Bridge();
                        break;
                    case 8:
                        Lake();
                        break;
                    case 9:
                        fountain();
                        break;
                    case 10:
                        home();
                        break;
                }
            }
        }
        private void Castle()
        {
            Typewriter("Camino al castillo te encuentras con un perro lastimado en una trampa\n");
            Typewriter("Qué quieres hacer? \n");
            while (true)
            {
                Typewriter("1.- Ayudarlo \n");
                Typewriter("2.- Atacarlo \n");

                Typewriter("3.- Ver tus stats \n");
                int answer = int.Parse(Console.ReadLine());
                switch (answer)
                {
                    case 1:
                        Console.WriteLine("Ayudaste al perro, y en recompensa, sacude su cola y te guía hacía un arbol donde hay un tesoro enterrado!");
                        Console.WriteLine("Ganaste un tesoro enterrado!");
                        FinishGame();
                        break;
                    case 2:
                        Console.WriteLine("sacas tu espada para terminarlo, pero en eso, más perros salen del arbusto y te muerden hasta acabarte.");
                        Console.WriteLine("Has muerto");
                        FinishGame();
                        break;

                    case 3:
                        Show_PlayerStats();
                        break;
                }
            }
        }
        private void Woods()
        {
            Typewriter("Decides ir al bosque... ¡te aparecen 3 enemigos!\n");
            CreateEnemy(3, 20, 5);

            bool victory = Combat();

            if (victory)
                Typewriter("\n¡Sobreviviste el bosque! Pero no has encontrado un tesoro aún, puedes continuar tu viaje.\n");
            else
                Typewriter("\nFuiste derrotado en el bosque...\n");
            FinishGame();
        }
        private void Fire()
        {
            Typewriter("Decides ir cerca al volcán, en el camino encuentras que hay rastros de fuego que van fuera de la ruta.\n");
            Typewriter("te acercas a revisar...Resultaron ser 4 enemigos de fuego atacando a un humano!\n");
            Typewriter("Qué quieres hacer? \n");

            while (true)
            {
                Typewriter("1.- Huir \n");
                Typewriter("2.- Ayudar \n");

                Typewriter("3.- Ver tus stats \n");
                int answer = int.Parse(Console.ReadLine());
                switch (answer)
                {
                    case 1:
                        Typewriter("¡Escapaste del combate!\n");
                        Route();
                        break;
                    case 2:
                        Typewriter("Te acercas con tu espada, y empieza el combate");
                        CreateEnemy(4, 25, 10);

                        bool victory = Combat();

                        if (victory)
                            Typewriter("\n¡Sobreviviste el combate y salvaste al habitante!. En compensa te dice sobre un tesoro cerca de la fuente \n");
                        else
                            Typewriter("\nFuiste derrotado.\n");
                        FinishGame();
                        break;

                    case 3:
                        Show_PlayerStats();
                        break;
                }
            }
        }
        private void Desert()
        {
            Typewriter("Vas al desierto, durante el viaje encuentras un caliz de oro entre la arena, parece que encontraste tu tesoro...\n");
            Typewriter("¡Pero se te aparecen unos ladrones y te lo arrebatan.\n");
            CreateEnemy(2, 40, 5);

            bool victory = Combat();

            if (victory)
                Typewriter("\n¡Sobreviviste al ataque! Pero un caliz no es suficiente, hay que seguir buscando!.\n");
            else
                Typewriter("\nFuiste derrotado por los ladrones...\n");
            FinishGame();
        }
        private void City()
        {
            Typewriter("Al llegar a la ciudad te encuetras con unos amigos caballeros descansando.\n");
            Typewriter("Qué quieres hacer? \n");
            while (true)
            {
                Typewriter("1.- Preguntar por un tesoro \n");
                Typewriter("2.- Pedir una misión \n");
                int answer = int.Parse(Console.ReadLine());
                switch (answer)
                {
                    case 1:
                        Typewriter("Les preguntas si saben de algún tesoro escondido, te recomiendan ir al bosque a buscar, pero no estáan seguros");
                        Typewriter("De todas maneras lo consideras");
                        break;
                    case 2:
                        Typewriter("Pides una misión de combate (esperando una recompensa)");
                        Typewriter("Te dicen sobre unos duendes que están por el puente atacando a los habitantes");
                        Typewriter("decides ir a revisar");
                        break;
                }
            }
        }
        private void Bar()
        {
            Typewriter("llegas al bar, y le preguntas al mesero si sabe algún tesoro cercano\n");
            Typewriter("Te recomienda ir al puente, ya que los duendes que viven ahí suelen tener tesoros guardados \n");
            Typewriter("Te tomas unos tragos y lo consideras \n");
        }
        private void Bridge()
        {
            Typewriter("LLegas al puente, y ves un grupo de 5 duendes atacando unas personas\n");
            CreateEnemy(5, 15, 8);

            bool victory = Combat();

            if (victory)
                Typewriter("\n Lograste derrotarlos! Y en su guarida encuentras todo lo que habian robado, oro y joyas. Ganaste!!.\n");
            else
                Typewriter("\nFuiste derrotado en el puente...\n");
            FinishGame();
        }
        private void Lake()
        {
            Typewriter("Fuiste por el lago y notas un brillo particular en el fondo...\n");
            Typewriter("Tratas de aacercarte, pero salen unos tiburones que tratan de comerte.\n");
            CreateEnemy(3, 30, 15);

            bool victory = Combat();

            if (victory)
                Typewriter("\n¡Sobreviviste al ataque! Pero lo que brillaba solo era una bolsa de monedas, hay que seguir buscando!.\n");
            else
                Typewriter("\nFuiste derrotado por los tiburones...\n");
            FinishGame();
        }
        private void fountain()
        {
            Typewriter("Los rumores no mentían... te acercas a la fuente y encuentras un tesoro en el fondo\n");
            Typewriter(" Abres el cofre... Pero no contenía joyas, se trataba de un cofre Mimico! \n");
            Typewriter("Te acercaste demasiado y fuiste comido! D: \n");
            FinishGame();
        }
        private void home()
        {
            Typewriter("Vas a de regreso a tu casa, pero no hay mucho que ver...\n");
            Typewriter("tal vez deberías buscar un tesoro en otro lugar... O podrias irte a domir y continuar luego \n");
            Typewriter("Qué quieres hacer? \n");
            while (true)
            {
                Typewriter("1.- Volver y buscar en otro lugar \n");
                Typewriter("2.- Dormir un rato \n");
                int answer = int.Parse(Console.ReadLine());
                switch (answer)
                {
                    case 1:
                        Typewriter("Regresas a elegir otro punto para buscar");
                        break;
                    case 2:
                        Typewriter("Decides salir otro día a buscar el tesoro");
                        Typewriter("Te vas a domir por un tiempo indefinido");
                        FinishGame();
                        break;
                }
            }
        }
        public void FinishGame()
        {
            Typewriter("\n------------------------------------");
            Typewriter("EL JUEGO HA TERMINADO.");
            Typewriter("Presiona cualquier tecla para salir...");
            Typewriter("------------------------------------n");
            Console.ReadKey();
        }
    }
}

