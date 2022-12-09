public class DoubleSlash : Card {
    public override void Initialize() {
        this.Name = "Double slash";
        this.CardEffects = new() { new Damage(1), new Damage(1) };
    }
}
