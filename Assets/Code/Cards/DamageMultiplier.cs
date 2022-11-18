using UnityEngine;

public class DamageMultiplier : CardEffect {
    private class DamageMultiplierCallback : OnCompute {
        private readonly int Ratio;

        public DamageMultiplierCallback(int priority, int ratio) {
            this.Priority = priority;
            this.Type = CallbackType.Damage;
            this.Ratio = ratio;
        }

        public override int Run(Character from, Character to, int value) {
            return value * this.Ratio;
        }
    }

    [SerializeField] private int Priority;
    [SerializeField] private int Ratio;

    public override void Run(Character from, Character to) {
        to.AddCallback(new DamageMultiplierCallback(this.Priority, this.Ratio));
    }
}
