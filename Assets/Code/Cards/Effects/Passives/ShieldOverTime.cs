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
        this.Description = "Adds " + this.GreenText(this.Value) + " shield at the end of each turn";
        if (this.Duration != null)
            this.Description += " (" + this.GreenText((int) this.Duration) + " turns)";
        this.Effects = new Effect[] { Effect.Shield };
        this.EffectType = EffectType.Passive;
    }

    public override void Run(Character from, Character to) {
        to.AddCallback(new ShieldOverTimeCallback(this, this.Value), this.Duration);
    }
}
