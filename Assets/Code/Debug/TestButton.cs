using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour {
    public Player Player;

    public void StartFight() {
        this.Player.StartFight();
    }

    public void DrawCard() {
        this.Player.DrawCard();
    }
}
