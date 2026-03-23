using UnityEngine;

public class CharaParam : CharaBase
{
    public GameObject playerLengeGoAtk=null;
    public GameObject playerLengeGoMove= null;

    public string charaName;
    public int hp;
    public string atkRank;
    public string defRank;
    public string moveRank;

    public string atkLengeRank;

    public string skill;

    public DOChangeHpUI doChangeHpUI;

    [SerializeField] int randNo = 3; //乱数　攻撃に加算される値の上限

    public bool isAtkTarget = false;// 攻撃範囲内かどうか

    public int GetAtkRandVal()
    {
        var randHit = Random.Range(0, randNo + 1);
        int atkVal = 0;
        if (atkRank == "S")
            atkVal = 30;
        else if (atkRank == "A")
            atkVal = 25;
        else if (atkRank == "B")
            atkVal = 15;
        else if (atkRank == "C")
            atkVal = 5;
        else
            Debug.Log("ランクエラー　:");

        return atkVal+randHit;
    }

    public float GetAtkLengeRankVal()
    {

        float val = 0;
        if (atkLengeRank == "S")
            val = 4;
        else if (atkLengeRank == "A")
            val = 3;
        else if (atkLengeRank == "B")
            val = 2;
        else if (atkLengeRank == "C")
            val = 1f;
        else
            Debug.Log("ランクエラー　:");

        return val;
    }

    public float GetMoveRankVal()
    {

        float val = 0;
        if (moveRank == "S")
            val = 4;
        else if (moveRank == "A")
            val = 3;
        else if (moveRank == "B")
            val = 2;
        else if (moveRank == "C")
            val = 1f;
        else
            Debug.Log("ランクエラー　:");

            return val;
    }

}
