using UnityEngine;

public class CharaParam : CharaBase
{
    public string charaName;
    public int hp;
    public string atkRank;
    public string defRank;
    public string moveRank;

    public string skill;



    [SerializeField] int randNo = 3;
    public DamageUI damageUI;

    public bool isAtkTarget = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestBut()
    {
        Debug.Log("Test");
    }



    public int GetAtkRandVal()
    {
        var randHit = Random.Range(0, randNo + 1);
        int atkVal = 0;
        if (atkRank == "S")
            atkVal = 30;
        if (atkRank == "A")
            atkVal = 25;
        if (atkRank == "B")
            atkVal = 15;
        if (atkRank == "C")
            atkVal = 5;

        return atkVal+randHit;
    }
}
