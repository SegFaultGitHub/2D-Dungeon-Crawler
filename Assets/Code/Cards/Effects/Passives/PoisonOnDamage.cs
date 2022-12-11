using System.Collections.Generic;
using static Enemy;

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

    private int Value;
    private int? Duration;

    public PoisonOnDamage(int poison, int? duration) {
        this.Value = poison;
        this.Duration = duration;
        this.Effects = new Effect[] { Effect.Poison };
        this.EffectType = EffectType.Passive;
    }

    public override void UpdateDescription(Player player) {
        int value = player.Compute(CallbackType.Poison, player, null, this.Value, 3);
        if (value > this.Value) {
            this.Description = string.Format("Applies {0} ({1}) poison when inflicting damage", this.GreenText(value), this.BlueText(this.Value));
        } else if (value < this.Value) {
            this.Description = string.Format("Applies {0} ({1}) poison when inflicting damage", this.RedText(value), this.BlueText(this.Value));
        } else {
            this.Description = string.Format("Applies {0} Hpoison when inflicting damage", this.BlueText(value));
        }
        if (this.Duration != null)
            this.Description += string.Format(" ({0} turns)", this.BlueText((int) this.Duration));
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new PoisonOnDamageCallback(this, this.Value), this.Duration);
    }

    public override List<CardSimulationEffect> Simulate(Character from, Character to) {
        return new() {
            new CardSimulationEffectPoison {
                Value = from.Compute(CallbackType.Poison, from, to, this.Value, 3) * 2 * (int) this.Duration
            },
        };
    }
}
