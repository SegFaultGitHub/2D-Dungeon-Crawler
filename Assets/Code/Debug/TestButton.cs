using UnityEngine;

public class TestButton : MonoBehaviour {
    public FightManager FightManager;
    public Player Player;
    public Artifact Artifact;

    public void StartFight() {
        this.FightManager.StartFight();
    }

    public void EndFight() {
        this.FightManager.WinFight();
    }

    public void NextTurn() {
        this.FightManager.NextTurn();
    }

    public void DrawCard() {
        this.Player.DrawCard();
    }

    public void EquipArtifact() {
        Artifact artifact = Instantiate(this.Artifact);
        artifact.Initialize();
        artifact.Equip(this.Player);
    }
}
