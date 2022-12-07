public class Concentrate : Card {
    public override void Initialize() {
        this.CardEffects = new() { new DamageUp(2, 2) };
    }
}
