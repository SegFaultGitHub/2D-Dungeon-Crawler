using UnityEngine;

public class TestButton : MonoBehaviour {
    public FightManager FightManager;
    public Player Player;

    public void StartFight() {
        this.FightManager.StartFight();
    }

    public void EndFight() {
        this.FightManager.EndFight();
    }

    public void NextTurn() {
        this.FightManager.NextTurn();
    }

    public void DrawCard() {
        this.Player.DrawCard();
    }
}
