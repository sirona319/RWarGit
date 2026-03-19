using System.Linq;
using UnityEngine;
//using UnityEngine.InputSystem; ProjectSetting→Player→OtherSettings→ActiveInputHandlingをInputSystemPackageにする必要がある
public class MouseControlSelect : MonoBehaviour
{
    GameObject selectObj;

    const int LEFT = 0;
    const int RIGHT = 1;
    const int MIDDLE = 1;//ホイールクリック

    [SerializeField] float XLIMIT = 8.5f;
   // [SerializeField] float YLIMIT = 0f;
    [SerializeField] float ZLIMIT = 8.5f;


    CharaUI charaUI;

    bool isCharaSelect = false;
    Vector3 savePos;//キャンセル時に戻す座標を保存

    [SerializeField]GameObject moveEreaGo;
    bool isMoveEreaEnable = false;

    [SerializeField] GameObject atkEreaGo;
    bool isAtkEreaEnable = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charaUI = GameObject.FindGameObjectWithTag("CharaUI").GetComponent<CharaUI>();
    }


    void Update()
    {
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
        }
        else if(selectObj.tag == "Chara")
        {
            charaUI.CharaSelectOn();
            isCharaSelect = true;

        }

        //パラメーター　取得

        charaUI.charaText.text = selectObj.GetComponent<CharaParam>().charaName;
        charaUI.hpText.text= selectObj.GetComponent<CharaParam>().hp.ToString();
        charaUI.atkText.text = selectObj.GetComponent<CharaParam>().atkRank;
        charaUI.defText.text = selectObj.GetComponent<CharaParam>().defRank;
        charaUI.moveText.text = selectObj.GetComponent<CharaParam>().moveRank;
        
        

    }





    void CharaSelectState()
    {
        if (!Input.GetMouseButtonDown(LEFT)) return;


        var select = MyLib.DebugRayViewCameraPosZ();
        if (select == null) return;



        if (select.isStatic/*||selectObj.tag=="Untagged"*/)
        {
            select = null;
        }
        else if (select.tag == "MoveErea")
        {
            selectObj.transform.position = MyLib.DebugRayViewCameraPosXZ();

            //攻撃不可能ならターン終了
            //攻撃可能なら攻撃する　複数なら選択　
        }
        else if (select.tag == "Enemy")
        {
            //AtkEreaの範囲に入っている敵のみ選択可能にする
            if(!select.GetComponent<CharaParam>().isAtkTarget)
            {

                //GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => x.GetComponent<CharaParam>().isAtkTarget = false);
                //GameObject.FindGameObjectsWithTag("AtkErea").ToList().ForEach(x => x.SetActive(false));
                //isAtkEreaEnable = false;
                return;
            }
            //selectObj.transform.position = MyLib.DebugRayViewCameraPosXZ();
            Debug.Log(select.name+"攻撃した＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿");

            //揺らす長さ
            const float shakeLength = 0.15f;
            //揺らす力
            const float power = 0.3f;

            StartCoroutine(MyLib.DoShake(shakeLength, power, select.transform));
            //敵のHP　減少　死亡処理など
            //selectObj.GetComponent<CharaParam>().atkRank;

            var damageVal = selectObj.GetComponent<CharaParam>().GetAtkRandVal();

            select.GetComponent<CharaParam>().damageUI.DamegeView(damageVal);

            select.GetComponent<CharaParam>().hp-= damageVal;


            if(select.GetComponent<CharaParam>().hp<=0)
                select.GetComponent<TimeDestroy>().SetTime();
            //攻撃不可能ならターン終了
            //攻撃可能なら攻撃する　複数なら選択
            //
            charaUI.CharaSelectOff();
            //　
        }

        GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => x.GetComponent<CharaParam>().isAtkTarget = false);
        GameObject.FindGameObjectsWithTag("AtkErea").ToList().ForEach(x => x.SetActive(false));
        isAtkEreaEnable = false;
    }

    //ボタンコンポーネントでの呼び出し
    public void SetMoveEreaGo()
    {
        if (isMoveEreaEnable) return;

        Instantiate(
            moveEreaGo,
            new Vector3(selectObj.transform.position.x, -0.5f, selectObj.transform.position.z),
            Quaternion.identity);

        isMoveEreaEnable = true;
    }

    public void SetAtkEreaGo()
    {
        if (isAtkEreaEnable) return;

        Instantiate(
            atkEreaGo,
            new Vector3(selectObj.transform.position.x, -0.5f, selectObj.transform.position.z),
            Quaternion.identity);

        isAtkEreaEnable = true;
    }










    void RightClick()
    {
        if (!Input.GetMouseButtonDown(RIGHT)) return;

        isCharaSelect = false;

        charaUI.CharaSelectOff();

        if (selectObj != null)
        {
            selectObj.transform.position = savePos;
            selectObj = null;
        }
        isMoveEreaEnable = false;
        isAtkEreaEnable = false;

        GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => x.GetComponent<CharaParam>().isAtkTarget = false);
    }




    void SetlectObjMove()
    {
        if (!Input.GetMouseButton(LEFT)) return;
        if (selectObj == null) return;

        const float SPEED = 0.4f;

        //selectObj.transform.position=
        selectObj.transform.position += Camera.main.transform.right * Input.GetAxis("Mouse X") * SPEED +
            Camera.main.transform.up * Input.GetAxis("Mouse Y") * SPEED;

        selectObj.transform.position = MoveLimit(selectObj.transform.position);
    }

    Vector3 MoveLimit(Vector3 pos)
    {

        //Vector3 resultPos = pos;
        pos.x = Mathf.Clamp(pos.x, -XLIMIT, XLIMIT);
        pos.z = Mathf.Clamp(pos.z, -ZLIMIT, ZLIMIT);

        return pos;
    }
}
