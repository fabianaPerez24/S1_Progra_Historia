internal class Enemy : Character
{
    public Enemy(string name, int life, int damage) : base(name, life, damage)
    {
    }

    public virtual int Attack(Character target)
    {
        return PerformAttack(target);
    }
}