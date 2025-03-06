using UnityEngine;

public class LevelPreviewDisplayer : MonoBehaviour
{

    [SerializeField] private Sprite[] LevelPreviewsSprite;
    [SerializeField] private Sprite LockedLevelSprite;
    [SerializeField] private LevelPreview LevelPreviewPrefab;
    private LevelPreview[] LevelPreviews;

    public void Start()
    {

        LevelPreviews=new LevelPreview[LevelPreviewsSprite.Length];
        for(int i=0;i<LevelPreviewsSprite.Length;++i)
        {

            LevelPreviews[i]=Instantiate(LevelPreviewPrefab,this.transform);

            int temp=LevelManager.instance.GetisLevelAvailable(i) ? LevelPreviews[i].SetSprite(LevelPreviewsSprite[i],i,true) : LevelPreviews[i].SetSprite(LockedLevelSprite,i,false);

        }

    }

    public void OnEnable()
    {

        if(LevelPreviews!=null)
            RefreshSprites();

    }

    private void RefreshSprites()
    {

        for(int i=0;i<LevelPreviewsSprite.Length;++i)
        {


            int temp=LevelManager.instance.GetisLevelAvailable(i) ? LevelPreviews[i].SetSprite(LevelPreviewsSprite[i],i,true) : LevelPreviews[i].SetSprite(LockedLevelSprite,i,false);

        }

    }

}
