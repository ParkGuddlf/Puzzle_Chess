using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPinsInfo : MonoBehaviour
{
    public int shopNum;
    public List<ObjectType> pintype;
    Image spriteImg;
    int i;

    private void Start()
    {
        i = PlayerPrefs.GetInt("skinum");
        spriteImg = GetComponent<Image>();
    }
    private void Update()
    {
        spriteImg.sprite = pintype[i].pinSprite;
        PlayerInfo.instance.pinobjectTypes[shopNum] = pintype[i];
    }

    public void ButtonUp()
    {
        if (i == pintype.Count-1)
        {
            i = 0;
        }
        else
            i++;
        PlayerPrefs.SetInt("skinum", i);
        PlayerPrefs.Save();
    }
    public void ButtonDown()
    {
        if (i == 0)
        {
            i = pintype.Count-1;
        }   
        else
            i--;
        PlayerPrefs.Save();
    }
}
