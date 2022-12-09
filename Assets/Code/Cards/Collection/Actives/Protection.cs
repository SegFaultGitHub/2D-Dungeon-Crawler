public class Protection : Card {
    public override void Initialize() {
        this.Name = "Protection";
        this.CardEffects = new() { new Shield(2) };
    }
}
