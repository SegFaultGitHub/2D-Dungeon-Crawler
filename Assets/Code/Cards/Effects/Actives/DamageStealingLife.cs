using static Character;

public class DamageStealingLife : CardEffect {
    private int Priority = 5;
    private int Damage;
    private float Ratio;

    public DamageStealingLife(int damage, float ratio) {
        this.Damage = damage;
        this.Ratio = ratio;
        this.Description = "Inflicts " + this.GreenText(this.Damage) + " damage and steals " + this.GreenText((int) (this.Ratio * 100)) + "% of the inflicted damage";
        this.Effects = new Effect[] { Effect.Damage, Effect.Heal };
        this.EffectType = EffectType.Active;
    }

    public override void Run(Character from, Character to) {
        CardEffectValues foo = from.Foo(CallbackType.Damage, from, to, this.Damage, short.MaxValue);
        from.Foo(CallbackType.Heal, from, from, (int) (foo.Applied * this.Ratio), this.Priority);
    }
}
