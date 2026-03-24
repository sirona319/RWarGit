using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.InputSystem; ProjectSetting→Player→OtherSettings→ActiveInputHandlingをInputSystemPackageにする必要がある
public class MouseControlSelect : MonoBehaviour
{
    GameObject selectObjFirst;

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

    [SerializeField] GameObject atkEreaGo;

    [SerializeField] bool isMoveEnd = false;

    [SerializeField] TurnMgr turnMgr;

    CharaParam atkEreaGoSaveEnemy;

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

        selectObjFirst = MyLib.DebugRayViewCameraPosZ();
        charaUI.CharaSelectOff();


        if (selectObjFirst == null) return;
        

        if (selectObjFirst.isStatic)
        {
            selectObjFirst = null;
            return;
        }

        savePos = selectObjFirst.transform.position;

        if (selectObjFirst.tag == "Enemy")
        {
            charaUI.EnemySelectOn();

            if(atkEreaGoSaveEnemy!=null)
                atkEreaGoSaveEnemy.playerLengeGoAtk.SetActive(false);

            selectObjFirst.GetComponent<CharaParam>().playerLengeGoAtk.SetActive(true);
            atkEreaGoSaveEnemy = selectObjFirst.GetComponent<CharaParam>();


            SetMobUI();
        }
        else if (selectObjFirst.tag == "Chara")
        {
            charaUI.CharaSelectOn();
            isCharaSelect = true;

            var se = GameObject.FindGameObjectWithTag("SoundM").GetComponent<SoundManager>().se.gameObject;
            var volume = .3f;
            MyLib.MyPlayOneSound("SE/決定ボタンを押す34", volume, se);

            SetMobUI();

            selectObjFirst.GetComponent<RotModule>().speed = 20;
        }

