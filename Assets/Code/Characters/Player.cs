using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : Character {
    private HandGUI HandGUI;
    [SerializeField] private PlayableCardGUI PlayableCardGUI;
    [field:SerializeField] public List<WeightDistribution<Card>> CardLootTable { get; private set; }
    [field:SerializeField] public List<WeightDistribution<Artifact>> ArtifactLootTable { get; private set; }

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
        cardGUI.name = card.Name + this.Hand.Count;
        cardGUI.Card = card;
        this.HandGUI.AddCard(this, cardGUI);
        return card;
    }

    public void UpdateCardDescriptions() {
        this.HandGUI.UpdateCardDescriptions(this);
    }

    public void RemoveArtifactFromLootTable(Artifact artifact) {
        this.ArtifactLootTable.RemoveAt(this.ArtifactLootTable.FindIndex(weightDistribution => weightDistribution.Obj == artifact));
    }
}
