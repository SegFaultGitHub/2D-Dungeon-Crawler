public class PoisonOnDamage : CardEffect {
    private class PoisonOnDamageCallback : OnApply {
        private readonly int Poison;

        public PoisonOnDamageCallback(CardEffect cardEffect, int poison) : base(3, CallbackType.Damage, cardEffect.Effects, cardEffect.Description) {
            this.Poison = poison;
        }

        public override int Run(Character from, Character to, int value) {
            from.Foo(CallbackType.Poison, from, to, this.Poison, this.Priority);
            return value;
        }
    }

    private int Poison;
    private int? Duration;

    public PoisonOnDamage(int poison, int? duration) {
        this.Poison = poison;
        this.Duration = duration;
        this.Description = "Applies " + this.GreenText(this.Poison) + " poison when inflicting damage";
        if (this.Duration != null)
            this.Description += " (" + this.GreenText((int) this.Duration) + " turns)";
        this.Effects = new Effect[] { Effect.Poison };
        this.EffectType = EffectType.Passive;
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new PoisonOnDamageCallback(this, this.Poison), this.Duration);
    }
}
