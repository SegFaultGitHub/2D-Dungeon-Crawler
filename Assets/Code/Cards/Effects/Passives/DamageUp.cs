public class DamageUp : CardEffect {
    private class DamageUpCallback : OnCompute {
        private readonly int Value;

        public DamageUpCallback(CardEffect cardEffect, int value) : base(0, CallbackType.Damage, cardEffect.Effects, cardEffect.Description) {
            this.Value = value;
        }

        public override int Run(Character from, Character to, int value) {
            return value + this.Value;
        }
    }

    private int Value;
    private int? Duration;

    public DamageUp(int value, int? duration) {
        this.Value = value;
        this.Duration = duration;
        this.Description = "Adds " + this.GreenText(this.Value) + " damage";
        if (this.Duration != null)
            this.Description += " (" + this.GreenText((int) this.Duration) + " turns)";
        this.Effects = new Effect[] { };
        this.EffectType = EffectType.Passive;
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new DamageUpCallback(this, this.Value), this.Duration);
    }
}
