public class Slash : Card {
    public override void Initialize() {
        this.CardEffects = new() { new Damage(2) };
    }
}
