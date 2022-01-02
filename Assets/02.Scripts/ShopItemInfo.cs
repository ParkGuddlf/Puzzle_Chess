using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemInfo : MonoBehaviour
{
    public ObjectType[] objectType;
    //레벨에 따른 소환 정보
    int length;
    public ObjectType currObjectType;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject buyEffect;

    private void Awake()
    {
        //반복문으로 돌려서 집어넣는다
        for (int i = 0; i < PlayerInfo.instance.pinobjectTypes.Count; i++)
        {
            objectType[i] = PlayerInfo.instance.pinobjectTypes[i];
        }        
    }

    private void Start()
    {
        currObjectType = objectType[Random.Range(0, GameManager.Level*2)];
    }
    //리롤하면 정보 초기화
    private void OnEnable()
    {
        //레벨에 맞는 리롤
        currObjectType = objectType[Random.Range(0, GameManager.Level*2)];
        GetComponent<Image>().sprite = currObjectType.pinSprite;
        GetComponent<Button>().interactable = true;
    }
    private void Update()
    {
        if (currObjectType != null)
        {
            transform.GetChild(0).GetComponent<Text>().text = currObjectType.pinName;
            transform.GetChild(1).GetComponent<Text>().text = currObjectType.price.ToString();
        }
    }
    //버튼을 누르면 아이템의 정보를 인벤토리에 집어넣는다.
    public void BuyItem()
    {
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            if (inventory.transform.GetChild(i).transform.childCount == 0 && GameManager.Money >= currObjectType.price)
            {
                GameObject spawnPin = Instantiate(currObjectType.pinPrefab, Vector3.zero, Quaternion.identity);
                //가격에 따라 돈이 줄어든다.
                GameManager.Money -= currObjectType.price;
                //구입버튼 잠금
                GetComponent<Button>().interactable = false;
                //비어있는 창고로 넣는다
                spawnPin.transform.SetParent(inventory.transform.GetChild(i));
                GameObject spawn = Instantiate(buyEffect, inventory.transform.GetChild(i));
                Destroy(spawn,0.5f);
                //생성한 프리팹의 기본정보 변경
                spawnPin.GetComponent<MouseDrag>().currParent = inventory.transform.GetChild(i);
                spawnPin.GetComponent<MouseDrag>().objectType = currObjectType;
                spawnPin.transform.localPosition = Vector3.zero;
                currObjectType = null;
                break;
            }
        }
    }

}
