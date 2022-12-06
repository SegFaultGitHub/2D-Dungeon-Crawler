public class PoisonOnDamage : CardEffect {
    private class PoisonOnDamageCallback : OnApply {
        private readonly int Poison;

        public PoisonOnDamageCallback(int priority, int damage) {
            this.Priority = priority;
            this.Type = CallbackType.Damage;
            this.Poison = damage;
        }

        public override int Run(Character from, Character to, int value) {
            from.Foo(CallbackType.Poison, from, to, this.Poison, this.Priority);
            return value;
        }
    }

    private int Priority = 3;
    private int Poison;

    public PoisonOnDamage(int poison) {
        this.Poison = poison;
        this.Description = "Applies " + this.GreenText(this.Poison) + " poison when inflicting damage";
        this.Effects = new Effect[] { Effect.Poison };
        this.EffectType = EffectType.Passive;
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new PoisonOnDamageCallback(this.Priority, this.Poison));
    }
}
