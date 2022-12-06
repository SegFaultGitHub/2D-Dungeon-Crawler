public class DamageOnPoison : CardEffect {
    private class DamageOnPoisonCallback : OnApply {
        private readonly int Damage;

        public DamageOnPoisonCallback(int priority, int damage) {
            this.Priority = priority;
            this.Type = CallbackType.Poison;
            this.Damage = damage;
        }

        public override int Run(Character from, Character to, int value) {
            from.Foo(CallbackType.Damage, from, to, this.Damage, this.Priority);
            return value;
        }
    }

    private int Priority = 3;
    private int Damage;

    public DamageOnPoison(int damage) {
        this.Damage = damage;
        this.Description = "Inflicts " + this.GreenText(this.Damage) + " damage when applying a poison";
        this.Effects = new Effect[] { Effect.Damage };
        this.EffectType = EffectType.Passive;
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new DamageOnPoisonCallback(this.Priority, this.Damage));
    }
}
