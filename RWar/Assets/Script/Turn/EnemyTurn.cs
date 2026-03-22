using UnityEngine;
using UnityEngine.UI;

public class EnemyTurn : MonoBehaviour
{

    [SerializeField] Vector3 sPos = new Vector3(0, 0, 0);
    //[SerializeField] bool isPosSave = false;

    CharaParam enParam;

    [SerializeField] float DEBUGLEN = 0f;
    [SerializeField] float DEBUGLENATK = 0f;

    [SerializeField] TurnMgr turnMgr;
    private void Start()
    {

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
            GameObject.FindGameObjectWithTag("GameMgr").GetComponent<GameMgr>().GameEnd(true);
            //ゲーム終了　勝ち
            return;
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
            GameObject.FindGameObjectWithTag("GameMgr").GetComponent<GameMgr>().GameEnd(false);
            return true;
        }

        var tcParam = MyLib.CharaNearScr(enParam.transform.position, "Chara").GetComponent<CharaParam>();
        var cPos = tcParam.transform.position;//一番近い味方キャラを取得
        var atkDis = Vector3.Distance(enParam.transform.position, cPos);
        DEBUGLENATK = atkDis;

        //攻撃範囲内に入っていたら攻撃
        if (atkDis < enParam.GetAtkLengeRankVal())
        {

            Debug.Log(enParam.name + "ENEMY　攻撃した＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿");

            tcParam.GetComponent<ShakeOnceModule>().enabled = true;

            //敵のHP　減少　死亡処理など
            var damageVal = enParam.GetAtkRandVal();
            tcParam.GetComponent<DOChangeHpUI>().DamegeView(enParam.GetAtkRandVal());
            tcParam.hp -= damageVal;

            Instantiate(GameObject.FindGameObjectWithTag("ParticleMgr").GetComponent<ParticleMgr>().atkPt,
              tcParam.transform.position,
              Quaternion.identity);

  ;
            MyLib.MyPlayOneSoundSingle("SE/重いパンチ3", 1f, GameObject.FindGameObjectWithTag("SoundM").GetComponent<SoundManager>().se.gameObject);

            if (tcParam.hp <= 0)
                tcParam.GetComponent<TimeDestroy>().SetTime();



            //StartCoroutine(MyLib.DelayCoroutine(1.5f, () =>
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
