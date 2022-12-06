public class Recover : Card {
    public override void Initialize() {
        this.CardEffects = new() { new Heal(2) };
    }
}
