public class Shield : CardEffect {
    private int Value;

    public Shield(int value) {
        this.Value = value;
        this.Description = "Adds " + this.GreenText(this.Value) + " shield";
        this.Effects = new Effect[] { Effect.Shield };
        this.EffectType = EffectType.Active;
    }

    public override void Run(Character from, Character to) {
        from.Foo(CallbackType.Shield, from, to, this.Value, short.MaxValue);
    }
}
