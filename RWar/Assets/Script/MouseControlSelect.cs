using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.InputSystem; ProjectSetting→Player→OtherSettings→ActiveInputHandlingをInputSystemPackageにする必要がある
public class MouseControlSelect : MonoBehaviour
{
    GameObject selectObj;

    const int LEFT = 0;
    const int RIGHT = 1;
    //const int MIDDLE = 1;//ホイールクリック

    //[SerializeField] float XLIMIT = 8.5f;
   // [SerializeField] float YLIMIT = 0f;
    //[SerializeField] float ZLIMIT = 8.5f;


    CharaUI charaUI;

    bool isCharaSelect = false;
    Vector3 savePos;//キャンセル時に戻す座標を保存

    [SerializeField] float ereaY=-0.8f;
    [SerializeField]GameObject moveEreaGo;
    //[SerializeField] bool isMoveEreaEnable = false;

    [SerializeField] GameObject atkEreaGo;
    //[SerializeField] bool isAtkEreaEnable = false;

    [SerializeField] bool isMoveEnd = false;

    [SerializeField] TurnMgr turnMgr;

    GameObject atkEreaGoSaveEnemy;
    GameObject atkEreaGoSave;
    GameObject moveEreaGoSave;
    void Start()
    {
        charaUI = GameObject.FindGameObjectWithTag("CharaUI").GetComponent<CharaUI>();
    }


    void Update()
    {
        if (!turnMgr.isPlayerTurn) return;
        //if (isTurnChange) return;

        if (Input.GetMouseButtonDown(RIGHT))
        {
            RightClick();
            return;
        }

        /////
        if (isCharaSelect)
        {
            CharaSelectState();
            return;
        }

        /////
        if (!Input.GetMouseButtonDown(LEFT)) return;

        var en = GameObject.FindGameObjectsWithTag("Enemy");
        if (en == null || en.Length <= 0)
        {
            GameObject.FindGameObjectWithTag("GameMgr").GetComponent<GameMgr>().GameEnd(true);
            //ゲーム終了　勝ち
            return;
        }

        if (GameObject.FindGameObjectsWithTag("Chara") == null ||
            GameObject.FindGameObjectsWithTag("Chara").Length <= 0)
        {
            //ゲーム終了　負け
            GameObject.FindGameObjectWithTag("GameMgr").GetComponent<GameMgr>().GameEnd(false);
            return;
        }
        selectObj = MyLib.DebugRayViewCameraPosZ();

        //var createPos = MyLib.DebugRayViewCameraPosXZ();
        charaUI.CharaSelectOff();


        if (selectObj == null) return;
        

        if (selectObj.isStatic)
        {
            selectObj = null;
            return;
        }

        savePos = selectObj.transform.position;

        if (selectObj.tag == "Enemy")
        {
            charaUI.EnemySelectOn();
            Destroy(atkEreaGoSaveEnemy);
            atkEreaGoSaveEnemy =Instantiate(
                selectObj.GetComponent<CharaParam>().playerLengeGoAtk,
                new Vector3(selectObj.transform.position.x, ereaY, selectObj.transform.position.z),
                Quaternion.identity);

            //パラメーター　取得

            SetMobUI();
        }
        else if (selectObj.tag == "Chara")
        {
            charaUI.CharaSelectOn();
            isCharaSelect = true;

            var se = GameObject.FindGameObjectWithTag("SoundM").GetComponent<SoundManager>().se.gameObject;
            var volume = .3f;
            MyLib.MyPlayOneSound("SE/決定ボタンを押す34", volume, se);

            SetMobUI();

            selectObj.GetComponent<RotModule>().speed = 20;
        }

        else if (selectObj.tag == "MoveErea")
        {
            return;
        }
        else
            return;



    }


    void SetMobUI()
    {
        //パラメーター　取得

        charaUI.charaText.text = selectObj.GetComponent<CharaParam>().charaName;
        charaUI.hpText.text = selectObj.GetComponent<CharaParam>().hp.ToString();
        charaUI.atkText.text = selectObj.GetComponent<CharaParam>().atkRank;
        charaUI.defText.text = selectObj.GetComponent<CharaParam>().defRank;
        charaUI.moveText.text = selectObj.GetComponent<CharaParam>().moveRank;
    }



