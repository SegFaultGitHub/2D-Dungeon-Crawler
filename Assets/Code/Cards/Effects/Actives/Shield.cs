public class Shield : CardEffect {
    private int Value;

    public Shield(int value) {
        this.Value = value;
        this.Effects = new Effect[] { Effect.Shield };
        this.EffectType = EffectType.Active;
    }

    public override void UpdateDescription(Player player) {
        int value = player.Compute(CallbackType.Shield, player, null, this.Value, short.MaxValue);
        if (value > this.Value) {
            this.Description = string.Format("Adds {0} ({1}) shield", this.GreenText(value), this.BlueText(this.Value));
        } else if (value < this.Value) {
            this.Description = string.Format("Adds {0} ({1}) shield", this.RedText(value), this.BlueText(this.Value));
        } else {
            this.Description = string.Format("Adds {0} shield", this.BlueText(value));
        }
    }

    public override void Run(Character from, Character to) {
        from.Foo(CallbackType.Shield, from, to, this.Value, short.MaxValue);
    }
}
