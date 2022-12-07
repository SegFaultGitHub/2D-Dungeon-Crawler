public class DrawCards : CardEffect {
    private int Value;

    public DrawCards(int value) {
        this.Value = value;
        this.Description = "Draws " + this.GreenText(this.Value) + " card" + (this.Value > 1 ? "s" : "");
        this.Effects = new Effect[] { };
        this.EffectType = EffectType.Active;
    }

    public override void Run(Character from, Character to) {
        for (int i = 0; i < this.Value; i++) {
            to.DrawCard();
        }
    }
}
