using UnityEngine;

public class DamageOnPoison : CardEffect {
    private class DamageOnPoisonCallback : OnApply {
        private readonly int Damage;

        public DamageOnPoisonCallback(int priority, int damage) {
            this.Priority = priority;
            this.Type = CallbackType.Poison;
            this.Damage = damage;
        }

        public override int Run(Character from, Character to, int value) {
            from.Foo(CallbackType.Damage, from, to, this.Damage, this.Priority);
            return value;
        }
    }

    [SerializeField] private int Priority;
    [SerializeField] private int Damage;

    public override void Run(Character from, Character to) {
        to.AddCallback(new DamageOnPoisonCallback(this.Priority, this.Damage));
    }
}
