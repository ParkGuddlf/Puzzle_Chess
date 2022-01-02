using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    public float mouseX;
    public float mouseY;
    //현재들어있는 칸정보
    public Transform currParent;
    //눌렀는지 안눌렀는지
    public bool isBeingHeld = false;
    //말에 들어있는 Pin정보
    public ObjectType objectType;

    SpriteRenderer spriteRen;
       
    public int hp;
    [SerializeField] GameObject hpBar;

    AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips;

    private void Start()
    {
        hp = objectType.hp;
        hpBar = transform.GetChild(1).gameObject;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = objectType.atkArrow;
        audioSource = GetComponent<AudioSource>();
        if (gameObject.tag == "Pin")
            audioSource.Play();
        spriteRen = GetComponent<SpriteRenderer>();
        spriteRen.sprite = objectType.pinBoardSprite;
    }
    void Update()
    {
        GameManager.instance.Sound(audioSource);
        //이동
        if (isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);

        }
        //놓기
        else if (isBeingHeld == false)
        {            
            transform.SetParent(currParent);
            transform.localPosition = new Vector3(0.1f, 0, 0);
            transform.localPosition = Vector3.zero;
        }
        switch (hp)
        {
            case 3:
                hpBar.transform.GetChild(2).gameObject.SetActive(true);
                hpBar.transform.GetChild(1).gameObject.SetActive(true);
                hpBar.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                hpBar.transform.GetChild(2).gameObject.SetActive(false);
                hpBar.transform.GetChild(1).gameObject.SetActive(true);
                hpBar.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 1:
                hpBar.transform.GetChild(2).gameObject.SetActive(false);
                hpBar.transform.GetChild(1).gameObject.SetActive(false);
                hpBar.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 0:
                hpBar.transform.GetChild(2).gameObject.SetActive(false);
                hpBar.transform.GetChild(1).gameObject.SetActive(false);
                hpBar.transform.GetChild(0).gameObject.SetActive(false);
                break;
        }        

        if (hp <= 0)
        {//사라지면서 배틀보드의 적이면 적리스트 아군ㅇ면 아군리스트에서 자신을 지운다

            switch (gameObject.tag)
            {
                case "Enemy":
                    for (int i = 0; i < BattleBoard.instance.enemyPinList.Count; i++)
                    {

                        if (BattleBoard.instance.enemyPinList[i] == transform.parent.gameObject)
                            BattleBoard.instance.enemyPinList.RemoveAt(i);
                    }
                    break;
            }
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.tag == "Pin" && TurnManager.instance.stateCur == StateCur.main)
        {
            currParent = transform.parent;
            if (BattleBoard.instance.pinCountInBpard.Contains(gameObject))
            {
                for (int i = 0; i < BattleBoard.instance.pinCountInBpard.Count; i++)
                {
                    if (BattleBoard.instance.pinCountInBpard[i] == gameObject)
                    {
                        BattleBoard.instance.pinCountInBpard.RemoveAt(i);
                        break;
                    }
                }
            }
            if (currParent == transform.parent)
                transform.SetParent(null);
            isBeingHeld = true;
        }
    }
    private void OnMouseUp()
    {
        isBeingHeld = false;        
        audioSource.PlayOneShot(audioClips[0]);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //넣으려는 곳에 말이 없을때 판위의 말개수가 레벨보다 작을때
        if (collision.tag == "Case" && isBeingHeld == true && collision.transform.childCount == 0 && GameManager.Level > BattleBoard.instance.pinCountInBpard.Count)
        {
            currParent = collision.transform;
        }
        if (collision.tag == "Inventory" && isBeingHeld == true && collision.transform.childCount == 0)
            currParent = collision.transform;
    }

}
