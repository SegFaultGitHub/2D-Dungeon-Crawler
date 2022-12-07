using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {
    public int Turn;
    public int TimelineIndex;

    public Player Player;
    public List<Enemy> Enemies;
    public List<Character> Timeline;

    public void StartFight() {
        this.Player.StartFight(this);
        foreach (Enemy enemy in this.Enemies)
            enemy.StartFight(this);
        this.Turn = 0;
        this.TimelineIndex = 0;
        this.Timeline.Add(this.Player);
        this.Timeline.AddRange(this.Enemies);

        this.Player.FightStarts();
        foreach (Enemy enemy in this.Enemies)
            enemy.FightStarts();

        this.StartTurn();
    }

    public void EndFight() {
        this.Player.FightEnds();
        foreach (Enemy enemy in this.Enemies)
            enemy.FightEnds();
        this.Player.ExpireAllCallbacks();

        Card loot = Utils.Sample(this.Player.PossibleLoot);
        this.Player.AddToDeck(loot);
    }

    public void StartTurn() {
        this.Timeline[this.TimelineIndex].TurnStarts();
    }

    public void EndTurn() {
        this.Timeline[this.TimelineIndex].TurnEnds();
        this.TimelineIndex++;
        if (this.TimelineIndex >= this.Timeline.Count) {
            this.TimelineIndex = 0;
            this.Turn++;
        }
    }

    public void NextTurn() {
        this.EndTurn();
        this.StartTurn();
    }
}
