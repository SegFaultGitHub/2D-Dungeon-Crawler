using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class LootManager : MonoBehaviour {
    [SerializeField] private LootCardGUI LootCardGUI;
    private List<LootCardGUI> LootCards;
    private Player Player;

    public void Generate(Player player, int count) {
        this.Player = player;

        Vector3 scale = new(.75f, .75f, .75f);
        Rect rect = this.LootCardGUI.GetComponent<RectTransform>().rect;
        float delta = rect.width * 1.3f * scale.x;
        float offset = (count - 1f) / 2f * -delta;

        List<Card> cards = Utils.Sample(player.PossibleLoot, count);
        this.LootCards = new();
        for (int i = 0; i < count; i++) {
            LootCardGUI cardGUI = Instantiate(this.LootCardGUI, this.transform);
            this.LootCards.Add(cardGUI);
            cardGUI.Card = cards[i];
            cardGUI.Card.Initialize();
            cardGUI.transform.localScale = scale;
            cardGUI.transform.localPosition = new(offset, 0, 0);
            offset += delta;
            cardGUI.Initialize(this);
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

    private IEnumerator Destroy() {
        yield return new WaitUntil(() => this.LootCards.All(lootCard => lootCard.Picked));
        Destroy(this.gameObject);
    }
}
