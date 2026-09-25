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
        }
        public bool Dead()
        {
            if (life <= 0)
            {
                return false;
            }
            else return true;
        }
        internal int PerformAttack(IInterfaceLife target)
        {
            target.Damage(damage);
            return damage;
        }
    }

