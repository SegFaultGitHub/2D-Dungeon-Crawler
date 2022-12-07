using System;

public abstract class OnApply : Callback {
    protected OnApply(int priority, CallbackType type, Effect[] effects, string description) : base(priority, type, effects, description) { }

    public override void Run(Character character) {
        throw new Exception("[" + this.GetType() + ":Run] Run(Character character) forbidden");
    }
}