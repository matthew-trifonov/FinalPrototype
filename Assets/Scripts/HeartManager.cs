using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHp;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        for(int i = 0; i < heartContainers.value; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
       float tempHp = playerCurrentHp.RuntimeValue / 2;
       for (int i = 0; i < heartContainers.value; i++)
       {
           if (i <= tempHp - 1)
           {
                hearts[i].sprite = fullHeart;
           }
           else if( i >= tempHp)
           {
                hearts[i].sprite = emptyHeart;
           }
           else
           {
                hearts[i].sprite = halfHeart;
           }

       }

    }
}
