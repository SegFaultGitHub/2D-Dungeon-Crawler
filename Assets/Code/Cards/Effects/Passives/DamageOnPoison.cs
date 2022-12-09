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

    private int Value;
    private int? Duration;

    public DamageOnPoison(int damage, int? duration) {
        this.Value = damage;
        this.Duration = duration;
        this.Effects = new Effect[] { Effect.Damage };
        this.EffectType = EffectType.Passive;
    }

    public override void UpdateDescription(Player player) {
        int value = player.Compute(CallbackType.Damage, player, null, this.Value, 3);
        if (value > this.Value) {
            this.Description = string.Format("Inflicts {0} ({1}) damage when applying a poison", this.GreenText(value), this.BlueText(this.Value));
        } else if (value < this.Value) {
            this.Description = string.Format("Inflicts {0} ({1}) damage when applying a poison", this.RedText(value), this.BlueText(this.Value));
        } else {
            this.Description = string.Format("Inflicts {0} damage when applying a poison", this.BlueText(value));
        }
        if (this.Duration != null)
            this.Description += string.Format(" ({0} turns)", this.BlueText((int) this.Duration));
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new DamageOnPoisonCallback(this, this.Value), this.Duration);
    }
}
