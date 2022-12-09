using System;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] private List<Target> AllowedTarget;
    [field:SerializeField] public bool RemoveAfterUsage { get; private set; }
    [field:SerializeField] public int Cost { get; private set; }
    [field:SerializeField] public Rarity Rarity { get; private set; }
    public string Name { get; protected set; }
    protected List<CardEffect> CardEffects;

    [field: SerializeField] public CardGUI CardGUI { get; set; }

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
            from.Hand.Remove(this);
            if (!this.RemoveAfterUsage)
                from.Discarded.Add(this);

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

    public IEnumerable<string> Description(Player player) {
        foreach (CardEffect cardEffect in this.CardEffects) {
            cardEffect.UpdateDescription(player);
            string description = "";
            if (cardEffect.EffectType == EffectType.Passive)
                description += "Passive\n";
            if (cardEffect.Effects.Length > 0) {
                foreach (Effect effect in cardEffect.Effects)
                    description += GUIConstants.EffectSpriteMapping[effect];
                description += " ";
            }
            description += cardEffect.Description;
            yield return description;
        }
    }
}
