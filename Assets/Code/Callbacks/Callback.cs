public abstract class Callback {
    // Lower = more priority = executed last
    public int Priority;
    public CallbackType Type;
    public Effect[] Effects;
    public string Description;

    protected Callback(int priority, CallbackType type, Effect[] effects, string description) {
        this.Priority = priority;
        this.Type = type;
        this.Effects = effects;
        this.Description = description;
    }

    protected Callback(int priority, Effect[] effects, string description) {
        this.Priority = priority;
        this.Effects = effects;
        this.Description = description;
    }

    public abstract int Run(Character from, Character to, int value);
    public abstract void Run(Character character);
}
