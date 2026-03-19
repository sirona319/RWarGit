using UnityEngine;

public class InCol : MonoBehaviour
{
    //public Collider col;

    string tagName = "Enemy";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == tagName)
        {
            other.GetComponent<CharaParam>().isAtkTarget = true;
            Debug.Log(other.gameObject.tag+ "TRUE TARGET");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == tagName)
        {
            other.GetComponent<CharaParam>().isAtkTarget = false;
            Debug.Log(other.gameObject.tag + "FALSE TARGET");
        }
    }
}
