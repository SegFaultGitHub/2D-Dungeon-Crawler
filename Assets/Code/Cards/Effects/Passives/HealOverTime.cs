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
        this.Description = "Recovers " + this.GreenText(this.Value) + " HP at the beginning of each turn";
        if (this.Duration != null)
            this.Description += " (" + this.GreenText((int) this.Duration) + " turns)";
        this.Effects = new Effect[] { Effect.Heal };
        this.EffectType = EffectType.Passive;
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new HealOverTimeCallback(this, this.Value), this.Duration);
    }
}
