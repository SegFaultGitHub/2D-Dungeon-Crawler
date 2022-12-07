public class VampiricSlice : Card {
    public override void Initialize() {
        this.CardEffects = new() { new DamageStealingLife(6, 1) };
    }
}
