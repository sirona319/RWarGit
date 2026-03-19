using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CharaUI : MonoBehaviour
{
    //statusは状態

    //攻撃力や防御力などのパラメータ
    public Transform param;
    public Transform paramName;

    public TMP_Text charaText;

    public TMP_Text hpText;
    public TMP_Text atkText;
    public TMP_Text defText;
    public TMP_Text moveText;

    public Transform command;
    public Transform commandName;


    public Transform enemyName;

    ////HP
    //public Image[] lifeImage;



    ////DEAD
    //public Transform DeadSprite;

    ////移動させる画像
    //public Image eyeImageUp;
    //public Image eyeImageDown;
    ////

    //public Transform closeEyeImageUp1;
    //public Transform closeEyeImageDown1;
    //public Transform closeEyeImageUp2;
    //public Transform closeEyeImageDown2;
    //public Transform closeEyeImageUp0;
    //public Transform closeEyeImageDown0;

    //public Transform EyeObj;
    //public TextMeshProUGUI reviveText;

    public void CharaSelectOn()
    {
        //enemyName.gameObject.SetActive(false);
        paramName.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.7f, 1);
        param.gameObject.SetActive(true);
        paramName.gameObject.SetActive(true);
        command.gameObject.SetActive(true);
        commandName.gameObject.SetActive(true);
    }

    public void CharaSelectOff()
    {
        param.gameObject.SetActive(false);
        paramName.gameObject.SetActive(false);
        command.gameObject.SetActive(false);
        commandName.gameObject.SetActive(false);

        //enemyName.gameObject.SetActive(false);
    }

    public void EnemySelectOn()
    {
        paramName.GetComponent<Image>().color= new Color(0.7f, 0.5f, 0.5f, 1);
        param.gameObject.SetActive(true);
        paramName.gameObject.SetActive(true);
        command.gameObject.SetActive(false);
        commandName.gameObject.SetActive(false);

        //enemyName.gameObject.SetActive(true);
    }
}
