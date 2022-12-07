public class Protection : Card {
    public override void Initialize() {
        this.CardEffects = new() { new Shield(2) };
    }
}
