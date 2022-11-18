using UnityEngine;
using static Character;

public class DamageStealingLife : CardEffect {
    [SerializeField] private int Value;
    [SerializeField] private float Ratio;
    [SerializeField] private int Priority;

    public override void Run(Character from, Character to) {
        CardEffectValues foo = from.Foo(CallbackType.Damage, from, to, this.Value, short.MaxValue);
        from.Foo(CallbackType.Heal, from, from, (int) (foo.Applied * this.Ratio), this.Priority);
    }
}
