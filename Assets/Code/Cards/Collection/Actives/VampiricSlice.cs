public class VampiricSlice : Card {
    public override void Initialize() {
        this.Name = "Vampiric slice";
        this.CardEffects = new() { new DamageStealingLife(6, 1) };
    }
}
