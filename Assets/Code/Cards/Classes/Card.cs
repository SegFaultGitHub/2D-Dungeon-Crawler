using System;
using System.Collections.Generic;
using UnityEngine;
using static Character;
using static Enemy;

public class Card : MonoBehaviour {
    [field: SerializeField] public List<Target> AllowedTarget { get; private set; }
    [field: SerializeField] public bool RemoveAfterUsage { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public Rarity Rarity { get; private set; }
    public string Name { get; protected set; }
    protected List<CardEffect> CardEffects;

    [field:SerializeField] public string Animation { get; private set; }
    [field:SerializeField] public bool MoveToTarget { get; private set; }

    public void Use(Character from, Character to) {
        from.Hand.Remove(this);
        if (!this.RemoveAfterUsage)
            from.Discarded.Add(this);

        foreach (CardEffect cardEffect in this.CardEffects)
            cardEffect.Run(from, to);
    }

    public bool CanUse(Character from, Character to) {
        if (this.Cost > from.CurrentActionPoints)
            return false;

        Target target;

        if (from == to)
            target = Target.Self;
        else if (from.GetType() != to.GetType() && to.Dead)
            target = Target.DeadEnemy;
        else if (from.GetType() != to.GetType() && !to.Dead)
            target = Target.AliveEnemy;
        else if (from.GetType() == to.GetType() && to.Dead)
            target = Target.DeadAlly;
        else if (from.GetType() == to.GetType() && !to.Dead)
            target = Target.AliveAlly;
        else
            throw new Exception("[Card:CanUse] Unexpected exception");

        return this.AllowedTarget.Contains(target);
    }

    public List<CardSimulationEffect> Simulate(Character from, Character to) {
        List<CardSimulationEffect> result = new();
        foreach (CardEffect cardEffect in this.CardEffects)
            result.AddRange(cardEffect.Simulate(from, to));
        return result;
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
