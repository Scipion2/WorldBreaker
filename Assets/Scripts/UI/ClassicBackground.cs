using UnityEngine;
using UnityEngine.UI;

public class ClassicBackground : MonoBehaviour
{
    [SerializeField] private Sprite[] BackGroundSprites;
    [SerializeField] private Image SpriteDisplayed;

    public void SetImage(int index){SpriteDisplayed.sprite=BackGroundSprites[index];}

    public void Awake()
    {

        UIManager.instance.SetClassicBackground(this);
        SetImage(LevelManager.instance.GetCurrentLevel());

    }


}
