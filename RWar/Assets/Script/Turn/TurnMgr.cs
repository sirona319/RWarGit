using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnMgr : MonoBehaviour
{
    [SerializeField]  TargetRot targetRot;
    [SerializeField] TMP_Text turnText;
    public bool isEnemyTurn = false;
    [SerializeField] SupTurn supTurn;

    public EnemyTurn et;

    public bool isPlayerTurn = false;

    public bool isReTurn = false;


    //public bool isWait = false;

    public void ChangeEnemyTurn(bool isTurn)
    {
        if(isTurn)turnText.text = "Enemy Turn";
        //Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, 0);
        isEnemyTurn = isTurn;
        et.gameObject.SetActive(isTurn);
    }
    public void ChangePlayerTurn(bool isTurn)
    {
        if (isTurn) turnText.text = "Player Turn";

        //Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, 0);
        isPlayerTurn = isTurn;
    }
    public void ChangeReTurn(bool isTurn)
    {
        var en = GameObject.FindGameObjectsWithTag("Enemy");
        if (en == null || en.Length <= 0)
        {
            GameObject.FindGameObjectWithTag("GameMgr").GetComponent<GameMgr>().GameEnd(true);
            //ゲーム終了　勝ち
            return;
        }

        if (GameObject.FindGameObjectsWithTag("Chara") == null || GameObject.FindGameObjectsWithTag("Chara").Length <= 0)
        {
            //ゲーム終了　負け
            GameObject.FindGameObjectWithTag("GameMgr").GetComponent<GameMgr>().GameEnd(false);
            return;
        }

        if (isTurn) turnText.text = "Sup Turn";

        if (isTurn)
            targetRot.SetDown();
        else
            targetRot.SetUp();
        supTurn.isActive = isTurn;
    }

    private void Start()
    {
        ChangePlayerTurn(true);

    }


    private void Update()
    {
        if(isPlayerTurn)
        {
            GetComponent<Button>().enabled = true;
        }
        else
        {
            GetComponent<Button>().enabled = false;
        }

        //if (!isWait) return;

        //if (!isEnemyTurn && !isPlayerTurn)
        //{
        //    isPlayerTurn = true;
        //    //裏ターンへ移行
        //}

        //RandomEnemySelect();
    }

   
}
