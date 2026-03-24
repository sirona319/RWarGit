using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class Sup : MonoBehaviour
{
    const float turnChangeTime = 0.8f;
    public string supName = "none";

    ParticleMgr particleMgr;
    GameObject se;

    public TextMeshProUGUI InfoText;

    private void Start()
    {
        particleMgr = GameObject.FindGameObjectWithTag("ParticleMgr").GetComponent<ParticleMgr>();
        se = GameObject.FindGameObjectWithTag("SoundM").GetComponent<SoundManager>().se.gameObject;
    }
    public void SupSkill(GameObject go)
    {
        InfoText.enabled = false;

        if (supName=="none")
        {
            Debug.Log("none");
        }
        else if(supName=="Heal")
        {

            StartCoroutine(MyLib.DelayCoroutine(turnChangeTime, () =>
            {
                var val = 5;
                var charas = GameObject.FindGameObjectsWithTag("Chara").ToList();
                charas.ForEach(g => g.GetComponent<CharaParam>().hp += val);
                charas.ForEach(g => g.GetComponent<CharaParam>().doChangeHpUI.HealView(val));

                MyLib.MyPlayOneSound("SE/魔法反射", 1f, se);

                foreach (var c in charas)
                {
                    Instantiate(particleMgr.healPt,
                    c.transform.position,
                    Quaternion.identity);
                }


            }));



        }
        else if(supName== "Buff")
        {
            Debug.Log("Buff");
            StartCoroutine(MyLib.DelayCoroutine(turnChangeTime, () =>
            {

                var chs = GameObject.FindGameObjectsWithTag("Chara");

                var randHit = Random.Range(0, chs.Length);
                var atkRank = chs[randHit].GetComponent<CharaParam>().atkRank;

                bool succes = z_Lib.RanKUp(ref chs[randHit].GetComponent<CharaParam>().atkRank);
                if (!succes)
                {
                    succes= z_Lib.RanKUp(ref chs[randHit].GetComponent<CharaParam>().defRank);

                    if(!succes)
                    {
                        InfoText.enabled = true;
                        InfoText.text="Buff失敗";
                        return;

                    }
                }


                MyLib.MyPlayOneSound("SE/魔法反射", 1f, se);


                Instantiate(particleMgr.buffPt,
                chs[randHit].transform.position,
                Quaternion.identity);


            }));


        }
         else if(supName=="Debuff")
        {

            StartCoroutine(MyLib.DelayCoroutine(turnChangeTime, () =>
            {
                var ens = GameObject.FindGameObjectsWithTag("Enemy");
                var randHit = Random.Range(0, ens.Length);

                bool succes = z_Lib.RanKDown(ref ens[randHit].GetComponent<CharaParam>().atkRank);
                if(!succes)
                {
                    succes = z_Lib.RanKDown(ref ens[randHit].GetComponent<CharaParam>().defRank);

                    if (!succes)
                    {
                        InfoText.enabled = true;
                        InfoText.text = "DeBuff失敗";
                        return;

                    }
                }


                MyLib.MyPlayOneSound("SE/スローモーション", 1f, se);


                Instantiate(particleMgr.debuffPt,
                new Vector3(ens[randHit].transform.position.x, ens[randHit].transform.position.y+1f, ens[randHit].transform.position.z),
                Quaternion.identity);

                Debug.Log("Debuff");


            }));
        }
         else if(supName=="Damage")
        {

            StartCoroutine(MyLib.DelayCoroutine(turnChangeTime, () =>
            {
                var val = 5;
                var ens = GameObject.FindGameObjectsWithTag("Enemy").ToList();
                ens.ForEach(g => g.GetComponent<CharaParam>().hp -= val);
                ens.ForEach(g => g.GetComponent<CharaParam>().doChangeHpUI.DamegeView(val));

                MyLib.MyPlayOneSound("SE/スローモーション", 1f, se);

                foreach (var e in ens)
                {
                    Instantiate(particleMgr.downPt,
                        new Vector3(e.transform.position.x, e.transform.position.y + 1f, e.transform.position.z),
                        Quaternion.identity);
                }

            }));

        }


    }
}
