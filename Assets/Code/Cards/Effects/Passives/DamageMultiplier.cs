using System.Collections.Generic;
using static Enemy;

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
        this.Effects = new Effect[] { Effect.Damage };
        this.EffectType = EffectType.Passive;
    }

    public override void UpdateDescription(Player player) {
        this.Description = string.Format("Multiplies damage by {0}", this.BlueText(this.Ratio));
        if (this.Duration != null)
            this.Description += string.Format(" ({0} turns)", this.BlueText((int) this.Duration));
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new DamageMultiplierCallback(this, this.Ratio), this.Duration);
    }

    public override List<CardSimulationEffect> Simulate(Character from, Character to) {
        return new();
    }
}
