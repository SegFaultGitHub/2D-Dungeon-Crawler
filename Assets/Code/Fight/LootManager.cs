using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class LootManager : MonoBehaviour {
    [SerializeField] private LootCardGUI LootCardGUI;
    [SerializeField] private LootArtifactGUI LootArtifactGUI;
    private List<LootCardGUI> LootCards;
    private List<LootArtifactGUI> LootArtifacts;
    private Player Player;

    public List<WeightDistribution<LootType>> LootDistribution { private get; set; }
    private LootType LootType;

    public void Generate(Player player, int count) {
        this.Player = player;

        if (this.Player.ArtifactLootTable.Count == 0)
            this.LootType = LootType.Card;
        else 
            this.LootType = Utils.Sample(this.LootDistribution);
        switch (this.LootType) {
            case LootType.Card:
                this.GenerateCardLoot(player, count);
                break;
            case LootType.Artifact:
                this.GenerateArtifactLoot(player, count);
                break;
        }
    }

    private void GenerateCardLoot(Player player, int count) {
        Vector3 scale = new(.75f, .75f, .75f);
        Rect rect = this.LootCardGUI.GetComponent<RectTransform>().rect;

        List<Card> cards = Utils.Sample(player.CardLootTable, count);
        float delta = rect.width * 1.3f * scale.x;
        float offset = (cards.Count - 1f) / 2f * -delta;
        this.LootCards = new();
        for (int i = 0; i < cards.Count; i++) {
            LootCardGUI cardGUI = Instantiate(this.LootCardGUI, this.transform);
            this.LootCards.Add(cardGUI);
            cardGUI.Card = cards[i];
            cardGUI.Card.Initialize();
            cardGUI.transform.localScale = scale;
            cardGUI.transform.localPosition = new(offset, 0, 0);
            offset += delta;
            cardGUI.Initialize(this, player);
        }
    }

    private void GenerateArtifactLoot(Player player, int count) {
        Vector3 scale = new(.75f, .75f, .75f);
        Rect rect = this.LootArtifactGUI.GetComponent<RectTransform>().rect;

        List<Artifact> artifacts = Utils.Sample(player.ArtifactLootTable, count);
        float delta = rect.width * 1.3f * scale.x;
        float offset = (artifacts.Count - 1f) / 2f * -delta;
        this.LootArtifacts = new();
        for (int i = 0; i < artifacts.Count; i++) {
            LootArtifactGUI artifactGUI = Instantiate(this.LootArtifactGUI, this.transform);
            this.LootArtifacts.Add(artifactGUI);
            artifactGUI.Artifact = artifacts[i];
            artifactGUI.Artifact.Initialize();
            artifactGUI.Artifact.UpdateDescription(player);
            artifactGUI.transform.localScale = scale;
            artifactGUI.transform.localPosition = new(offset, 0, 0);
            offset += delta;
            artifactGUI.Initialize(this, player);
        }
    }

    public void PickLoot(LootCardGUI cardGUI) {
        foreach (LootCardGUI lootCardGUI in this.LootCards) {
            lootCardGUI.Locked = true;
            LTDescr descr = lootCardGUI.Disappear();
            if (lootCardGUI == cardGUI)
                this.Player.AddToDeck(cardGUI.Card);
            else
                descr.setDelay(.1f);
        }
        this.StartCoroutine(this.Destroy());
    }

    public void PickLoot(LootArtifactGUI artifactGUI) {
        foreach (LootArtifactGUI lootArtifactGUI in this.LootArtifacts) {
            lootArtifactGUI.Locked = true;
            LTDescr descr = lootArtifactGUI.Disappear();
            if (lootArtifactGUI == artifactGUI) {
                Artifact artifact = Instantiate(artifactGUI.Artifact);
                artifact.Initialize();
                artifact.Equip(this.Player);
                this.Player.RemoveArtifactFromLootTable(artifactGUI.Artifact);
            }  else
                descr.setDelay(.1f);
        }
        this.StartCoroutine(this.Destroy());
    }

    private IEnumerator Destroy() {
        if (this.LootType == LootType.Card)
            yield return new WaitUntil(() => this.LootCards.All(lootCard => lootCard.Picked));
        else if (this.LootType == LootType.Artifact)
            yield return new WaitUntil(() => this.LootArtifacts.All(lootArtifact => lootArtifact.Picked));
        Destroy(this.gameObject);
    }
}
