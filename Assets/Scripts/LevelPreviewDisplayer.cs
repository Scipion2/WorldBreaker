using UnityEngine;

public class LevelPreviewDisplayer : MonoBehaviour
{

    [SerializeField] private Sprite[] LevelPreviewsSprite;
    [SerializeField] private GameObject LevelPreviewPrefab;
    private GameObject[] LevelPreviews;

    public void OnAwake()
    {

        LevelPreviews=new GameObject[LevelPreviewsSprite.Length];
        for(int i=0;i<LevelPreviewsSprite.Length;LevelPreviews[i++]=Instantiate(LevelPreviewPrefab,this.transform)){}

    }

}
