public class LifeSteal : CardEffect {
    private class LifeStealCallback : OnApplied {
        private readonly float Ratio;

        public LifeStealCallback(int priority, float ratio) {
            this.Priority = priority;
            this.Type = CallbackType.Damage;
            this.Ratio = ratio;
        }

        public override int Run(Character from, Character _to, int value) {
            from.Foo(CallbackType.Heal, from, from, (int) (value * this.Ratio), this.Priority);
            return value;
        }
    }

    private int Priority = 0;
    private float Ratio;

    public LifeSteal(float ratio) {
        this.Ratio = ratio;
        this.Description = "Steals " + this.GreenText((int) (this.Ratio * 100)) + "% of the inflicted damage";
        this.Effects = new Effect[] { Effect.Heal };
        this.EffectType = EffectType.Passive;
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new LifeStealCallback(this.Priority, this.Ratio));
    }
}
