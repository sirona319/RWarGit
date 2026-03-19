using UnityEngine;

public class MoveLimit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = MoveLimitY(transform.position);
    }

    Vector3 MoveLimitY(Vector3 pos)
    {

        //Vector3 resultPos = pos;
        //pos.x = Mathf.Clamp(pos.x, -XLIMIT, XLIMIT);
        pos.y = Mathf.Clamp(pos.y, -0.0001f, 0);

        return pos;
    }
}
