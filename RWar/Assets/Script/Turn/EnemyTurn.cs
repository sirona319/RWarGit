using UnityEngine;
using UnityEngine.UI;

public class EnemyTurn : MonoBehaviour
{
    /// <summary>
    /// 敵ターン
    /// </summary>
    [SerializeField] Vector3 sPos = new Vector3(0, 0, 0);
    [SerializeField] bool isPosSave = false;
    //float moveLngth = 0f;
    CharaParam enParam;

    [SerializeField] float DEBUGLEN = 0f;
    [SerializeField] float DEBUGLENATK = 0f;

    [SerializeField] TurnMgr turnMgr;
    private void Start()
    {
        //turnMgr = GameObject.FindGameObjectWithTag("TurnMgr").GetComponent<TurnMgr>();
        //gameObject.SetActive(false);
    }

    private void Update()
    {
        RandomEnemySelect();
    }

    private void OnEnable()
    {
        var en = GameObject.FindGameObjectsWithTag("Enemy");
        if(en==null||en.Length<=0)
        {
            turnMgr.ChangeEnemyTurn(false);
            turnMgr.GetComponent<Image>().color = new Color(0.5f, 1, 1, 1f);
            Debug.Log("敵がいないからゲーム終了"+ en.Length);

            //ゲーム終了　勝ち
            return;
            //gameObject.SetActive(false);
        }
        var randHit = Random.Range(0, en.Length);
        enParam = en[randHit].GetComponent<CharaParam>();

        sPos = enParam.transform.position;

        turnMgr = GameObject.FindGameObjectWithTag("TurnMgr").GetComponent<TurnMgr>();

        turnMgr.GetComponent<Image>().color = new Color(1, 0, 0, 1);

    }

    public void RandomEnemySelect()
    {
        if (!GameObject.FindGameObjectWithTag("TurnMgr").GetComponent<TurnMgr>().isEnemyTurn)
        {
            return;
        }
        bool isGameEnd = AtkLengeCteck();
        if (isGameEnd) return;
        //if (!isPosSave)
        //{
        //    sPos = en[randHit].transform.position;
        //    isPosSave = true;
        //}
        var tc = MyLib.CharaNearScr(enParam.transform.position, "Chara");
        var cPos = tc.transform.position;//一番近い味方キャラを取得

        //ムーブランクに応じて移動距離変更
        var direction = (cPos - enParam.transform.position).normalized;
        var spd = 1f;
        enParam.transform.position += direction * spd * Time.deltaTime;



        float dis = Vector3.Distance(enParam.transform.position, sPos);
        DEBUGLEN = dis;
        if (dis > enParam.GetMoveRankVal())
        {
            AtkLengeCteck();
            //    var atkDis = Vector3.Distance(enParam.transform.position, cPos);
            //    //DEBUGLENATK= atkDis;
            //    if (atkDis < enParam.GetAtkLengeRankVal())
            //    {
            //        //攻撃
            //        //enParam.GetComponent<EnemyBase>().Attack();

            //        Debug.Log(enParam.name + "ENEMY　攻撃した＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿");

            //        //揺らす長さ
            //        const float shakeLength = 0.15f;
            //        //揺らす力
            //        const float power = 0.3f;
            //        StartCoroutine(MyLib.DoShake(shakeLength, power, enParam.transform));
            //        //敵のHP　減少　死亡処理など
            //        var damageVal = enParam.GetAtkRandVal();
            //        tc.GetComponent<CharaParam>().damageUI.DamegeView(damageVal);
            //        tc.GetComponent<CharaParam>().hp -= damageVal;

            //        if (enParam.hp <= 0)
            //            tc.GetComponent<TimeDestroy>().SetTime();
            //    }
            //    //移動終了　攻撃判定へ

            //isPosSave = false;
            turnMgr.ChangePlayerTurn(true);
            turnMgr.ChangeEnemyTurn(false);
            turnMgr.GetComponent<Image>().color = new Color(0.5f, 1, 1, 1f);

            gameObject.SetActive(false);
        }
    }

    bool AtkLengeCteck()
    {
        if(GameObject.FindGameObjectsWithTag("Chara")==null|| GameObject.FindGameObjectsWithTag("Chara").Length<=0)
        {
            //ゲーム終了　負け
            Debug.Log("味方いないからターン終了 ゲーム終了　負け");
            turnMgr.ChangeEnemyTurn(false);
            return true;
        }
        var tcParam = MyLib.CharaNearScr(enParam.transform.position, "Chara").GetComponent<CharaParam>();
        var cPos = tcParam.transform.position;//一番近い味方キャラを取得
        var atkDis = Vector3.Distance(enParam.transform.position, cPos);
        DEBUGLENATK = atkDis;
        if (atkDis < enParam.GetAtkLengeRankVal())
        {
            //攻撃
            //enParam.GetComponent<EnemyBase>().Attack();

            Debug.Log(enParam.name + "ENEMY　攻撃した＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿");

            tcParam.GetComponent<ShakeOnceModule>().enabled = true;
            ////揺らす長さ
            //const float shakeLength = 0.15f;
            ////揺らす力
            //const float power = 0.3f;
            //StartCoroutine(MyLib.DoShake(shakeLength, power, tcParam.transform));
            //敵のHP　減少　死亡処理など
            var damageVal = enParam.GetAtkRandVal();


            tcParam.damageUI.DamegeView(damageVal);
            tcParam.hp -= damageVal;

            if (tcParam.hp <= 0)
                tcParam.GetComponent<TimeDestroy>().SetTime();

            //移動終了　攻撃判定へ

            //isPosSave = false;

            //StartCoroutine(MyLib.DelayCoroutine(1f, () =>
            //{
            //    turnMgr.ChangePlayerTurn(true);

            //}));
            turnMgr.ChangePlayerTurn(true);

            //ターン終了
            turnMgr.ChangeEnemyTurn(false);
            turnMgr.GetComponent<Image>().color = new Color(0.5f, 1, 1, 1f);

            gameObject.SetActive(false);
        }

        return false;
    }
}
