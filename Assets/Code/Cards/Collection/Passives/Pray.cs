public class Pray : Card {
    public override void Initialize() {
        this.CardEffects = new() { new HealOverTime(2, 3) };
    }
}
