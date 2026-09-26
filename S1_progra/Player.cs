using Promedio1.scripts;
using System.Collections.Generic;

internal class Player : Character
{
    public List<Item> Inventario { get; set; }

    public int Oro { get; set; }

    public Player(string name, int life, int damage) : base(name, life, damage)
    {
        Inventario = new List<Item>();
        Oro = 0;
    }

}