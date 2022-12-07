using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CardGUI : MonoBehaviour {
    [Serializable]
    private struct EffectIcon {
        public Effect Effect;
        public Sprite Icon;
    }

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
                this.transform.Find("Box/Frame/Frame - Common").gameObject.SetActive(true);
                this.transform.Find("Box/Frame/Frame - Rare").gameObject.SetActive(false);
                this.transform.Find("Box/Frame/Frame - Legendary").gameObject.SetActive(false);
                this.transform.Find("Box/Cost/Background - Common").gameObject.SetActive(true);
                this.transform.Find("Box/Cost/Background - Rare").gameObject.SetActive(false);
                this.transform.Find("Box/Cost/Background - Legendary").gameObject.SetActive(false);
                break;
            case Rarity.Rare:
                this.transform.Find("Box/Frame/Frame - Common").gameObject.SetActive(false);
                this.transform.Find("Box/Frame/Frame - Rare").gameObject.SetActive(true);
                this.transform.Find("Box/Frame/Frame - Legendary").gameObject.SetActive(false);
                this.transform.Find("Box/Cost/Background - Common").gameObject.SetActive(false);
                this.transform.Find("Box/Cost/Background - Rare").gameObject.SetActive(true);
                this.transform.Find("Box/Cost/Background - Legendary").gameObject.SetActive(false);
                break;
            case Rarity.Legendary:
                this.transform.Find("Box/Frame/Frame - Common").gameObject.SetActive(false);
                this.transform.Find("Box/Frame/Frame - Rare").gameObject.SetActive(false);
                this.transform.Find("Box/Frame/Frame - Legendary").gameObject.SetActive(true);
                this.transform.Find("Box/Cost/Background - Common").gameObject.SetActive(false);
                this.transform.Find("Box/Cost/Background - Rare").gameObject.SetActive(false);
                this.transform.Find("Box/Cost/Background - Legendary").gameObject.SetActive(true);
                break;
        }

        this.transform.Find("Box/Name/Name").GetComponent<TMP_Text>().SetText(this.Card.Name);
        this.transform.Find("Box/Description/Description").GetComponent<TMP_Text>().SetText(string.Join("\n\n", this.Card.Description(EffectSpriteMapping)));
        this.transform.Find("Box/Cost/Cost").GetComponent<TMP_Text>().SetText(this.Card.Cost.ToString());
    }
}
