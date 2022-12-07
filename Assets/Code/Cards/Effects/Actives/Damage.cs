public class Damage : CardEffect {
    private int Value;

    public Damage(int value) {
        this.Value = value;
        this.Description = "Inflicts " + this.GreenText(this.Value) + " damage";
        this.Effects = new Effect[] { Effect.Damage };
        this.EffectType = EffectType.Active;
    }

    public override void Run(Character from, Character to) {
        from.Foo(CallbackType.Damage, from, to, this.Value, short.MaxValue);
    }
}
