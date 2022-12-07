public class Draw : Card {
    public override void Initialize() {
        this.CardEffects = new() { new DrawCards(2) };
    }
}
