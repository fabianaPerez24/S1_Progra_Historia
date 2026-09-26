using System;

internal class Goblin : Enemy
{
    public Goblin(string name, int life, int damage)
        : base(name, life, damage)
    {
    }

    public override int Attack(Character target)
    {
        Random random = new Random();

        if (random.Next(1, 6) == 1)
        {
            return PerformAttack(target) + 5;
        }

        return PerformAttack(target);
    }
}