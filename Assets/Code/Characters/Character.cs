using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    private Animator Animator;
    public Transform AttackZone { get; private set; }
    public Dictionary<string, AnimationClip> Clips { get; private set; }

    [SerializeField] private int MaxHP;
    [SerializeField] private int HP;
    [SerializeField] private int Shield;
    [SerializeField] private int Poison;
    [SerializeField] private int ActionPoints;
    [SerializeField] private int CurrentActionPoints;
    [SerializeField] protected List<Card> BaseDeck;
    [SerializeField] protected List<Card> Deck;
    [SerializeField] protected List<Card> Hand;
    [SerializeField] protected List<Card> Discarded;
    [SerializeField] private int InitialHandSize;

    protected void Start() {
        this.Animator = this.GetComponent<Animator>();
        this.Clips = new();
        foreach (AnimationClip clip in this.Animator.runtimeAnimatorController.animationClips) {
            this.Clips[clip.name] = clip;
        }
        this.AttackZone = this.transform.Find("Attack zone");
    }

    public virtual void StartFight() {
        this.Deck = new(this.BaseDeck);
        this.Hand = new();
        this.Discarded = new();
        for (int i = 0; i < this.InitialHandSize; i++) {
            this.DrawCard();
        }
    }

    public virtual Card DrawCard() {
        if (this.Deck.Count == 0) {
            if (this.Discarded.Count == 0)
                return null;
            this.Deck = new(this.Discarded);
            this.Discarded = new();
        }

        Card card = Utils.Sample(this.Deck);
        card.Initialize();
        this.Deck.Remove(card);
        this.Hand.Add(card);
        return card;
    }

    #region Movement
    public LTDescr MoveTo(Vector3 to, float duration) {
        return LeanTween.move(this.gameObject, to, duration)
            .setOnStart(() => {
                this.Animator.SetBool("Moving", true);
                Vector3 scale = this.transform.localScale;
                if (to.x > this.transform.position.x) {
                    this.transform.localScale = new(Mathf.Abs(scale.x), scale.y, scale.z);
                } else {
                    this.transform.localScale = new(-Mathf.Abs(scale.x), scale.y, scale.z);
                }
            })
            .setOnComplete(() => this.Animator.SetBool("Moving", false));
    }

    public void Attack() {
        this.Animator.SetTrigger("Attack");
    }

    public void Attack(Character target, Action action) {
        Vector3 scale = this.transform.localScale;
        Vector3 position = this.transform.position;

        LTSeq sequence = LeanTween.sequence();
        sequence.append(this.MoveTo(target.AttackZone.position, .25f));
        sequence.append(() => this.Attack());
        sequence.append(this.Clips["Attack"].length * 0.8f);
        sequence.append(action);
        sequence.append(0.2f);
        sequence.append(this.MoveTo(position, .25f));
        sequence.append(() => this.transform.localScale = scale);
    }
    #endregion

    #region Card methods
    private readonly List<OnCompute> OnComputeCallbacks = new();
    private readonly List<OnApply> OnApplyCallbacks = new();
    private readonly List<OnTake> OnTakeCallbacks = new();
    private readonly List<OnApplied> OnAppliedCallbacks = new();

    public struct CardEffectValues {
        public int Input;
        public int Compute;
        public int Apply;
        public int Take;
        public int Applied;
    }

    public bool UseCard(Card card, Character target) {
        Debug.Log("-------");
        Debug.Log("Card played: " + card.Name);
        if (card.Cost > this.CurrentActionPoints)
            return false;
        bool played = card.Use(this, target);
        if (!played)
            return false;
        this.Foo(CallbackType.ActionPoint, this, this, -card.Cost, 0);
        this.Hand.Remove(card);
        if (!card.RemoveAfterUsage)
            this.Discarded.Add(card);
        return played;
    }

    public void AddCallback(Callback callback) {
        if (callback is OnCompute onCompute) {
            this.OnComputeCallbacks.Add(onCompute);
            this.OnComputeCallbacks.Sort((c1, c2) => {
                return c1.Priority - c2.Priority;
            });
        } else if (callback is OnApply onApply) {
            this.OnApplyCallbacks.Add(onApply);
            this.OnApplyCallbacks.Sort((c1, c2) => {
                return c1.Priority - c2.Priority;
            });
        } else if (callback is OnTake onTake) {
            this.OnTakeCallbacks.Add(onTake);
            this.OnTakeCallbacks.Sort((c1, c2) => {
                return c1.Priority - c2.Priority;
            });
        } else if (callback is OnApplied onApplied) {
            this.OnAppliedCallbacks.Add(onApplied);
            this.OnAppliedCallbacks.Sort((c1, c2) => {
                return c1.Priority - c2.Priority;
            });
        }
    }

    public void RemoveCallback(Callback callback) {
        if (callback is OnCompute onCompute) {
            this.OnComputeCallbacks.Remove(onCompute);
        } else if (callback is OnApply onApply) {
            this.OnApplyCallbacks.Remove(onApply);
        } else if (callback is OnTake onTake) {
            this.OnTakeCallbacks.Remove(onTake);
        } else if (callback is OnApplied onApplied) {
            this.OnAppliedCallbacks.Remove(onApplied);
        }
    }

    public CardEffectValues Foo(CallbackType type, Character from, Character to, int value, int priority) {
        CardEffectValues values = new() {
            Input = value
        };
        value = from.Compute(type, from, to, value, priority);
        values.Compute = value;
        value = from.Apply(type, from, to, value, priority);
        values.Apply = value;
        value = to.Take(type, from, to, value, priority);
        values.Take = value;
        value = from.Applied(type, from, to, value, priority);
        values.Applied = value;

        return values;
    }

    public int Compute(CallbackType type, Character from, Character to, int value, int priority) {
        foreach (Callback callback in this.OnComputeCallbacks) {
            if (callback.Type != type || callback.Priority >= priority)
                continue;
            value = callback.Run(from, to, value);
        }
        return value;
    }

    public int Apply(CallbackType type, Character from, Character to, int value, int priority) {
        foreach (Callback callback in this.OnApplyCallbacks) {
            if (callback.Type != type || callback.Priority >= priority)
                continue;
            value = callback.Run(from, to, value);
        }
        return value;
    }

    public int Take(CallbackType type, Character from, Character to, int value, int priority) {
        foreach (Callback callback in this.OnTakeCallbacks) {
            if (callback.Type != type || callback.Priority >= priority)
                continue;
            value = callback.Run(from, to, value);
        }

        switch (type) {
            case CallbackType.Damage:
                to.RemoveHP(value);
                break;
            case CallbackType.Poison:
                to.AddPoison(value);
                break;
            case CallbackType.Heal:
                to.AddHP(value);
                break;
            case CallbackType.ActionPoint:
                to.AddActionPoint(value);
                break;
            case CallbackType.Shield:
                to.AddShield(value);
                break;
        }
        Debug.Log(to + " took " + value + " " + type);
        return value;
    }

    public int Applied(CallbackType type, Character from, Character to, int value, int priority) {
        foreach (Callback callback in this.OnAppliedCallbacks) {
            if (callback.Type != type || callback.Priority >= priority)
                continue;
            value = callback.Run(from, to, value);
        }
        return value;
    }

    public void AddHP(int value) {
        if (value <= 0)
            return;
        this.HP += value;
        if (this.HP > this.MaxHP)
            this.HP = this.MaxHP;
    }

    public void RemoveHP(int value) {
        if (value <= 0)
            return;
        int shieldDamage = Math.Min(this.Shield, value);
        this.Shield -= shieldDamage;
        value -= shieldDamage;
        this.HP -= value;

        if (this.HP <= 0)
            this.Animator.SetTrigger("Death");
    }

    public void AddPoison(int value) {
        if (value <= 0)
            return;
        this.Poison += value;
    }

    public void RemovePoison(int value) {
        if (value <= 0)
            return;
        this.Poison -= value;
    }

    public void AddActionPoint(int value) {
        this.CurrentActionPoints += value;
    }

    public void AddShield(int value) {
        this.Shield += value;
    }
    #endregion
}
