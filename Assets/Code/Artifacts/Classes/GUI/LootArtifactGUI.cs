using UnityEngine;
using UnityEngine.EventSystems;

public class LootArtifactGUI : ArtifactGUI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
    [SerializeField] private LayerMask CardColliderLayer;

    private LootManager LootManager;
    private Vector3 InitialScale;

    public bool Locked { private get; set; }
    public bool Picked { get; private set; }

    public void Initialize(LootManager lootManager, Player player) {
        this.Locked = false;
        this.LootManager = lootManager;
        this.InitialScale = this.transform.localScale;
        this.transform.localScale *= 0;
        LeanTween.scale(this.gameObject, this.InitialScale, .3f).setEaseOutBack();
        base.Initialize(player);
    }

    public LTDescr Disappear() {
        return LeanTween.scale(this.gameObject, Vector3.zero, .3f).setEaseInBack()
            .setOnComplete(() => this.Picked = true);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (this.Locked)
            return;
        LeanTween.scale(this.gameObject, this.InitialScale * 1.3f, .1f)
            .setEaseOutExpo();
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (this.Locked)
            return;
        LeanTween.scale(this.gameObject, this.InitialScale, .1f)
            .setEaseOutExpo();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (this.Locked)
            return;
        this.LootManager.PickLoot(this);
    }
}
