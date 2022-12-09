public class HealOverTime : CardEffect {
    private class HealOverTimeCallback : OnTurnStarts {
        private readonly int Value;

        public HealOverTimeCallback(CardEffect cardEffect, int value) : base(1, cardEffect.Effects, cardEffect.Description) {
            this.Value = value;
        }

        public override void Run(Character character) {
            character.Foo(CallbackType.Heal, character, character, this.Value, short.MaxValue);
        }
    }

    private int Value;
    private int? Duration;

    public HealOverTime(int value, int? duration) {
        this.Value = value;
        this.Duration = duration;
        this.Effects = new Effect[] { Effect.Heal };
        this.EffectType = EffectType.Passive;
    }

    public override void UpdateDescription(Player player) {
        int value = player.Compute(CallbackType.Heal, player, null, this.Value, 1);
        if (value > this.Value) {
            this.Description = string.Format("Recovers {0} ({1}) HP at the beginning of each turn", this.GreenText(value), this.BlueText(this.Value));
        } else if (value < this.Value) {
            this.Description = string.Format("Recovers {0} ({1}) HP at the beginning of each turn", this.RedText(value), this.BlueText(this.Value));
        } else {
            this.Description = string.Format("Recovers {0} HP at the beginning of each turn", this.BlueText(value));
        }
        if (this.Duration != null)
            this.Description += string.Format(" ({0} turns)", this.BlueText((int) this.Duration));
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new HealOverTimeCallback(this, this.Value), this.Duration);
    }
}
