public class Poison : CardEffect {
    private int Value;

    public Poison(int value) {
        this.Value = value;
        this.Description = "Applies " + this.GreenText(this.Value) + " poison";
        this.Effects = new Effect[] { Effect.Poison };
        this.EffectType = EffectType.Active;
    }

    public override void Run(Character from, Character to) {
        from.Foo(CallbackType.Poison, from, to, this.Value, short.MaxValue);
    }
}
