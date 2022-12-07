public class Heal : CardEffect {
    private int Value;

    public Heal(int value) {
        this.Value = value;
        this.Description = "Recovers " + this.GreenText(this.Value) + " HP";
        this.Effects = new Effect[] { Effect.Heal };
        this.EffectType = EffectType.Active;
    }

    public override void Run(Character from, Character to) {
        from.Foo(CallbackType.Heal, from, to, this.Value, short.MaxValue);
    }
}
