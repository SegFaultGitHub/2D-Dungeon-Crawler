using TMPro;
using UnityEngine;

public class CardGUI : MonoBehaviour {
    public Card Card { get; set; }

    public virtual void Initialize(Player player) {
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
        this.transform.Find("Card - Box/Description/Description").GetComponent<TMP_Text>().SetText(string.Join("\n\n", this.Card.Description(player)));
        this.transform.Find("Card - Box/Cost/Cost").GetComponent<TMP_Text>().SetText(this.Card.Cost.ToString());
    }
}
