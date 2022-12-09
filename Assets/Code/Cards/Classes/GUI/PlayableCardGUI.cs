using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayableCardGUI : CardGUI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
    private Player Player;
    public HandGUI Hand { get; set; }

    [HideInInspector] public Vector3 Position, Rotation;
    private bool Dragging;
    private bool Locked;
    private bool MouseOver;

    [SerializeField] private LayerMask CardColliderLayer;
    private Character Target;
    private Camera Camera;

    public override void Initialize(Player player) {
        this.Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        this.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        base.Initialize(player);
    }

    private void FixedUpdate() {
        if (this.Dragging) {
            Vector3 mousePosition = Mouse.current.position.ReadValue();

            #region Move card
            Vector3 cardPosition = new(mousePosition.x, mousePosition.y - 15, this.Camera.nearClipPlane);
            cardPosition = this.Camera.ScreenToWorldPoint(cardPosition);
            cardPosition.z = 0;
            Vector3 delta = cardPosition - this.transform.position;
            if (delta.magnitude < 40f) {
                this.transform.position = cardPosition;
            } else {
                delta = delta / delta.magnitude + delta.normalized * 40f;
                this.transform.position += delta;
            }
            #endregion

            #region Raycast under mouse
            Vector3 worldPosition = this.Camera.ScreenToWorldPoint(mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPosition, -Vector2.up, this.CardColliderLayer);
            if (hits.Length != 0) {
                this.Target = hits[0].collider.GetComponentInParent<Character>();
            } else {
                this.Target = null;
            }
            #endregion
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (this.Dragging || this.Hand.Dragging || this.Locked)
            return;
        this.MouseOver = true;
        this.transform.Find("ColliderBox").gameObject.SetActive(true);
        Vector3 position = this.transform.localPosition;
        Rect rect = this.GetComponent<RectTransform>().rect;
        LeanTween.moveLocal(this.gameObject, new Vector3(position.x, rect.height * this.transform.localScale.x, position.z), .1f)
            .setEaseOutExpo()
            .setOnStart(() => LeanTween.rotate(this.gameObject, new Vector3(0, 0, 0), .1f).setEaseOutExpo());
        this.transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (this.Dragging || this.Hand.Dragging)
            return;
        this.MouseOver = false;
        this.Locked = true;
        this.transform.Find("ColliderBox").gameObject.SetActive(false);
        LeanTween.moveLocal(this.gameObject, this.Position, .1f)
            .setEaseOutExpo()
            .setOnStart(() => LeanTween.rotate(this.gameObject, this.Rotation, .1f).setEaseOutExpo())
            .setOnComplete(() => this.Locked = false);
        this.Hand.RearrangeCardObjects();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (this.Hand.Dragging || this.Locked || !this.MouseOver)
            return;
        this.Dragging = true;
        this.Hand.Dragging = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        this.Dragging = false;
        this.Hand.Dragging = false;
        if (this.Target != null) {
            bool played = this.Player.UseCard(this.Card, this.Target);
            if (played) {
                this.Hand.RemoveCard(this);
                return;
            }
        }
        this.OnPointerExit(null);
        return;
    }
}
