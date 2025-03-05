using UnityEngine;

public class LevelPreviewDisplayer : MonoBehaviour
{

    [SerializeField] private Sprite[] LevelPreviewsSprite;
    [SerializeField] private LevelPreview LevelPreviewPrefab;
    private LevelPreview[] LevelPreviews;

    public void Start()
    {

        LevelPreviews=new LevelPreview[LevelPreviewsSprite.Length];
        for(int i=0;i<LevelPreviewsSprite.Length;++i)
        {

            LevelPreviews[i]=Instantiate(LevelPreviewPrefab,this.transform);
            LevelPreviews[i].SetSprite(LevelPreviewsSprite[i],i);

        }

    }

}
