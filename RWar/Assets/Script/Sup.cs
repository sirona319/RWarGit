using System.Linq;
using UnityEngine;

public class Sup : MonoBehaviour
{

    public string supName = "none";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SupSkill()
    {
        if(supName=="none")
        {
            Debug.Log("none");
        }
        else if(supName=="heal")
        {
            Debug.Log("heal");
            GameObject.FindGameObjectsWithTag("Chara").ToList().ForEach(g => g.GetComponent<CharaParam>().hp+=5);

        }
         else if(supName=="buff")
        {
            Debug.Log("buff");

        }
         else if(supName=="debuff")
        {
            Debug.Log("debuff");

        }
         else if(supName=="damage")
        {
            Debug.Log("damage");

        }
    }
}
