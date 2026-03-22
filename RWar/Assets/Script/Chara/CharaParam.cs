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



    [SerializeField] int randNo = 3; //乱数　攻撃に加算される値の上限

    public bool isAtkTarget = false;

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
        //var randHit = Random.Range(0, randNo + 1);
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

    public float GetMoveRankVal()
    {
        //var randHit = Random.Range(0, randNo + 1);
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


    //public bool RanKUp(ref string rank)
    //{
    //    if (rank == "S")
    //        return false;
    //    else if (rank == "A")
    //        rank = "S";
    //    else if (rank == "B")
    //        rank = "A";
    //    else if (rank == "C")
    //        rank = "B";
    //    else
    //        Debug.Log("ランクエラー　:");

    //    return true;
    //}

    //public bool RanKDown(ref string rank)
    //{
    //    if (rank == "S")
    //        rank = "A";
    //    else if (rank == "A")
    //        rank = "B";
    //    else if (rank == "B")
    //        rank = "C";
    //    else if (rank == "C")
    //        return false;
    //    else
    //        Debug.Log("ランクエラー　:");

    //    return true;
    //}
}
