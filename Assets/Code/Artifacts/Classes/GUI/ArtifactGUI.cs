using TMPro;
using UnityEngine;

public class ArtifactGUI : MonoBehaviour {
    public Artifact Artifact { get; set; }

    public virtual void Initialize(Player player) {
        switch (this.Artifact.Rarity) {
            case Rarity.Common:
                this.transform.Find("Artifact - Box/Frame/Frame - Common").gameObject.SetActive(true);
                this.transform.Find("Artifact - Box/Frame/Frame - Rare").gameObject.SetActive(false);
                this.transform.Find("Artifact - Box/Frame/Frame - Legendary").gameObject.SetActive(false);
                break;
            case Rarity.Rare:
                this.transform.Find("Artifact - Box/Frame/Frame - Common").gameObject.SetActive(false);
                this.transform.Find("Artifact - Box/Frame/Frame - Rare").gameObject.SetActive(true);
                this.transform.Find("Artifact - Box/Frame/Frame - Legendary").gameObject.SetActive(false);
                break;
            case Rarity.Legendary:
                this.transform.Find("Artifact - Box/Frame/Frame - Common").gameObject.SetActive(false);
                this.transform.Find("Artifact - Box/Frame/Frame - Rare").gameObject.SetActive(false);
                this.transform.Find("Artifact - Box/Frame/Frame - Legendary").gameObject.SetActive(true);
                break;
        }

        this.transform.Find("Artifact - Box/Name/Name").GetComponent<TMP_Text>().SetText(this.Artifact.Name);
        this.transform.Find("Artifact - Box/Description/Description").GetComponent<TMP_Text>().SetText(string.Join("\n\n", this.Artifact.Description));
    }
}
