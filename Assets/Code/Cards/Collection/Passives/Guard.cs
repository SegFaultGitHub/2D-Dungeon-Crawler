public class Guard : Card {
    public override void Initialize() {
        this.Name = "Guard";
        this.CardEffects = new() { new ShieldOverTime(4, 2) };
    }
}
