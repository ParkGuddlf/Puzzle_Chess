using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinAtk : MonoBehaviour
{
    public string pinTag;

    [SerializeField] GameObject DmgText;
    //공격대상인식 나일떄는 적만 적일때는 나만
    private void Awake()
    {
        GameManager.instance.Sound(gameObject.GetComponent<AudioSource>());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "nomal")
        {
            switch (collision.tag)
            {
                case "Pin":
                    if (pinTag == "Enemy")
                    {
                        collision.GetComponent<MouseDrag>().hp--;
                        DmgEffect(collision);
                    }
                    break;
                case "Enemy":
                    if (pinTag == "Pin")
                    {
                        collision.GetComponent<MouseDrag>().hp--;
                        DmgEffect(collision);
                    }
                    break;
            }
            
        }
        else if (gameObject.tag == "Razer")
        {
            switch (collision.tag)
            {
                case "Pin":
                    if (pinTag == "Enemy")
                    {
                        MouseDrag[] asd = collision.GetComponents<MouseDrag>();
                        foreach (var item in asd)
                        {
                            item.hp--;
                            DmgEffect(collision);
                        }
                    }
                    break;
                case "Enemy":
                    if (pinTag == "Pin")
                    {
                        MouseDrag[] asdf = collision.GetComponents<MouseDrag>();
                        foreach (var item in asdf)
                        {
                            item.hp--;
                            DmgEffect(collision);
                        }
                    }
                    break;
            }
           
        }
    }
    void DmgEffect(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pin" || collision.gameObject.tag == "Enemy")
        {
            GameObject asd = Instantiate(DmgText, FindObjectOfType<Canvas>().transform);
            asd.transform.position = collision.transform.position;
            asd.GetComponent<DmgTextEffect>().resetPos();
            Destroy(asd, 0.5f);
        }
    }

}
