using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemInfo : MonoBehaviour
{
    public ObjectType[] objectType;

    //레벨에 따른 소환 정보
    public ObjectType currObjectType;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject buyEffect;
    private int length;

    private Button _button;
    private Image _buttonImage;
    private Text _pinNameText;
    private Text _pinPriceText;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonImage = GetComponent<Image>();
        _pinNameText = transform.GetChild(0).GetComponent<Text>();
        _pinPriceText = transform.GetChild(1).GetComponent<Text>();

        //반복문으로 돌려서 집어넣는다
        for (int i = 0; i < PlayerInfo.instance.pinobjectTypes.Count; i++)
        {
            objectType[i] = PlayerInfo.instance.pinobjectTypes[i];
        }
    }

    private void Start()
    {
        currObjectType = objectType[Random.Range(0, GameManager.Level * 2)];
    }

    //리롤하면 정보 초기화
    private void OnEnable()
    {
        //레벨에 맞는 리롤
        currObjectType = objectType[Random.Range(0, GameManager.Level * 2)];

        _buttonImage.sprite = currObjectType.pinSprite;
        _button.interactable = true;
    }

    private void Update()
    {
        if (currObjectType != null)
        {
            _pinNameText.text = currObjectType.pinName;
            _pinPriceText.text = currObjectType.price.ToString();
        }
    }

    //버튼을 누르면 아이템의 정보를 인벤토리에 집어넣는다.
    public void BuyItem()
    {
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            Transform currParent = inventory.transform.GetChild(i);
            if (currParent.transform.childCount == 0 && GameManager.Money >= currObjectType.price)
            {
                //가격에 따라 돈이 줄어든다.
                GameManager.Money -= currObjectType.price;
                //구입버튼 잠금
                _button.interactable = false;
                
                GameObject spawnPin = Instantiate(currObjectType.pinPrefab, Vector3.zero, Quaternion.identity,currParent);
                spawnPin.transform.localPosition = Vector3.zero;

                //생성한 프리팹의 기본정보 변경
                spawnPin.GetComponent<MouseDrag>().currParent = currParent;
                spawnPin.GetComponent<MouseDrag>().objectType = currObjectType;
                
                //이펙트 생성
                GameObject spawnEffect = Instantiate(buyEffect, currParent);
                Destroy(spawnEffect, 0.5f);
                
                currObjectType = null;
                break;
            }
        }
    }
}