        else if (selectObjFirst.tag == "MoveErea")
        {
            return;
        }
        else
            return;



    }

    //パラメーター　取得

    void SetMobUI()
    {
        charaUI.charaText.text = selectObjFirst.GetComponent<CharaParam>().charaName;
        charaUI.hpText.text = selectObjFirst.GetComponent<CharaParam>().hp.ToString();
        charaUI.atkText.text = selectObjFirst.GetComponent<CharaParam>().atkRank;
        charaUI.defText.text = selectObjFirst.GetComponent<CharaParam>().defRank;
        charaUI.moveText.text = selectObjFirst.GetComponent<CharaParam>().moveRank;
    }



    void CharaSelectState()
    {
        if (!Input.GetMouseButtonDown(LEFT)) return;


        //var select = MyLib.DebugRayViewCameraPosZ();

        var select = MyLib.DebugRayViewCameraPosZLayer("Enemy", "MoveErea");

        if (select == null) return;


        if (select.isStatic/*||selectObjFirst.tag=="Untagged"*/)
        {
            select = null;
            return;
        }
        else if (select.tag == "MoveErea")
        {
            if (isMoveEnd) return;
            selectObjFirst.transform.position = MyLib.DebugRayViewCameraPosXZ();
            GameObject.FindGameObjectsWithTag("MoveErea").ToList().ForEach(x => x.SetActive(false));
            isMoveEnd = true;
            //selectObjFirst.GetComponent<RotModule>().enabled = false;
            return;

        }
        else if (select.tag == "Enemy")
        {
            //AtkEreaの範囲に入っている敵のみ選択可能にする
            if(!select.GetComponent<CharaParam>().isAtkTarget)return;
            Debug.Log(select.name+"攻撃した＿");
            GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach
                        (x => x.GetComponent<CharaParam>().isAtkTarget = false);
            selectObjFirst.GetComponent<CharaParam>().playerLengeGoAtk.SetActive(false);

            select.GetComponent<ShakeOnceModule>().enabled = true;

            //敵のHP　減少　死亡処理など
            var damageVal = selectObjFirst.GetComponent<CharaParam>().GetAtkRandVal();

            select.GetComponent<CharaParam>().doChangeHpUI.DamegeView(damageVal);

            select.GetComponent<CharaParam>().hp-= damageVal;


            //きゃらごとの攻撃エフェクト
            if(selectObjFirst.GetComponent<CharaParam>().charaName== "MGC")
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

            var se = GameObject.FindGameObjectWithTag("SoundM").GetComponent<SoundManager>().se.gameObject;
            MyLib.MyPlayOneSound("SE/重いパンチ3", 1f, se);

            if (select.GetComponent<CharaParam>().hp<=0)
                select.GetComponent<TimeDestroy>().SetTime();
            
            //攻撃不可能ならターン終了
            //攻撃可能なら攻撃する　複数なら選択　攻撃後強制終了　ターン
            charaUI.CharaSelectOff();
            isCharaSelect = false;
            selectObjFirst.GetComponent<RotModule>().speed = 0;

 
            isMoveEnd = false;
            turnMgr.isPlayerTurn = false;

            //　

            StartCoroutine(MyLib.DelayCoroutine(1f, () =>
            {
                turnMgr.ChangeReTurn(true);
                //Camera.main.GetComponent<MoveCamera>().SetDown();

            }));


            selectObjFirst = null;

            return;
   
        }

    }

    //ボタンコンポーネントでの呼び出し
    public void SetMoveEreaGo()
    {
        if (!turnMgr.isPlayerTurn) return;
        if(selectObjFirst==null) return;
        selectObjFirst.GetComponent<CharaParam>().playerLengeGoAtk.SetActive(false);

        GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => { x.GetComponent<CharaParam>().isAtkTarget = false; });


        selectObjFirst.GetComponent<CharaParam>().playerLengeGoMove.SetActive(true);


    }

    public void SetAtkEreaGo()
    {
        if (!turnMgr.isPlayerTurn) return;
        if (selectObjFirst == null) return;


        selectObjFirst.GetComponent<CharaParam>().playerLengeGoMove.SetActive(false);

        selectObjFirst.GetComponent<CharaParam>().playerLengeGoAtk.SetActive(true);

    }

    public void TurnEndBottun()
    {
        Debug.Log("ボタンが押された");
        isCharaSelect = false;

        charaUI.CharaSelectOff();

        if (atkEreaGoSaveEnemy != null)
            atkEreaGoSaveEnemy.playerLengeGoAtk.SetActive(false);

        if (selectObjFirst != null)
        {
            if (selectObjFirst.GetComponent<RotModule>() != null)
                selectObjFirst.GetComponent<RotModule>().speed = 0;

            if (selectObjFirst.tag == "Chara")
                selectObjFirst.GetComponent<CharaParam>().playerLengeGoMove.SetActive(false);

            selectObjFirst.GetComponent<CharaParam>().playerLengeGoAtk.SetActive(false);
            GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => { x.GetComponent<CharaParam>().isAtkTarget = false; });

            //selectObjFirst.transform.position = savePos;
            selectObjFirst = null;
        }
        //Destroy(moveEreaGoSave);
        //Destroy(atkEreaGoSave);

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

        if(atkEreaGoSaveEnemy!=null)
            atkEreaGoSaveEnemy.playerLengeGoAtk.SetActive(false);

        if (selectObjFirst != null)
        {
            if(selectObjFirst.GetComponent<RotModule>()!=null)
            selectObjFirst.GetComponent<RotModule>().speed = 0;

            if(selectObjFirst.tag=="Chara")
            selectObjFirst.GetComponent<CharaParam>().playerLengeGoMove.SetActive(false);

            selectObjFirst.GetComponent<CharaParam>().playerLengeGoAtk.SetActive(false);
            GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => { x.GetComponent<CharaParam>().isAtkTarget = false; });

            selectObjFirst.transform.position = savePos;
            selectObjFirst = null;
        }
        //Destroy(moveEreaGoSave);
        //Destroy(atkEreaGoSave);

        isMoveEnd = false;
        GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach
            (x => x.GetComponent<CharaParam>().isAtkTarget = false);
    }


}
