using UnityEngine;
using UnityEngine.UI;

public class LevelPreview : MonoBehaviour
{

    [SerializeField] private Image SpriteDisplay;
    private int LevelNumber;

    public void SetSprite(Sprite src,int LevelNumberSrc)
    {

        SpriteDisplay.sprite=src;
        LevelNumber=LevelNumberSrc;

    }

    public void GoToLevel()
    {

        LevelManager.instance.GoToLevel(LevelNumber);

    }

}
