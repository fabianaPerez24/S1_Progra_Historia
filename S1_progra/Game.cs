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
        Player _player;
        Random rand = new Random();
        private Nodo nodoActual;
        private bool juegoTerminado = false;

        private Nodo inicio;
        private Nodo castillo;
        private Nodo bosque;
        private Nodo fuego;
        private Nodo desierto;
        private Nodo ciudad;
        private Nodo bar;
        private Nodo puente;
        private Nodo lago;
        private Nodo fuente;
        private Nodo casa;

        private Historial inicioHistorial;
        private Historial ultimoHistorial;

        private Pila pilaLugares = new Pila();
        private Cola colaEnemigos = new Cola();

        private ArbolBinario arbolLugares = new ArbolBinario();

        public Game(Player player)
        {
            _player = player;
            CrearNodos();
            nodoActual = inicio;

            AgregarHistorial(nodoActual.Nombre);
            pilaLugares.Agregar(nodoActual.Nombre);
            arbolLugares.Agregar(nodoActual.Nombre);
        }
        public int LeerNumero()
        {
            while (true)
            {
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Typewriter("Entrada inválida. Debes escribir un número.\n");
                }
                catch (OverflowException)
                {
                    Typewriter("El número es demasiado grande.\n");
                }
            }
        }
        List<Enemy> EnemyList = new List<Enemy>();
        public void CreateCharacter(string p1, int p2, int p3)
        {
            _player = new Player(p1, p2, p3);
        }
        public void CreateEnemy(int x, int p1, int p2, string tipo = "normal")
        {
            EnemyList.Clear();

            for (int i = 0; i < x; i++)
            {
                if (tipo == "goblin")
                {
                    EnemyList.Add(new Goblin("Goblin " + (i + 1), p1, p2));
                }
                else
                {
                    EnemyList.Add(new Enemy("Enemigo " + (i + 1), p1, p2));
                }
                colaEnemigos.Agregar(EnemyList[i].name);
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

                int answers = LeerNumero();

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

                        if (_player.Dead())
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
                int playerHurt = enemy.Attack(_player);

                Typewriter("--------------------------------------------------------------\n");
                Typewriter($"[ENEMIGO] {enemy.name} atacó a {_player.name} e infligió {playerHurt} de daño.\n");
                Typewriter($"  Tu vida actual: {_player.life}\n");
                Typewriter("--------------------------------------------------------------\n");

                if (_player.Dead())
                    break;
            }
        }
        void RemoveDeadEnemy()
        {
            for (int i = EnemyList.Count - 1; i >= 0; i--)
            {
                if (EnemyList[i].Dead())
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
            Typewriter($"Oro........ {_player.Oro}\n");
            Typewriter("-------------------------------------------------------------- \n");

            Typewriter("Inventario:\n");

            if (_player.Inventario.Count == 0)
            {
                Typewriter("  (Vacío)\n");
            }
            else
            {
                foreach (Item item in _player.Inventario)
                {
                    Typewriter($"  - {item.Nombre}: {item.Descripcion}\n");
                }
            }

            MostrarHistorial();
            
            Typewriter("-------------------------------------------------------------- \n");
            bool tienePocion = false;

            foreach (Item item in _player.Inventario)
            {
                if (item.Nombre == "Poción")
                {
                    tienePocion = true;
                    break;
                }
            }

            if (tienePocion)
            {
                Typewriter("\n¿Quieres usar una poción?\n");
                Typewriter("1.- Sí\n");
                Typewriter("2.- No\n");

                int opcion = LeerNumero();

                if (opcion == 1)
                {
                    UsePotion();
                }
            }

            Typewriter("-------------------------------------------------------------- \n");
        }
        private void AgregarHistorial(string lugar)
        {
            Historial nuevo = new Historial(lugar);

            if (inicioHistorial == null)
            {
                inicioHistorial = nuevo;
                ultimoHistorial = nuevo;
            }
            else
            {
                ultimoHistorial.Siguiente = nuevo;
                ultimoHistorial = nuevo;
            }
        }
        public void MostrarHistorial()
        {
            Typewriter("\n--- Lugares visitados ---\n");

            Historial actual = inicioHistorial;

            while (actual != null)
            {
                Typewriter(actual.Lugar);

                if (actual.Siguiente != null)
                    Typewriter(" -> ");

                actual = actual.Siguiente;
            }

            Typewriter("\n");
        }
        public void GiveReward(string nombre, string descripcion, int oro)
        {
            Item item = new Item(nombre, descripcion);

            _player.Inventario.Add(item);
            _player.Oro += oro;

            Typewriter("\n¡Obtuviste una recompensa!\n");
            Typewriter($"Objeto: {nombre}\n");
            Typewriter($"Oro recibido: {oro}\n");
        }
        public void GivePotion(int curacion)
        {
            Pocion potion = new Pocion(
                "Poción",
                $"Recupera {curacion} puntos de vida.",
                curacion
            );

            _player.Inventario.Add(potion);

            Typewriter("\n¡Has recibido una poción!\n");
            Typewriter($"La poción recupera {curacion} puntos de vida.\n");
        }
        public Item BuscarItem(string nombre)
        {
            return _player.Inventario.Find(item => item.Nombre == nombre);
        }
        public Enemy BuscarEnemigoVivo()
        {
            return EnemyList.Find(enemy => !enemy.Dead());
        }
        
        public void UsePotion()
        {
            Item potion = BuscarItem("Poción");
            if (potion != null)
            {
                _player.life += 20;

                if (_player.life > 100)
                    _player.life = 100;

                _player.Inventario.Remove(potion);

                Typewriter("\nUsaste una poción y recuperaste 20 de vida.\n");
            }
            else
            {
                Typewriter("\nNo tienes ninguna poción.\n");
            }
        }

        public void Show_Enemystats(Enemy enemy)
        {
            Typewriter("-------------------------------------------------------------- \n");
            Typewriter($"El enemigo..... recibió {_player.damage}\n");

            Typewriter($"La vida actual del enemigo es... {enemy.life}\n");
            Typewriter("-------------------------------------------------------------- \n");
            for (int i = 0; i < EnemyList.Count; i++) 
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
            RecorrerNodos();
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
                int answer = LeerNumero();
                switch (answer)
                {
                    case 1:
                        Console.WriteLine("Ayudaste al perro, y en recompensa, sacude su cola y te guía hacía un arbol donde hay un tesoro enterrado!");
                        Console.WriteLine("Ganaste un tesoro enterrado!");
                        GiveReward("Tesoro enterrado", "Un tesoro encontrado gracias al perro.", 100);
                        FinishGame();
                        return;
                    case 2:
                        Console.WriteLine("sacas tu espada para terminarlo, pero en eso, más perros salen del arbusto y te muerden hasta acabarte.");
                        Console.WriteLine("Has muerto");
                        FinishGame();
                        return;

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
            {
                Typewriter("\n¡Sobreviviste el bosque! Pero no has encontrado un tesoro aún, puedes continuar tu viaje.\n");
                GivePotion(20);
            }
            else
                Typewriter("\nFuiste derrotado en el bosque...\n");
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
                int answer = LeerNumero();
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
                       return;

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
                int answer = LeerNumero();
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
            CreateEnemy(5, 15, 8, "goblin");

            bool victory = Combat();

            if (victory)
            {
                Typewriter("\nLograste derrotarlos! Y en su guarida encuentras todo lo que habían robado, oro y joyas.\n");

                GiveReward("Joyas robadas", "Joyas recuperadas de la guarida de los duendes.", 150);
            }
            else
            {
                Typewriter("\nFuiste derrotado en el puente...\n");
            }
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
                int answer = LeerNumero();
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
            Typewriter("\n------------------------------------\n");
            Typewriter("EL JUEGO HA TERMINADO.\n");
            Typewriter("Presiona cualquier tecla para salir...\n");
            Typewriter("------------------------------------\n");

            juegoTerminado = true;

            Console.ReadKey();
        }
        private void CrearNodos()
        {
            inicio = new Nodo(
                "Inicio",
                "Te encuentras frente a un camino misterioso."
            );

            castillo = new Nodo(
                "Castillo",
                "Un enorme castillo se alza frente a ti."
            );

            bosque = new Nodo(
                "Bosque",
                "Un bosque oscuro y silencioso."
            );

            fuego = new Nodo(
                "Tierra de Fuego",
                "El calor es intenso y el suelo está cubierto de cenizas."
            );

            desierto = new Nodo(
                "Desierto",
                "Un desierto interminable se extiende frente a ti."
            );

            ciudad = new Nodo(
                "Ciudad",
                "Llegas a una ciudad llena de personas."
            );

            bar = new Nodo(
                "Bar",
                "Un viejo bar se encuentra en una esquina."
            );

            puente = new Nodo(
                "Puente",
                "Un viejo puente cruza un enorme río."
            );

            lago = new Nodo(
                "Lago",
                "Un lago tranquilo refleja la luz del cielo."
            );

            fuente = new Nodo(
                "Fuente",
                "Encuentras una antigua fuente de piedra."
            );

            casa = new Nodo(
                "Casa",
                "Una pequeña casa se encuentra al final del camino."
            );

            castillo.Accion = () => Castle();
            bosque.Accion = () => Woods();
            fuego.Accion = () => Fire();
            desierto.Accion = () => Desert();
            ciudad.Accion = () => City();
            bar.Accion = () => Bar();
            puente.Accion = () => Bridge();
            lago.Accion = () => Lake();
            fuente.Accion = () => fountain();
            casa.Accion = () => home();

            inicio.AgregarOpcion(castillo);
            inicio.AgregarOpcion(bosque);
            inicio.AgregarOpcion(fuego);

            castillo.AgregarOpcion(desierto);
            castillo.AgregarOpcion(ciudad);

            bosque.AgregarOpcion(puente);
            bosque.AgregarOpcion(lago);

            fuego.AgregarOpcion(desierto);

            desierto.AgregarOpcion(ciudad);

            ciudad.AgregarOpcion(bar);
            ciudad.AgregarOpcion(fuente);

            bar.AgregarOpcion(puente);

            puente.AgregarOpcion(lago);

            lago.AgregarOpcion(fuente);

            fuente.AgregarOpcion(casa);
        }
        private void MostrarNodoActual()
        {
            Typewriter("\n====================================\n");
            Typewriter(nodoActual.Nombre + "\n");
            Typewriter("====================================\n");
            Typewriter(nodoActual.Descripcion + "\n\n");

            for (int i = 0; i < nodoActual.Opciones.Count; i++)
            {
                Typewriter($"{i + 1}. Ir a {nodoActual.Opciones[i].Nombre}\n");
            }
        }
        private void RecorrerNodos()
        {
            while (nodoActual != null)
            {
                MostrarNodoActual();

                if (nodoActual.Opciones.Count == 0)
                {
                    Typewriter("\nHas llegado al final de este camino.\n");
                    break;
                }

                Typewriter("\nElige una opción: ");

                int opcion = LeerNumero();

                if (opcion >= 1 && opcion <= nodoActual.Opciones.Count)
                {
                    nodoActual = nodoActual.Opciones[opcion - 1];
                    AgregarHistorial(nodoActual.Nombre);
                    pilaLugares.Agregar(nodoActual.Nombre);
                    arbolLugares.Agregar(nodoActual.Nombre);

                    if (nodoActual.Accion != null)
                    {
                        nodoActual.Accion();
                    }

                    if (juegoTerminado)
                    {
                        break;
                    }
                }
                else
                {
                    Typewriter("\nOpción inválida.\n");
                }
            }
        }
    }
}

