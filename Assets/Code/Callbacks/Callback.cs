public abstract class Callback {
    // Lower = more priority = executed last
    public int Priority { get; private set; }
    public CallbackType Type { get; private set; }
    public Effect[] Effects { get; private set; }
    public string Description { get; private set; }

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
