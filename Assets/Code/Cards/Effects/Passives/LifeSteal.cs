public class LifeSteal : CardEffect {
    private class LifeStealCallback : OnApplied {
        private readonly float Ratio;

        public LifeStealCallback(CardEffect cardEffect, float ratio) : base(0, CallbackType.Damage, cardEffect.Effects, cardEffect.Description) {
            this.Ratio = ratio;
        }

        public override int Run(Character from, Character _, int value) {
            from.Foo(CallbackType.Heal, from, from, (int) (value * this.Ratio), this.Priority);
            return value;
        }
    }

    private float Ratio;
    private int? Duration;

    public LifeSteal(float ratio, int? duration) {
        this.Ratio = ratio;
        this.Duration = duration;
        this.Effects = new Effect[] { Effect.Heal };
        this.EffectType = EffectType.Passive;
    }

    public override void UpdateDescription(Player player) {
        this.Description = string.Format("Steals {0}% of the inflicted damage", this.GreenText((int) (this.Ratio * 100)));
        if (this.Duration != null)
            this.Description += string.Format(" ({0} turns)", this.BlueText((int) this.Duration));
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new LifeStealCallback(this, this.Ratio), this.Duration);
    }
}
