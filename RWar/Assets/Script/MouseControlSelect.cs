using UnityEngine;
//using UnityEngine.InputSystem; ProjectSetting→Player→OtherSettings→ActiveInputHandlingをInputSystemPackageにする必要がある
public class MouseControlSelect : MonoBehaviour
{
    public GameObject selectObj;

    const int LEFT = 0;
    const int RIGHT = 1;

    [SerializeField] float XLIMIT = 8.5f;
   // [SerializeField] float YLIMIT = 0f;
    [SerializeField] float ZLIMIT = 8.5f;


    CharaUI charaUI;

    bool isCharaSelect = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charaUI = GameObject.FindGameObjectWithTag("CharaUI").GetComponent<CharaUI>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(RIGHT)) isCharaSelect = false;

        if (isCharaSelect) return;
        if (!Input.GetMouseButtonDown(LEFT)) return;
        

        selectObj = MyLib.DebugRayViewCameraPosZ();

        var createPos = MyLib.DebugRayViewCameraPosXZ();
        charaUI.CharaSelectOff();


        if (selectObj == null) return;
        

        if (selectObj.isStatic/*||selectObj.tag=="Untagged"*/)
        {
            GameObject.FindGameObjectWithTag("test").transform.position = createPos;//移動用

            selectObj = null;
        }
        else
        {
            if(selectObj.tag == "Enemy")
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
            charaUI.hpText.text= selectObj.GetComponent<CharaParam>().hp;
            charaUI.atkText.text = selectObj.GetComponent<CharaParam>().atkRank;
            charaUI.defText.text = selectObj.GetComponent<CharaParam>().defRank;
            charaUI.moveText.text = selectObj.GetComponent<CharaParam>().moveRank;
        }
        

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
