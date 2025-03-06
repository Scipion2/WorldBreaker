using UnityEngine;
using UnityEngine.UI;

public class LevelPreview : MonoBehaviour
{

    [SerializeField] private Image SpriteDisplay;
    [SerializeField] private Button PreviewButton;
    private int LevelNumber;

    public int SetSprite(Sprite src,int LevelNumberSrc,bool isInteractable)
    {

        SpriteDisplay.sprite=src;
        LevelNumber=LevelNumberSrc;
        PreviewButton.interactable=isInteractable;

        return LevelNumber;

    }

    public void GoToLevel()
    {

        LevelManager.instance.GoToLevel(LevelNumber);

    }

}
