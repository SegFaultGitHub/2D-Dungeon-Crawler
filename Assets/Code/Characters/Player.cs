using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    private HandGUI HandGUI;
    [SerializeField] private PlayableCardGUI PlayableCardGUI;
    [field:SerializeField] public List<Card> PossibleLoot { get; private set; }

    private new void Start() {
        base.Start();
        this.HandGUI = GameObject.FindGameObjectWithTag("Hand").GetComponent<HandGUI>();
    }

    public override void FightStarts(FightManager fightManager) {
        this.HandGUI.Empty();
        base.FightStarts(fightManager);
    }

    public override void FightEnds() {
        base.FightEnds();
        this.HandGUI.Empty();
    }

    public override Card DrawCard() {
        Card card = base.DrawCard();
        if (card == null)
            return null;
        PlayableCardGUI cardGUI = Instantiate(this.PlayableCardGUI);
        cardGUI.Card = card;
        this.HandGUI.AddCard(cardGUI);
        return card;
    }

    public void AddToDeck(Card card) {
        this.BaseDeck.Add(card);
    }
}
