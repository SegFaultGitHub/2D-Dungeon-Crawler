public abstract class Callback {
    // Lower = more priority = executed last
    public int Priority;
    public string Description;

    public CallbackType Type;

    public abstract int Run(Character from, Character to, int value);
}
