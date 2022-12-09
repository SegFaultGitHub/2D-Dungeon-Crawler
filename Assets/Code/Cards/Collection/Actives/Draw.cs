public class Draw : Card {
    public override void Initialize() {
        this.Name = "Draw";
        this.CardEffects = new() { new DrawCards(2) };
    }
}
