using UnityEngine;

public class Player : Character {
    private Hand HandGUI;
    [SerializeField] private CardGUI CardGUI;

    private new void Start() {
        base.Start();
        this.HandGUI = GameObject.FindGameObjectWithTag("Hand").GetComponent<Hand>();
        this.StartFight();
    }

    public override void StartFight() {
        this.HandGUI.Empty();
        base.StartFight();
    }

    public override Card DrawCard() {
        Card card = base.DrawCard();
        if (card == null)
            return null;
        CardGUI cardGUI = Instantiate(this.CardGUI);
        cardGUI.Card = card;
        this.HandGUI.AddCard(cardGUI);
        return card;
    }
}
