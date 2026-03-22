using System.Linq;
using UnityEngine;

public class SupTurn : MonoBehaviour
{

    public bool isActive = false;

    [SerializeField] TurnMgr turnMgr;

    const float turnChangeTime = 0.8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        const int LEFT = 0;
        if (!Input.GetMouseButtonDown(LEFT)) return;

        var selectObj = MyLib.DebugRayViewCameraPosZ();

        if (selectObj == null) return;


        if (selectObj.isStatic)
        {
            selectObj = null;
            return;
        }

        if(selectObj.gameObject.tag=="Sup")
        {
            if (selectObj.GetComponent<RotModule>().enabled) return;


            GameObject.FindGameObjectsWithTag("Sup").ToList().ForEach(g => g.GetComponent<RotModule>().enabled = false);
            //四種類効果
            //ランダムキャラ回復大　　全体バフ弱　　　全体敵ダメージ小　　ランダム敵デバフ強
            selectObj.GetComponent<RotModule>().enabled = true;

            selectObj.GetComponent<Sup>().SupSkill(selectObj);


            StartCoroutine(MyLib.DelayCoroutine(turnChangeTime, () =>
            {
                turnMgr.ChangeEnemyTurn(true);

            }));
            //Camera.main.GetComponent<MoveCamera>().SetUp();
            turnMgr.ChangeReTurn(false);
            return;
        }
    }
}
