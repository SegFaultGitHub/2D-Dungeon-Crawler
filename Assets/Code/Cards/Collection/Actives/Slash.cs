public class Slash : Card {
    public override void Initialize() {
        this.Name = "Slash";
        this.CardEffects = new() { new Damage(2) };
    }
}
