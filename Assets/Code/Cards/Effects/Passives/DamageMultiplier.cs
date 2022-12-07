public class DamageMultiplier : CardEffect {
    private class DamageMultiplierCallback : OnCompute {
        private readonly float Ratio;

        public DamageMultiplierCallback(CardEffect cardEffect, float ratio) : base(0, CallbackType.Damage, cardEffect.Effects, cardEffect.Description) {
            this.Ratio = ratio;
        }

        public override int Run(Character from, Character to, int value) {
            return (int) (value * this.Ratio);
        }
    }

    private float Ratio;
    private int? Duration;

    public DamageMultiplier(float ratio, int? duration) {
        this.Ratio = ratio;
        this.Duration = duration;
        this.Description = "Multiplies damage by " + this.GreenText(this.Ratio);
        if (this.Duration != null)
            this.Description += " (" + this.GreenText((int) this.Duration) + " turns)";
        this.Effects = new Effect[] { };
        this.EffectType = EffectType.Passive;
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new DamageMultiplierCallback(this, this.Ratio), this.Duration);
    }
}
