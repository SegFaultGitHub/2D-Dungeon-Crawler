using System;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    public List<Target> AllowedTarget;
    public bool RemoveAfterUsage;
    public int Cost;
    protected List<CardEffect> CardEffects;
    public Rarity Rarity;
    public string Name;

    public bool Use(Character from, Character to) {
        Target target;
        if (from.GetType() != to.GetType())
            target = Target.Enemy;
        else if (from == to)
            target = Target.Self;
        else
            target = Target.Ally;

        if (!this.AllowedTarget.Contains(target)) {
            return false;
        } else {
            void runEffects() {
                foreach (CardEffect cardEffect in this.CardEffects)
                    cardEffect.Run(from, to);
            }
            if (target != Target.Enemy) {
                runEffects();
            } else {
                from.Attack(to, runEffects);
            }
            return true;
        }
    }

    public virtual void Initialize() {
        throw new Exception("[Card:Initialize] Must be implemented in the child");
    }

    public IEnumerable<Effect> Effects() {
        foreach (CardEffect cardEffect in this.CardEffects)
            foreach (Effect effect in cardEffect.Effects)
                yield return effect;
    }

    public IEnumerable<string> Description(Dictionary<Effect, string> mapping) {
        foreach (CardEffect cardEffect in this.CardEffects) {
            string description = "";
            if (cardEffect.EffectType == EffectType.Passive)
                description += "Passive\n";
            foreach (Effect effect in cardEffect.Effects)
                description += mapping[effect];
            if (cardEffect.Effects.Length > 0)
                description += " ";
            description += cardEffect.Description;
            yield return description;
        }
    }
}
