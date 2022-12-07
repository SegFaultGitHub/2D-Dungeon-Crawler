public class DoubleSlash : Card {
    public override void Initialize() {
        this.CardEffects = new() { new Damage(1), new Damage(1) };
    }
}
