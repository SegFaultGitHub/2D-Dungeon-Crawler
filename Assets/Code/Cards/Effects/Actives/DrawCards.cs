public class DrawCards : CardEffect {
    private int Value;

    public DrawCards(int value) {
        this.Value = value;
        this.Effects = new Effect[] { };
        this.EffectType = EffectType.Active;
    }

    public override void UpdateDescription(Player player) {
        this.Description = string.Format("Draws {0} card{1}", this.BlueText(this.Value), this.Value > 1 ? "s" : "");
    }

    public override void Run(Character from, Character to) {
        for (int i = 0; i < this.Value; i++) {
            to.DrawCard();
        }
    }
}
