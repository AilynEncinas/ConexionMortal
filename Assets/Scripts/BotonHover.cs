using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normalSprite;
    public Sprite hoverSprite;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = normalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = normalSprite;
    }
}
