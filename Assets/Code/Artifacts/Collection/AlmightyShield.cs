public class AlmightyShield : Artifact {
    private class AlmightyShieldCallback : OnApplied {
        public AlmightyShieldCallback(Artifact artifact) : base(0, CallbackType.Damage, artifact.Effects, artifact.Description) { }

        public override int Run(Character from, Character to, int value) {
            from.Foo(CallbackType.Shield, from, from, value, 0);
            return value;
        }
    }

    public override void Initialize() {
        this.Name = "Almighty shield";
        this.Effects = new Effect[] { Effect.Shield };
    }

    public override void UpdateDescription(Player player) {
        this.Description = this.GetEffects() + "Adds as many shield as you dealt damage";
    }

    public override void Equip(Character character) {
        base.Equip(character);
        character.AddCallback(new AlmightyShieldCallback(this));
    }
}
