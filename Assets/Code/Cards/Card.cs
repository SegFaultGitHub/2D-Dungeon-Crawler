using UnityEngine;

public class Card : MonoBehaviour {
    public bool CanCastOnSelf;
    public bool CanCastOnEnemy;
    [TextArea] public string Description;

    public void Use(Character from, Character to) {
        foreach (CardEffect cardEffect in this.GetComponents<CardEffect>()) {
            cardEffect.Run(from, to);
        }
    }
}
