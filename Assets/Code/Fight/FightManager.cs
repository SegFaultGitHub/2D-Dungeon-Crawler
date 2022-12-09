using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {
    [field:SerializeField] public int Turn { get; private set; }
    [SerializeField] private int TimelineIndex;

    [SerializeField] private Player Player;
    [SerializeField] private List<Enemy> Enemies;
    [SerializeField] private List<Character> Timeline;

    [SerializeField] private LootManager LootManager;
    [SerializeField] private List<WeightDistribution<LootType>> LootDistribution;

    [SerializeField] private bool Ended;

    public void StartFight() {
        this.Ended = false;
        this.Turn = 0;
        this.TimelineIndex = 0;
        this.Timeline.Add(this.Player);
        this.Timeline.AddRange(this.Enemies);
        this.Player.FightStarts(this);
        foreach (Enemy enemy in this.Enemies)
            enemy.FightStarts(this);

        this.StartTurn();
    }

    private void EndFight() {
        this.Ended = true;
        this.Player.FightEnds();
        foreach (Enemy enemy in this.Enemies)
            enemy.FightEnds();
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

    public void WinFight() {
        this.EndFight();

        LootManager lootManager = Instantiate(this.LootManager, GameObject.FindGameObjectWithTag("Canvas").transform);
        lootManager.LootDistribution = this.LootDistribution;
        lootManager.Generate(this.Player, 3);
    }

    public void LoseFight() {
        this.EndFight();
    }

    public void CheckFightEnded() {
        if (this.Ended)
            return;
        if (this.Player.Dead)
            this.LoseFight();
        else if (this.Enemies.All(enemy => enemy.Dead))
            this.WinFight();
    }
}
