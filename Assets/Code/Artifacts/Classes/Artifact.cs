using System;
using UnityEngine;

public abstract class Artifact : MonoBehaviour {
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public Effect[] Effects { get; protected set; }
    [field: SerializeField] public Rarity Rarity { get; private set; }

    public virtual void Equip(Character character) {
        this.transform.SetParent(character.transform.Find("Artifacts"));
    }

    public virtual void Initialize() {
        throw new Exception("[Artifact:Initialize] Must be implemented in the child");
    }

    public virtual void UpdateDescription(Player player) {
        throw new Exception("[Artifact:UpdateDescription] Must be implemented in the child");
    }

    protected string GreenText(string input) {
        return "<color=#005500>" + input + "</color>";
    }

    protected string GreenText(int input) {
        return this.GreenText(input.ToString());
    }

    protected string GreenText(float input) {
        return this.GreenText(input.ToString());
    }

    protected string GetEffects() {
        if (this.Effects.Length == 0)
            return "";
        string result = "";
        foreach (Effect effect in this.Effects)
            result += GUIConstants.EffectSpriteMapping[effect];
        return result + " ";
    }
}
