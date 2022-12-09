using static Character;

public class DamageStealingLife : CardEffect {
    private int Value;
    private float Ratio;

    public DamageStealingLife(int damage, float ratio) {
        this.Value = damage;
        this.Ratio = ratio;
        this.Effects = new Effect[] { Effect.Damage, Effect.Heal };
        this.EffectType = EffectType.Active;
    }

    public override void UpdateDescription(Player player) {
        int value = player.Compute(CallbackType.Damage, player, null, this.Value, short.MaxValue);
        if (value > this.Value) {
            this.Description = string.Format("Inflicts {0} ({1}) damage and steals {2}% of the inflicted damage", this.GreenText(value), this.BlueText(this.Value), this.BlueText((int) (this.Ratio * 100)));
        } else if (value < this.Value) {
            this.Description = string.Format("Inflicts {0} ({1}) damage and steals {2}% of the inflicted damage", this.GreenText(value), this.BlueText(this.Value), this.BlueText((int) (this.Ratio * 100)));
        } else {
            this.Description = string.Format("Inflicts {0} damage and steals {1}% of the inflicted damage", this.BlueText(value), this.BlueText(this.Ratio));
        }
    }

    public override void Run(Character from, Character to) {
        CardEffectValues foo = from.Foo(CallbackType.Damage, from, to, this.Value, short.MaxValue);
        from.Foo(CallbackType.Heal, from, from, (int) (foo.Applied * this.Ratio), 0);
    }
}
