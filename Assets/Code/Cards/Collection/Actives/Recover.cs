public class Recover : Card {
    public override void Initialize() {
        this.Name = "Recover";
        this.CardEffects = new() { new Heal(2) };
    }
}
