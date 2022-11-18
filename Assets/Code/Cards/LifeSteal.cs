using UnityEngine;

public class LifeSteal : CardEffect {
    private class LifeStealCallback : OnApplied {
        private readonly float Ratio;

        public LifeStealCallback(int priority, float ratio) {
            this.Priority = priority;
            this.Type = CallbackType.Damage;
            this.Ratio = ratio;
        }

        public override int Run(Character from, Character _to, int value) {
            from.Foo(CallbackType.Heal, from, from, (int) (value * this.Ratio), this.Priority);
            return value;
        }
    }

    [SerializeField] private int Priority;
    [SerializeField] private float Ratio;

    public override void Run(Character from, Character to) {
        to.AddCallback(new LifeStealCallback(this.Priority, this.Ratio));
    }
}
