using UnityEngine;

public class Poison : CardEffect {
    [SerializeField] private int Value;

    public override void Run(Character from, Character to) {
        from.Foo(CallbackType.Poison, from, to, this.Value, short.MaxValue);
    }
}
