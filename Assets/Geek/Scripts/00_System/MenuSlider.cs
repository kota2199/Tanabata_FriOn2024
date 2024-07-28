using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MenuSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private MenuController menuCtrlr;

    /// <summary>
    /// Handle Drag
    /// </summary>
    [SerializeField]
    private Canvas canvas;

    private RectTransform canvasRect;

    public Image targetImage;

    private RectTransform targetRect;

    private float targetDefaultPosY;

    public bool isDraging;

    [SerializeField]
    private RectTransform fillRect;

    [SerializeField]
    private RectTransform baseRect;

    [SerializeField]
    private Text valueText;

    private float _value;

    public float value
    {
        get { return _value; }
        private set
        {
            if (_value != value)
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
    }

    public event Action<float> OnValueChanged;

    private void Awake()
    {
        targetRect = targetImage.GetComponent<RectTransform>();
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    private void Start()
    {
        targetDefaultPosY = targetRect.anchoredPosition.y;
    }

    private void OnEnable()
    {
        value = menuCtrlr.SetDefaultValue(this);
        targetRect.anchoredPosition = new Vector2((value * baseRect.sizeDelta.x) - baseRect.sizeDelta.x / 2, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDraging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDraging =  false;
    }

    private void Update()
    {
        if (isDraging)
        {
            Vector2 mousePos = Input.mousePosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (canvasRect, Input.mousePosition, canvas.worldCamera, out mousePos);

            targetRect.anchoredPosition = new Vector2(mousePos.x, targetDefaultPosY);

            if (mousePos.x > baseRect.sizeDelta.x / 2)
            {
                targetRect.anchoredPosition = new Vector2(baseRect.sizeDelta.x / 2, targetDefaultPosY);
                fillRect.localScale = new Vector3(mousePos.x ,1 ,1);
            }

            else if (mousePos.x < baseRect.sizeDelta.x / -2)
            {
                targetRect.anchoredPosition = new Vector2(baseRect.sizeDelta.x / -2, targetDefaultPosY);
            }

            value = (targetRect.anchoredPosition.x + (baseRect.sizeDelta.x / 2)) / baseRect.sizeDelta.x;
        }

        if (!float.IsNaN(value))
        {
            valueText.text = (value * 100).ToString("f0");
            fillRect.localScale = new Vector3(value, 1, 1);
        }
    }
}
