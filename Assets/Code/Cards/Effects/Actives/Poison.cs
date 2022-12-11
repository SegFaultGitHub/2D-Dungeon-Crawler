using System.Collections.Generic;
using static Enemy;

public class Poison : CardEffect {
    private int Value;

    public Poison(int value) {
        this.Value = value;
        this.Description = "Applies " + this.GreenText(this.Value) + " poison";
        this.Effects = new Effect[] { Effect.Poison };
        this.EffectType = EffectType.Active;
    }

    public override void UpdateDescription(Player player) {
        int value = player.Compute(CallbackType.Poison, player, null, this.Value, short.MaxValue);
        if (value > this.Value) {
            this.Description = string.Format("Applies {0} ({1}) poison", this.GreenText(value), this.BlueText(this.Value));
        } else if (value < this.Value) {
            this.Description = string.Format("Applies {0} ({1}) poison", this.RedText(value), this.BlueText(this.Value));
        } else {
            this.Description = string.Format("Applies {0} poison", this.BlueText(value));
        }
    }

    public override void Run(Character from, Character to) {
        from.Foo(CallbackType.Poison, from, to, this.Value, short.MaxValue);
    }

    public override List<CardSimulationEffect> Simulate(Character from, Character to) {
        return new() {
            new CardSimulationEffectPoison {
                Value = from.Compute(CallbackType.Poison, from, to, this.Value, short.MaxValue)
            }
        };
    }
}
