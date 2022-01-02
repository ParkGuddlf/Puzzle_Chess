using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public GameObject[] shopSlot = new GameObject[4];

    public bool reroll = false;

    AudioSource audioSource;
    private void Awake() {
        if(instance == null)
            instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i< shopSlot.Length;i++ )
        {
            shopSlot[i] = transform.GetChild(i).gameObject;
        }        
    }
    //경험치 구매
    public void ExpBut()
    {
        if (GameManager.Money >= 4)
        {
            audioSource.Play();
            GameManager.Exp += 2;
            GameManager.Money -= 4;
        }        
    }
    //상점 리롤
    public void RerollBtu()
    {
        reroll = false;
        GameManager.instance.Sound(audioSource);
        audioSource.Play();
        StartCoroutine(RerollCo());
    }
    IEnumerator RerollCo()
    {
        if( GameManager.Money >= 1)
        {
            for(int i = 0; i< shopSlot.Length;i++ )
            {
                shopSlot[i].SetActive(false);            
            }
            yield return new WaitForSeconds(0.5f);
            for(int i = 0; i< shopSlot.Length;i++ )
            {
                shopSlot[i].SetActive(true);            
            }
            GameManager.Money -= 1;
        }
    }
}
