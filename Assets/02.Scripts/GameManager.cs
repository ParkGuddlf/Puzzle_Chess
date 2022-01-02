using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //돈관리 경험치 레벨
    private static int life = 10;
    private static int level = 1;
    private static int exp;
    private static int currExp = 0;
    private static int money = 0;

    public static int Life { get => life; set => life = value; }
    public static int Money { get => money; set => money = value; }
    public static int Exp { get => currExp; set => currExp = value; }
    public static int Level { get => level; }

    [SerializeField] Text levelText;
    [SerializeField] Text expText;
    [SerializeField] Text moneyText;
    [SerializeField] Text stageText;
    [SerializeField] Text stageTextBar;
    [SerializeField] Text lifeTextBar;
    [SerializeField] GameObject gameover;
    [SerializeField] Slider soundSlider;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        soundSlider.value = PlayerInfo.instance.sound;
    }
    private void Start()
    {
        //저장정보가 있으면 저장정보에 따라 불러오고 아니면 처음부터 불러온다
        switch (PlayerInfo.instance.life)
        {
            case 0:
                life = 10;
                level = 1;
                currExp = 0;
                money = 0;
                break;
            default:
                life = PlayerInfo.instance.life;
                level = PlayerInfo.instance.level;
                currExp = PlayerInfo.instance.exp;
                money = PlayerInfo.instance.gold;
                
                break;
        }

    }

    void Update()
    {
        LevelToExp();
        if (life > 0)
            Turn();
        else
            gameover.SetActive(true);
        levelText.text = "Level : " + level;
        expText.text = "EXP : " + currExp + "/" + exp;
        moneyText.text = "GOLD : " + money;
        lifeTextBar.text = "LIFE : " + life;
        stageText.text = "Stage " + StageManager.instance.stageNum;
        if (TurnManager.instance.stateCur != StateCur.battle)
            stageTextBar.text = "Stage " + StageManager.instance.stageNum;

    }
    //레벨업 및 최대경험치
    void LevelToExp()
    {
        switch (level)
        {
            case 1:
                exp = 4;
                break;
            case 2:
                exp = 8;
                break;
            case 3:
                exp = 16;
                break;
            case 4:
                exp = 32;
                break;
        }
        if (currExp == exp)
        {
            level++;
            currExp = 0;
        }
    }
    public void Sound(AudioSource ads)
    {
        
        ads.volume = soundSlider.value;
        if(ads.volume != PlayerInfo.instance.sound)
        {
            PlayerInfo.instance.sound = ads.volume;
            PlayerInfo.instance.GameSystemInfo();
        }
    }

    //엔드턴은 체력관리와 게임오버판단
    void Turn()
    {
        switch (TurnManager.instance.stateCur)
        {
            //시작턴 시작시 돈과 경험치 지급
            case StateCur.start:
                money += 1;
                ShopManager.instance.RerollBtu();                
                stageTextBar.transform.parent.gameObject.SetActive(true);
                if (StageManager.instance.enemyArrangeEnd == true)
                {
                    BattleBoard.battleEnd = false;
                    money += 10;
                    currExp += 2;
                    if (currExp == exp)
                    {
                        level++;
                        currExp = 0;
                    }
                    TurnManager.instance.stateCur = StateCur.main;
                }
                break;
            //메인턴은 기물합성 구매 배치
            case StateCur.main:
                if (BattleBoard.instance.enemyPinList.Count == 0)
                    TurnManager.instance.stateCur = StateCur.end;
                break;
            case StateCur.battle:
                if (BattleBoard.battleStart == false)
                    BattleBoard.instance.BattelPage();
                if (BattleBoard.instance.enemyPinList.Count == 0)
                    TurnManager.instance.stateCur = StateCur.end;
                break;
            case StateCur.end:
                stageTextBar.transform.parent.gameObject.SetActive(false);
                GameinfoSave();
                //공격이 끝나면 넘어간다
                if (BattleBoard.battleEnd == true && InventoryManager.isSave)
                    TurnManager.instance.stateCur = StateCur.start;
                break;
        }
    }
    //배틀스타트버튼
    public void BattleButton()
    {
        if (TurnManager.instance.stateCur == StateCur.main)
            TurnManager.instance.stateCur = StateCur.battleReady;
    }
    //Save목록
    void GameinfoSave()
    {
        PlayerInfo.instance.stageNum = StageManager.instance.stageNum;
        PlayerInfo.instance.level = level;
        PlayerInfo.instance.gold = money;
        PlayerInfo.instance.exp = currExp;
        PlayerInfo.instance.life = life;
        PlayerInfo.instance.GameManegerSave();
    }
}
