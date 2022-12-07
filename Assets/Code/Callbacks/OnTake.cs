using System;

public abstract class OnTake : Callback {
    protected OnTake(int priority, CallbackType type, Effect[] effects, string description) : base(priority, type, effects, description) { }

    public override void Run(Character character) {
        throw new Exception("[" + this.GetType() + ":Run] Run(Character character) forbidden");
    }
}