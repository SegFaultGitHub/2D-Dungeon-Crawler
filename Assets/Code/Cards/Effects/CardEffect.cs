public abstract class CardEffect {
    public string Description { get; protected set; }
    public Effect[] Effects { get; protected set; }
    public EffectType EffectType { get; protected set; }

    public abstract void Run(Character from, Character to);

    public abstract void UpdateDescription(Player player);

    protected string GreenText(string input) {
        return "<color=#005500>" + input + "</color>";
    }

    protected string GreenText(int input) {
        return this.GreenText(input.ToString());
    }

    protected string GreenText(float input) {
        return this.GreenText(input.ToString());
    }

    protected string RedText(string input) {
        return "<color=#550000>" + input + "</color>";
    }

    protected string RedText(int input) {
        return this.RedText(input.ToString());
    }

    protected string RedText(float input) {
        return this.RedText(input.ToString());
    }

    protected string BlueText(string input) {
        return "<color=#000055>" + input + "</color>";
    }

    protected string BlueText(int input) {
        return this.BlueText(input.ToString());
    }

    protected string BlueText(float input) {
        return this.BlueText(input.ToString());
    }
}