public abstract class CardEffect {
    public string Description;
    public Effect[] Effects;
    public EffectType EffectType;

    public abstract void Run(Character from, Character to);

    protected string GreenText(string input) {
        return "<color=#005500>" + input + "</color>";
    }

    protected string GreenText(int input) {
        return this.GreenText(input.ToString());
    }

    protected string GreenText(float input) {
        return this.GreenText(input.ToString());
    }
}