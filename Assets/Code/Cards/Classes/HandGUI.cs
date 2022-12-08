using System.Collections.Generic;
using UnityEngine;

public class HandGUI : MonoBehaviour {
    [SerializeField] private List<PlayableCardGUI> Cards;
    [SerializeField] private Transform Center;
    [SerializeField] private float AngleOffset = 5;
    [SerializeField] private float MaxAngle = 40;
    public bool Dragging { get; set; }

    private void Start() {
        if (this.Cards == null)
            this.Cards = new();
        this.RearrangeCards();
    }

    public void Empty() {
        foreach (PlayableCardGUI card in this.Cards) {
            Destroy(card.gameObject);
        }
        this.Cards = new();
    }

    public void RemoveCard(PlayableCardGUI card) {
        this.Cards.Remove(card);
        this.RearrangeCardObjects();
        this.RearrangeCards();
        Destroy(card.gameObject);
    }

    public void AddCard(PlayableCardGUI card) {
        card.transform.SetParent(this.transform.Find("Cards"));
        card.transform.localScale = new(.5f, .5f, .5f);
        card.transform.localPosition = this.Center.localPosition + 0.95f * (this.Center.localScale.x / 2f) * Vector3.up;
        card.Hand = this;
        card.Initialize();
        this.Cards.Add(card);
        this.RearrangeCards();
    }

    public void RearrangeCards() {
        float adjustedAngleOffset = Mathf.Min(this.MaxAngle / (this.Cards.Count - 1f), this.AngleOffset);
        float angleOffset = (this.Cards.Count - 1f) * adjustedAngleOffset / 2f;
        foreach (PlayableCardGUI card in this.Cards) {
            Vector2 position = this.Center.localPosition + (Quaternion.Euler(0, 0, angleOffset) * Vector2.up) * this.Center.localScale.x / 2;
            LeanTween.rotate(card.gameObject, new(0, 0, angleOffset), 0.1f).setEaseOutExpo();
            LeanTween.moveLocal(card.gameObject, new Vector3(position.x, position.y, 0), 0.1f).setEaseOutExpo();

            card.Position = position;
            card.Rotation = new(0, 0, angleOffset);

            angleOffset -= adjustedAngleOffset;
        }
    }

    public void RearrangeCardObjects() {
        for (int i = 0; i < this.Cards.Count; i++) {
            this.Cards[i].transform.SetSiblingIndex(i);
        }
    }
}
