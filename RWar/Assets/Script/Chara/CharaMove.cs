using UnityEngine;

public class CharaMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("当たった");
        }

        if (collision.gameObject.tag == "Chara")
        {
            Debug.Log("当たった");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("当たった");
        }
        if (other.gameObject.tag == "Chara")
        {
            Debug.Log("当たった");
        }
    }
}
