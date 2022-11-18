using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour {
    [SerializeField] private Character Caster;
    [SerializeField] private Character Target;
    [SerializeField] private List<Card> Cards;

    public void Test() {
        foreach (Card card in this.Cards)
            this.Caster.UseCard(card, this.Target);
    }
}
