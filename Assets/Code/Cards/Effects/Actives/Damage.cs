public class Damage : CardEffect {
    private int Value;

    public Damage(int value) {
        this.Value = value;
        this.Effects = new Effect[] { Effect.Damage };
        this.EffectType = EffectType.Active;
    }

    public override void UpdateDescription(Player player) {
        int value = player.Compute(CallbackType.Damage, player, null, this.Value, short.MaxValue);
        if (value > this.Value) {
            this.Description = string.Format("Inflicts {0} ({1}) damage", this.GreenText(value), this.BlueText(this.Value));
        } else if (value < this.Value) {
            this.Description = string.Format("Inflicts {0} ({1}) damage", this.RedText(value), this.BlueText(this.Value));
        } else {
            this.Description = string.Format("Inflicts {0} damage", this.BlueText(value));
        }
    }

    public override void Run(Character from, Character to) {
        from.Foo(CallbackType.Damage, from, to, this.Value, short.MaxValue);
    }
}
