using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//핀의정보
public enum AtkType
{
    PonA, PonB, PonC, Lock, Bishop, Knight, Queen
}
[CreateAssetMenu(fileName = "Pin.asset", menuName = "Pin/PinObject")]
public class ObjectType : ScriptableObject
{
    public string pinName;
    public int price;
    public Sprite pinSprite;
    public Sprite pinBoardSprite;
    public AtkType atkType;
    public GameObject pinPrefab;
    public int hp;

}
