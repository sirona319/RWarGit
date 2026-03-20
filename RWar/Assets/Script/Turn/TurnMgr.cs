using UnityEngine;
using UnityEngine.UI;

public class TurnMgr : MonoBehaviour
{

    public bool isEnemyTurn { get; private set; } = false;

    public EnemyTurn et;

    public bool isPlayerTurn = false;

    public bool isReTurn = false;

    public void ChangeEnemyTurn(bool isTurn)
    {
        isEnemyTurn = isTurn;
        et.gameObject.SetActive(isTurn);
    }
    public void ChangePlayerTurn(bool isTurn)
    {
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

        //if (!isEnemyTurn && !isPlayerTurn)
        //{
        //    isPlayerTurn = true;
        //    //裏ターンへ移行
        //}

        //RandomEnemySelect();
    }

   
}
