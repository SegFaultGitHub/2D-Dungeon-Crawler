using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    public class CardOption {
        public float Weight;
        public Card Card;
        public Character Target;
        public float Score;
        public bool LateScoring;
        public float LateScoringRatio;
    }
    public class CardSimulationEffect { }
    public class CardSimulationEffectDamage : CardSimulationEffect { public float Value; }
    public class CardSimulationEffectShield : CardSimulationEffect { public float Value; }
    public class CardSimulationEffectHeal : CardSimulationEffect { public float Value; }
    public class CardSimulationEffectPoison : CardSimulationEffect { public float Value; }
    public class CardSimulationEffectMaxPriority : CardSimulationEffect { public float Value; }

    private bool MakeChoice() {
        List<WeightDistribution<CardOption>> cardOptions = this.GetCardOptions();
        if (cardOptions.Count == 0)
            return false;
        CardOption option = Utils.Sample(cardOptions);
        return this.UseCard(option.Card, option.Target);
    }
     
    private List<WeightDistribution<CardOption>> GetCardOptions() {
        List<Enemy> allies = new(this.FightManager.Enemies);
        allies.Remove(this);

        List<CardOption> options = new();

        foreach (Card card in this.Hand) {
            foreach (Target target in card.AllowedTarget) {
                switch (target) {
                    case Target.AliveAlly:
                        List<CardOption> _options = new();
                        foreach (Enemy ally in allies)
                            if (card.CanUse(this, ally))
                                _options.Add(new CardOption { Card = card, Target = ally });
                        foreach (CardOption option in _options)
                            option.Weight = 1f / _options.Count;
                        options.AddRange(_options);
                        break;
                    case Target.AliveEnemy:
                        if (card.CanUse(this, this.FightManager.Player))
                            options.Add(new CardOption { Card = card, Target = this.FightManager.Player, Weight = 1 });
                        break;
                    case Target.Self:
                        if (card.CanUse(this, this))
                            options.Add(new CardOption { Card = card, Target = this, Weight = 1 });
                        break;
                }
            }
        }

        return this.EvaluateOptions(options);
    }

    private float EvaluateCardSimulation(CardSimulationEffect simulationEffect, CardOption option) {
        if (simulationEffect is CardSimulationEffectDamage cardSimulationEffectDamage) {
            cardSimulationEffectDamage.Value = Math.Min(cardSimulationEffectDamage.Value, option.Target.HP);
            return cardSimulationEffectDamage.Value;
        } else if (simulationEffect is CardSimulationEffectHeal cardSimulationEffectHeal) {
            cardSimulationEffectHeal.Value = Math.Min(cardSimulationEffectHeal.Value, option.Target.MissingHP);
            return cardSimulationEffectHeal.Value;
        } else if (simulationEffect is CardSimulationEffectShield cardSimulationEffectShield) {
            return cardSimulationEffectShield.Value * 0.8f;
        } else if (simulationEffect is CardSimulationEffectPoison cardSimulationEffectPoison) {
            return cardSimulationEffectPoison.Value * 1.2f;
        } else if (simulationEffect is CardSimulationEffectMaxPriority cardSimulationEffectLateScoring) {
            option.LateScoring = true;
            option.LateScoringRatio = cardSimulationEffectLateScoring.Value;
            return cardSimulationEffectLateScoring.Value;
        }
        throw new Exception("[Enemy:EvaluateCardSimulation] Unexcepted class " + simulationEffect.GetType());
    }

    private List<WeightDistribution<CardOption>> EvaluateOptions(List<CardOption> options) {
        List<WeightDistribution<CardOption>> result = new();
        int minCost = short.MaxValue;
        float maxScore = short.MinValue;
        foreach (CardOption option in options)
            minCost = Math.Min(option.Card.Cost, minCost);
        foreach (CardOption option in options) {
            List<CardSimulationEffect> simulationEffects = option.Card.Simulate(this, option.Target);
            float score = 0;
            foreach (CardSimulationEffect simulationEffect in simulationEffects)
                score += this.EvaluateCardSimulation(simulationEffect, option);
            if (score == 0)
                continue;
            score /= option.Card.Cost - minCost + 1;
            //score /= this.CurrentActionPoints - option.Card.Cost;
            score *= option.Weight;
            result.Add(new() { Weight = score * score, Obj = option });
            maxScore = Mathf.Max(maxScore, score * score);
        }
        foreach (WeightDistribution<CardOption> weightDistribution in result) {
            if (weightDistribution.Obj.LateScoring)
                weightDistribution.Obj.Score = maxScore * weightDistribution.Obj.LateScoringRatio;
        }
        return result;
    }

    public override void TurnStarts() {
        base.TurnStarts();
        this.StartCoroutine(this.PlayAsynchronousTurn());
    }

    private IEnumerator PlayAsynchronousTurn() {
        if (this.MakeChoice()) {
            yield return new WaitUntil(() => !this.FightLocked);
            this.StartCoroutine(this.PlayAsynchronousTurn());
        } else {
            this.FightManager.NextTurn();
        }
    }
}