    void CharaSelectState()
    {
        if (!Input.GetMouseButtonDown(LEFT)) return;


        //var select = MyLib.DebugRayViewCameraPosZ();

        var select = MyLib.DebugRayViewCameraPosZLayer("Enemy", "MoveErea");

        if (select == null) return;


        if (select.isStatic/*||selectObj.tag=="Untagged"*/)
        {
            select = null;
            return;
        }
        else if (select.tag == "MoveErea")
        {
            if (isMoveEnd) return;
            selectObj.transform.position = MyLib.DebugRayViewCameraPosXZ();
            GameObject.FindGameObjectsWithTag("MoveErea").ToList().ForEach(x => x.SetActive(false));
            isMoveEnd = true;
            //selectObj.GetComponent<RotModule>().enabled = false;
            return;

        }
        else if (select.tag == "Enemy")
        {
            //AtkEreaの範囲に入っている敵のみ選択可能にする
            if(!select.GetComponent<CharaParam>().isAtkTarget)return;
            

            Debug.Log(select.name+"攻撃した＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿");

            select.GetComponent<ShakeOnceModule>().enabled = true;

            //敵のHP　減少　死亡処理など
            var damageVal = selectObj.GetComponent<CharaParam>().GetAtkRandVal();

            select.GetComponent<CharaParam>().doChangeHpUI.DamegeView(damageVal);

            select.GetComponent<CharaParam>().hp-= damageVal;


            //きゃらごとの攻撃エフェクト
            if(selectObj.GetComponent<CharaParam>().charaName== "MGC")
            {
                Instantiate(GameObject.FindGameObjectWithTag("ParticleMgr").GetComponent<ParticleMgr>().atkFirePt,
                  select.transform.position,
                  Quaternion.identity);
            }
            else
            {
                Instantiate(GameObject.FindGameObjectWithTag("ParticleMgr").GetComponent<ParticleMgr>().atkPt,
                  select.transform.position,
                  Quaternion.identity);
            }


                MyLib.MyPlayOneSound("SE/重いパンチ3", 1f, gameObject);

            if (select.GetComponent<CharaParam>().hp<=0)
                select.GetComponent<TimeDestroy>().SetTime();
            
            //攻撃不可能ならターン終了
            //攻撃可能なら攻撃する　複数なら選択　攻撃後強制終了　ターン
            charaUI.CharaSelectOff();
            isCharaSelect = false;
            selectObj.GetComponent<RotModule>().speed = 0;

            selectObj = null;
            isMoveEnd = false;
            turnMgr.isPlayerTurn = false;

            //　

            StartCoroutine(MyLib.DelayCoroutine(1f, () =>
            {
                turnMgr.ChangeReTurn(true);
                //Camera.main.GetComponent<MoveCamera>().SetDown();

            }));

            GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach
                (x => x.GetComponent<CharaParam>().isAtkTarget = false);
            GameObject.FindGameObjectsWithTag("AtkErea").ToList().ForEach(x => x.SetActive(false));
            //isAtkEreaEnable = false;

            //isMoveEreaEnable = false;
            Destroy(moveEreaGoSave);
            Destroy(atkEreaGoSave);

            return;
   
        }

    }

    //ボタンコンポーネントでの呼び出し
    public void SetMoveEreaGo()
    {
        if (!turnMgr.isPlayerTurn) return;
        //if (isMoveEreaEnable) return;
        if(selectObj==null) return;
        GameObject.FindGameObjectsWithTag("AtkErea").ToList().ForEach(x => x.SetActive(false));
        //isAtkEreaEnable = false;
        if(moveEreaGoSave!=null)
        {
            return;
        }
        Destroy(atkEreaGoSave);
        //Debug.Log(atkEreaGoSave);
        //atkEreaGoSave = null;

        moveEreaGoSave =Instantiate(
            selectObj.GetComponent<CharaParam>().playerLengeGoMove,
            new Vector3(selectObj.transform.position.x, ereaY, selectObj.transform.position.z),
            Quaternion.identity);

        //isMoveEreaEnable = true;
    }

    public void SetAtkEreaGo()
    {
        if (!turnMgr.isPlayerTurn) return;
        //if (isAtkEreaEnable) return;
        if (selectObj == null) return;
        GameObject.FindGameObjectsWithTag("MoveErea").ToList().ForEach(x => x.SetActive(false));
        //isMoveEreaEnable = false;

        if (atkEreaGoSave != null)
        {
            return;
        }

        Destroy(moveEreaGoSave);
        //selectObj.GetComponent<CharaParam>().playerLengeGoAtk.SetActive(true);
        atkEreaGoSave = Instantiate(
            selectObj.GetComponent<CharaParam>().playerLengeGoAtk,
            new Vector3(selectObj.transform.position.x, ereaY, selectObj.transform.position.z),
            Quaternion.identity);

        //isAtkEreaEnable = true;
    }

    public void EndBottun()
    {
        Debug.Log("ボタンが押された");
        isCharaSelect = false;

        charaUI.CharaSelectOff();

        if (selectObj != null)
        {
            selectObj.GetComponent<RotModule>().speed = 0;

            selectObj = null;
        }

        Destroy(moveEreaGoSave);
        Destroy(atkEreaGoSave);

        //isMoveEreaEnable = false;
        //isAtkEreaEnable = false;
        isMoveEnd = false;
        GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach
            (x => x.GetComponent<CharaParam>().isAtkTarget = false);
    }
    //・・・・・・・・・・・・・・・




    void RightClick()
    {
        if (!Input.GetMouseButtonDown(RIGHT)) return;

        var se = GameObject.FindGameObjectWithTag("SoundM").GetComponent<SoundManager>().se.gameObject;
        var volume = .3f;
        MyLib.MyPlayOneSound("SE/ビープ音5", volume, se);
        isCharaSelect = false;

        charaUI.CharaSelectOff();

        if (selectObj != null)
        {
            if(selectObj.GetComponent<RotModule>()!=null)
            selectObj.GetComponent<RotModule>().speed = 0;

            selectObj.transform.position = savePos;
            selectObj = null;
        }
        Destroy(moveEreaGoSave);
        Destroy(atkEreaGoSave);

        isMoveEnd = false;
        GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach
            (x => x.GetComponent<CharaParam>().isAtkTarget = false);
    }


}
