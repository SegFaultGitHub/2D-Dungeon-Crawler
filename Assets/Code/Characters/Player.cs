using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    private Hand HandGUI;
    [SerializeField] private PlayableCardGUI CardGUI;
    public List<Card> PossibleLoot;

    private new void Start() {
        base.Start();
        this.HandGUI = GameObject.FindGameObjectWithTag("Hand").GetComponent<Hand>();
    }

    public override void StartFight(FightManager fightManager) {
        this.HandGUI.Empty();
        base.StartFight(fightManager);
    }

    public override Card DrawCard() {
        Card card = base.DrawCard();
        if (card == null)
            return null;
        PlayableCardGUI cardGUI = Instantiate(this.CardGUI);
        cardGUI.Card = card;
        this.HandGUI.AddCard(cardGUI);
        return card;
    }

    public void AddToDeck(Card card) {
        this.BaseDeck.Add(card);
    }
}
