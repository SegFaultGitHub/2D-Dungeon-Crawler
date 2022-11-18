using UnityEngine;

public class PoisonOnDamage : CardEffect {
    private class PoisonOnDamageCallback : OnApply {
        private readonly int Damage;

        public PoisonOnDamageCallback(int priority, int damage) {
            this.Priority = priority;
            this.Type = CallbackType.Damage;
            this.Damage = damage;
        }

        public override int Run(Character from, Character to, int value) {
            from.Foo(CallbackType.Poison, from, to, this.Damage, this.Priority);
            return value;
        }
    }

    [SerializeField] private int Priority;
    [SerializeField] private int Damage;

    public override void Run(Character from, Character to) {
        to.AddCallback(new PoisonOnDamageCallback(this.Priority, this.Damage));
    }
}
