using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class WinLoseButton : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
{
    
    [SerializeField] private TextMeshProUGUI ButtonText;
    [SerializeField] private string OriginalText,HoveredText,ClickedText;

    public void OnPointerEnter(PointerEventData eventData)
    {

        ButtonText.text=HoveredText;
        Debug.Log("Entered");

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        ButtonText.text=ClickedText;
        Debug.Log("Clicked");

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        ButtonText.text=HoveredText;
        Debug.Log("Released");

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        ButtonText.text=OriginalText;
        Debug.Log("Leaved");

    }

}
