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

    private void Awake()
    {
        //플레이어인포에 있는 스킨번호를 i에 넣어준다
        
    }
    private void Start()
    {
        spriteImg = GetComponent<Image>();
        i = PlayerInfo.instance.skinArray[shopNum];
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
        PlayerInfo.instance.MyShopInfo(shopNum, i);
    }
    public void ButtonDown()
    {
        if (i == 0)
        {
            i = pintype.Count-1;
        }   
        else
            i--;
        PlayerInfo.instance.MyShopInfo(shopNum, i);
    }
}
