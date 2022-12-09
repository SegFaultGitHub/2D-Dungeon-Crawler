public class ShieldOverTime : CardEffect {
    private class ShieldOverTimeCallback : OnTurnEnds {
        private readonly int Value;

        public ShieldOverTimeCallback(CardEffect cardEffect, int value) : base(1, cardEffect.Effects, cardEffect.Description) {
            this.Value = value;
        }

        public override void Run(Character character) {
            character.Foo(CallbackType.Shield, character, character, this.Value, short.MaxValue);
        }
    }

    private int Value;
    private int? Duration;

    public ShieldOverTime(int value, int? duration) {
        this.Value = value;
        this.Duration = duration;
        this.Effects = new Effect[] { Effect.Shield };
        this.EffectType = EffectType.Passive;
    }

    public override void UpdateDescription(Player player) {
        int value = player.Compute(CallbackType.Shield, player, null, this.Value, 1);
        if (value > this.Value) {
            this.Description = string.Format("Adds {0} ({1}) shield at the end of each turn", this.GreenText(value), this.BlueText(this.Value));
        } else if (value < this.Value) {
            this.Description = string.Format("Adds {0} ({1}) shield at the end of each turn", this.RedText(value), this.BlueText(this.Value));
        } else {
            this.Description = string.Format("Adds {0} shield at the end of each turn", this.BlueText(value));
        }
        if (this.Duration != null)
            this.Description += string.Format(" ({0} turns)", this.BlueText((int) this.Duration));
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new ShieldOverTimeCallback(this, this.Value), this.Duration);
    }
}
