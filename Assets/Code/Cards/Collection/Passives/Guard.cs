public class Guard : Card {
    public override void Initialize() {
        this.CardEffects = new() { new ShieldOverTime(4, 2) };
    }
}
