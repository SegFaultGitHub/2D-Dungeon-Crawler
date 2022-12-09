public class RingOfStrength : Artifact {
    private class RingOfStrengthCallback : OnCompute {
        private readonly int Value;

        public RingOfStrengthCallback(Artifact artifact, int value) : base(0, CallbackType.Damage, artifact.Effects, artifact.Description) {
            this.Value = value;
        }

        public override int Run(Character from, Character to, int value) {
            return value + this.Value;
        }
    }

    private int Value = 2;

    public override void Initialize() {
        this.Name = "Ring of strength";
        this.Effects = new Effect[] { Effect.Damage };
    }

    public override void UpdateDescription(Player player) {
        this.Description = this.GetEffects() + string.Format("Adds {0} damage", this.GreenText(this.Value));
    }

    public override void Equip(Character character) {
        base.Equip(character);
        character.AddCallback(new RingOfStrengthCallback(this, this.Value));
    }
}
