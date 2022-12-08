using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CardGUI : MonoBehaviour {
    protected Camera Camera;
    public Card Card { get; set; }
    private static readonly Dictionary<Effect, string> EffectSpriteMapping = new() {
        [Effect.Damage] = "<sprite index=9>",
        [Effect.Poison] = "<sprite index=8>",
        [Effect.Heal] = "<sprite index=0>",
        [Effect.ActionPoint] = "<sprite index=3>",
        [Effect.Shield] = "<sprite index=6>",
    };

    public virtual void Initialize() {
        this.Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        switch (this.Card.Rarity) {
            case Rarity.Common:
                this.transform.Find("Card - Box/Frame/Frame - Common").gameObject.SetActive(true);
                this.transform.Find("Card - Box/Frame/Frame - Rare").gameObject.SetActive(false);
                this.transform.Find("Card - Box/Frame/Frame - Legendary").gameObject.SetActive(false);
                this.transform.Find("Card - Box/Cost/Background - Common").gameObject.SetActive(true);
                this.transform.Find("Card - Box/Cost/Background - Rare").gameObject.SetActive(false);
                this.transform.Find("Card - Box/Cost/Background - Legendary").gameObject.SetActive(false);
                break;
            case Rarity.Rare:
                this.transform.Find("Card - Box/Frame/Frame - Common").gameObject.SetActive(false);
                this.transform.Find("Card - Box/Frame/Frame - Rare").gameObject.SetActive(true);
                this.transform.Find("Card - Box/Frame/Frame - Legendary").gameObject.SetActive(false);
                this.transform.Find("Card - Box/Cost/Background - Common").gameObject.SetActive(false);
                this.transform.Find("Card - Box/Cost/Background - Rare").gameObject.SetActive(true);
                this.transform.Find("Card - Box/Cost/Background - Legendary").gameObject.SetActive(false);
                break;
            case Rarity.Legendary:
                this.transform.Find("Card - Box/Frame/Frame - Common").gameObject.SetActive(false);
                this.transform.Find("Card - Box/Frame/Frame - Rare").gameObject.SetActive(false);
                this.transform.Find("Card - Box/Frame/Frame - Legendary").gameObject.SetActive(true);
                this.transform.Find("Card - Box/Cost/Background - Common").gameObject.SetActive(false);
                this.transform.Find("Card - Box/Cost/Background - Rare").gameObject.SetActive(false);
                this.transform.Find("Card - Box/Cost/Background - Legendary").gameObject.SetActive(true);
                break;
        }

        this.transform.Find("Card - Box/Name/Name").GetComponent<TMP_Text>().SetText(this.Card.Name);
        this.transform.Find("Card - Box/Description/Description").GetComponent<TMP_Text>().SetText(string.Join("\n\n", this.Card.Description(EffectSpriteMapping)));
        this.transform.Find("Card - Box/Cost/Cost").GetComponent<TMP_Text>().SetText(this.Card.Cost.ToString());
    }
}
