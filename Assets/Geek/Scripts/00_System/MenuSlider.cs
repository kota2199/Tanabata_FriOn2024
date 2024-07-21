using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MenuSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
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

    private float maxfillScale = 1;

    [SerializeField]
    private RectTransform baseRect;

    public int defaultValue;

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

    private void Start()
    {
        targetRect = targetImage.GetComponent<RectTransform>();
        targetDefaultPosY = targetRect.anchoredPosition.y;

        canvasRect = canvas.GetComponent<RectTransform>();

        //DisplayMenu();
    }

    public void DisplayMenu()
    {
        value = defaultValue;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDraging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDraging = false;
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
        }

        value = ((targetRect.anchoredPosition.x / (baseRect.sizeDelta.x / 2) * 100) + 100) / 2;

        if (!float.IsNaN(value))
        {
            valueText.text = value.ToString("f0");
            fillRect.localScale = new Vector3(value / 100, 1, 1);
        }
    }
}
