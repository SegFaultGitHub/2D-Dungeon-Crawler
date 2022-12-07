public class DamageOnPoison : CardEffect {
    private class DamageOnPoisonCallback : OnApply {
        private readonly int Damage;

        public DamageOnPoisonCallback(CardEffect cardEffect, int damage) : base(3, CallbackType.Poison, cardEffect.Effects, cardEffect.Description) {
            this.Damage = damage;
        }

        public override int Run(Character from, Character to, int value) {
            from.Foo(CallbackType.Damage, from, to, this.Damage, this.Priority);
            return value;
        }
    }

    private int Damage;
    private int? Duration;

    public DamageOnPoison(int damage, int? duration) {
        this.Damage = damage;
        this.Duration = duration;
        this.Description = "Inflicts " + this.GreenText(this.Damage) + " damage when applying a poison";
        if (this.Duration != null)
            this.Description += " (" + this.GreenText((int) this.Duration) + " turns)";
        this.Effects = new Effect[] { Effect.Damage };
        this.EffectType = EffectType.Passive;
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new DamageOnPoisonCallback(this, this.Damage), this.Duration);
    }
}
