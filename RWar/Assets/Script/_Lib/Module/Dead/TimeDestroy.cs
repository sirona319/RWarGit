using System.Collections;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    public float deadTime = 1f;

    [SerializeField]bool isStart = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!isStart) return;
        StartCoroutine(DestroyTimer(deadTime));
    }

    public void SetTime()
    {
        StartCoroutine(DestroyTimer(deadTime));
    }

    IEnumerator DestroyTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("TimeDestroy:" + gameObject.name);

        Destroy(gameObject);
    }
}
