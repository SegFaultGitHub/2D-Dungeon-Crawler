public abstract class CardEffect {
    public string Description { get; protected set; }
    public Effect[] Effects { get; protected set; }
    public EffectType EffectType { get; protected set; }

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