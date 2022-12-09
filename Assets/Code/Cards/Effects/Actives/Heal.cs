public class Heal : CardEffect {
    private int Value;

    public Heal(int value) {
        this.Value = value;
        this.Effects = new Effect[] { Effect.Heal };
        this.EffectType = EffectType.Active;
    }

    public override void UpdateDescription(Player player) {
        int value = player.Compute(CallbackType.Heal, player, null, this.Value, short.MaxValue);
        if (value > this.Value) {
            this.Description = string.Format("Recovers {0} ({1}) HP", this.GreenText(value), this.BlueText(this.Value));
        } else if (value < this.Value) {
            this.Description = string.Format("Recovers {0} ({1}) HP", this.RedText(value), this.BlueText(this.Value));
        } else {
            this.Description = string.Format("Recovers {0} HP", this.BlueText(value));
        }
    }

    public override void Run(Character from, Character to) {
        from.Foo(CallbackType.Heal, from, to, this.Value, short.MaxValue);
    }
}
