public abstract class Character : IInterfaceLife
{
    public int life;
    public int damage;
    public string name;

    protected Character(string name, int life, int damage)
    {
        this.name = name;
        this.life = life;
        this.damage = damage;
    }

    public void Damage(int hit)
    {
        life -= hit;

        if (life < 0)
        {
            life = 0;
        }
    }

    public bool Dead()
    {
        return life <= 0;
    }

    public virtual int PerformAttack(IInterfaceLife target)
    {
        target.Damage(damage);
        return damage;
    }
}