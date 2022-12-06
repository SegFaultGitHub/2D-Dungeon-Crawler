public class DamageMultiplier : CardEffect {
    private class DamageMultiplierCallback : OnCompute {
        private readonly float Ratio;

        public DamageMultiplierCallback(int priority, float ratio) {
            this.Priority = priority;
            this.Type = CallbackType.Damage;
            this.Ratio = ratio;
        }

        public override int Run(Character from, Character to, int value) {
            return (int) (value * this.Ratio);
        }
    }

    private int Priority = 0;
    private float Ratio;

    public DamageMultiplier(float ratio) {
        this.Ratio = ratio;
        this.Description = "Multiplies damage by " + this.GreenText(this.Ratio);
        this.Effects = new Effect[] { };
        this.EffectType = EffectType.Passive;
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new DamageMultiplierCallback(this.Priority, this.Ratio));
    }
}
