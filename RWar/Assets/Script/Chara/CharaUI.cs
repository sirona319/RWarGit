using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
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

    public void CharaSelectOn()
    {
        //enemyName.gameObject.SetActive(false);
        paramName.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.7f, 1);
        param.gameObject.SetActive(true);
        paramName.gameObject.SetActive(true);
        command.gameObject.SetActive(true);
        commandName.gameObject.SetActive(true);

        GameObject.FindGameObjectsWithTag("MoveErea").ToList().ForEach(x => x.SetActive(false));

        GameObject.FindGameObjectsWithTag("AtkErea").ToList().ForEach(x => x.SetActive(false));
    }

    public void CharaSelectOff()
    {
        param.gameObject.SetActive(false);
        paramName.gameObject.SetActive(false);
        command.gameObject.SetActive(false);
        commandName.gameObject.SetActive(false);


        GameObject.FindGameObjectsWithTag("MoveErea").ToList().ForEach(x => x.SetActive(false));

        GameObject.FindGameObjectsWithTag("AtkErea").ToList().ForEach(x => x.SetActive(false));
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
