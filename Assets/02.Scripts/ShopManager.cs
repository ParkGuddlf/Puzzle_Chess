using System.Collections;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    private GameObject[] shopSlots;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        shopSlots = new GameObject[4];
        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i] = transform.GetChild(i).gameObject;
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
        GameManager.instance.Sound(audioSource);
        audioSource.Play();

        if (GameManager.Money >= 1)
        {
            GameManager.Money -= 1;
            StartCoroutine(RerollCo());
        }
    }

    private IEnumerator RerollCo()
    {
        SetShopSlots(false);
        yield return new WaitForSeconds(0.5f);
        SetShopSlots(true);
    }

    private void SetShopSlots(bool active)
    {
        foreach (var shopSlot in shopSlots)
        {
            shopSlot.SetActive(active);
        }
    }
}