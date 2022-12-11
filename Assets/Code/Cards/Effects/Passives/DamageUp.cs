using System.Collections.Generic;
using System.Linq;
using static Enemy;

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
        this.Effects = new Effect[] { Effect.Damage };
        this.EffectType = EffectType.Passive;
    }

    public override void UpdateDescription(Player player) {
        this.Description = string.Format("Adds {0} damage", this.BlueText(this.Value));
        if (this.Duration != null)
            this.Description += string.Format(" ({0} turns)", this.BlueText((int) this.Duration));
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new DamageUpCallback(this, this.Value), this.Duration);
    }

    public override List<CardSimulationEffect> Simulate(Character from, Character to) {
        return new() {
            new CardSimulationEffectMaxPriority { Value = 2 }
        };
    }
}
