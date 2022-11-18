using UnityEngine;

public class Damage : CardEffect {
    [SerializeField] private int Value;

    public override void Run(Character from, Character to) {
        from.Foo(CallbackType.Damage, from, to, this.Value, short.MaxValue);
    }
}
