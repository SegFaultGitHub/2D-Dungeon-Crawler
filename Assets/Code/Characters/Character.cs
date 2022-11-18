using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    private List<OnCompute> OnComputeCallbacks = new();
    private List<OnApply> OnApplyCallbacks = new();
    private List<OnTake> OnTakeCallbacks = new();
    private List<OnApplied> OnAppliedCallbacks = new();

    [SerializeField] private int HP;
    [SerializeField] private int Poison;

    public struct CardEffectValues {
        public int Input;
        public int Compute;
        public int Apply;
        public int Take;
        public int Applied;
    }

    public void UseCard(Card card, Character target) {
        Debug.Log("-------");
        Debug.Log("Card played: " + card.name);
        card.Use(this, target);
    }

    public void AddCallback(Callback callback) {
        if (callback is OnCompute onCompute) {
            this.OnComputeCallbacks.Add(onCompute);
            this.OnComputeCallbacks.Sort((c1, c2) => {
                return c1.Priority - c2.Priority;
            });
        } else if (callback is OnApply onApply) {
            this.OnApplyCallbacks.Add(onApply);
            this.OnApplyCallbacks.Sort((c1, c2) => {
                return c1.Priority - c2.Priority;
            });
        } else if (callback is OnTake onTake) {
            this.OnTakeCallbacks.Add(onTake);
            this.OnTakeCallbacks.Sort((c1, c2) => {
                return c1.Priority - c2.Priority;
            });
        } else if (callback is OnApplied onApplied) {
            this.OnAppliedCallbacks.Add(onApplied);
            this.OnAppliedCallbacks.Sort((c1, c2) => {
                return c1.Priority - c2.Priority;
            });
        }
    }

    public void RemoveCallback(Callback callback) {
        if (callback is OnCompute onCompute) {
            this.OnComputeCallbacks.Remove(onCompute);
        } else if (callback is OnApply onApply) {
            this.OnApplyCallbacks.Remove(onApply);
        } else if (callback is OnTake onTake) {
            this.OnTakeCallbacks.Remove(onTake);
        } else if (callback is OnApplied onApplied) {
            this.OnAppliedCallbacks.Remove(onApplied);
        }
    }

    public CardEffectValues Foo(CallbackType type, Character from, Character to, int value, int priority) {
        CardEffectValues values = new() {
            Input = value
        };
        value = from.Compute(type, from, to, value, priority);
        values.Compute = value;
        value = from.Apply(type, from, to, value, priority);
        values.Apply = value;
        value = to.Take(type, from, to, value, priority);
        values.Take = value;
        value = from.Applied(type, from, to, value, priority);
        values.Applied = value;

        return values;
    }

    public int Compute(CallbackType type, Character from, Character to, int value, int priority) {
        foreach (Callback callback in this.OnComputeCallbacks) {
            if (callback.Type != type || callback.Priority >= priority)
                continue;
            value = callback.Run(from, to, value);
        }
        return value;
    }

    public int Apply(CallbackType type, Character from, Character to, int value, int priority) {
        foreach (Callback callback in this.OnApplyCallbacks) {
            if (callback.Type != type || callback.Priority >= priority)
                continue;
            value = callback.Run(from, to, value);
        }
        return value;
    }

    public int Take(CallbackType type, Character from, Character to, int value, int priority) {
        foreach (Callback callback in this.OnTakeCallbacks) {
            if (callback.Type != type || callback.Priority >= priority)
                continue;
            value = callback.Run(from, to, value);
        }

        switch (type) {
            case CallbackType.Damage:
                to.RemoveHP(value);
                break;
            case CallbackType.Poison:
                to.AddPoison(value);
                break;
            case CallbackType.Heal:
                to.AddHP(value);
                break;
        }
        Debug.Log(to + " took " + value + " " + type);
        return value;
    }

    public int Applied(CallbackType type, Character from, Character to, int value, int priority) {
        foreach (Callback callback in this.OnAppliedCallbacks) {
            if (callback.Type != type || callback.Priority >= priority)
                continue;
            value = callback.Run(from, to, value);
        }
        return value;
    }

    public void AddHP(int value) {
        if (value <= 0)
            return;
        this.HP += value;
    }

    public void RemoveHP(int value) {
        if (value <= 0)
            return;
        this.HP -= value;
    }

    public void AddPoison(int value) {
        if (value <= 0)
            return;
        this.Poison += value;
    }

    public void RemovePoison(int value) {
        if (value <= 0)
            return;
        this.Poison -= value;
    }
}
