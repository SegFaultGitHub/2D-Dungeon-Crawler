public class Concentrate : Card {
    public override void Initialize() {
        this.Name = "Concentrate";
        this.CardEffects = new() { new DamageUp(2, 2) };
    }
}
