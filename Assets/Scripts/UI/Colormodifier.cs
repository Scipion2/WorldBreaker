using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Colormodifier : MonoBehaviour
{
    
    [SerializeField] private Image imageToModify;
    [SerializeField] private TextMeshProUGUI textToModify;
    [SerializeField] private SpriteRenderer spriteToModify;
    private Color currentColor;
    [SerializeField] [Range(0,255)] private float Red,Blue,Green;
    private float ColorDelta=0.25f;
    public bool isAutoModifier=true;

    public void Start()
    {

        int temp=Random.Range(0,100)%3;

        Red= temp==0 ? 255 : temp==1 ? 0 : (float)(int)Random.Range(0,255);
        Blue= temp==1 ? 255 : temp==2 ? 0 : (float)(int)Random.Range(0,255);
        Green= temp==2 ? 255 : temp==0 ? 0 : (float)(int)Random.Range(0,255);

    }

    public void Update()
    {

        if(isAutoModifier)
            Modifier();

    }

    private void Modifier()
    {

        if(Red==255 && Blue==0 && Green!=0)
        {

            Green-=ColorDelta;

        }

        if(Red==255 && Blue!=255 && Green==0)
        {

            Blue+=ColorDelta;

        }

        if(Red!=0 && Blue==255 && Green==0)
        {

            Red-=ColorDelta;

        }

        if(Red==0 && Blue==255 && Green!=255)
        {

            Green+=ColorDelta;

        }

        if(Red==0 && Blue!=0 && Green==255)
        {

            Blue-=ColorDelta;

        }

        if(Red!=255 && Blue==0 && Green==255)
        {

            Red+=ColorDelta;

        }


        currentColor=new Color(Red/255,Green/255,Blue/255,1);


        if(imageToModify != null)
        {

            imageToModify.color=currentColor;

        }

        if(textToModify != null)
        {

            textToModify.color=currentColor;

        }

        if(spriteToModify != null)
        {

            spriteToModify.color=currentColor;

        }

    }

}
