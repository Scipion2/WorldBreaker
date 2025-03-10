using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class WinLoseButton : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
{
    
    [Header("Components")]
    [Space(10)]

        [SerializeField] private TextMeshProUGUI ButtonText;
        [SerializeField] private string OriginalText,HoveredText,ClickedText;

    //ON POINTER EVENTS

        public void OnPointerEnter(PointerEventData eventData)
        {

            ButtonText.text=HoveredText;

        }

        public void OnPointerDown(PointerEventData eventData)
        {

            ButtonText.text=ClickedText;

        }

        public void OnPointerUp(PointerEventData eventData)
        {

            ButtonText.text=HoveredText;

        }

        public void OnPointerExit(PointerEventData eventData)
        {

            ButtonText.text=OriginalText;

        }

}
