using UnityEngine;
using UnityEngine.UI;

public class TurnMgr : MonoBehaviour
{

    public bool isEnemyTurn { get; private set; } = false;

    public EnemyTurn et;

    public bool isPlayerTurn = false;

    public bool isReTurn = false;


    public bool isWait = false;

    public void ChangeEnemyTurn(bool isTurn)
    {
        Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, 0);
        isEnemyTurn = isTurn;
        et.gameObject.SetActive(isTurn);
    }
    public void ChangePlayerTurn(bool isTurn)
    {
        Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, 0);
        isPlayerTurn = isTurn;
    }
    public void ChangeReTurn(bool isTurn)
    {
        isReTurn = isTurn;
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

        if (!isWait) return;

        //if (!isEnemyTurn && !isPlayerTurn)
        //{
        //    isPlayerTurn = true;
        //    //裏ターンへ移行
        //}

        //RandomEnemySelect();
    }

   
}
