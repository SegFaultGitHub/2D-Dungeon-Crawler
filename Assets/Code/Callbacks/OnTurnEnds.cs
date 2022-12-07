using System;

public abstract class OnTurnEnds : Callback {
    protected OnTurnEnds(int priority, Effect[] effects, string description) : base(priority, effects, description) { }

    public override int Run(Character from, Character to, int value) {
        throw new Exception("[" + this.GetType() + ":Run] Run(Character from, Character to, int value) forbidden");
    }
}