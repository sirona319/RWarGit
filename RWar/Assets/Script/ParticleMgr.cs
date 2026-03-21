using Unity.VisualScripting;
using UnityEngine;

public class ParticleMgr : MonoBehaviour
{
    public GameObject deadPt;

    public GameObject atkPt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deadPt = (GameObject)Resources.Load("prefab/Particle/CFXR Magic Poof");

        atkPt = (GameObject)Resources.Load("prefab/Particle/CFXR4 Sword Hit PLAIN (Cross)");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
