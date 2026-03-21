using DG.Tweening;
using UnityEngine;

public class TargetRot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    const float duration = 1f;
    //    transform.DORotate(Vector3.forward, duration, RotateMode.LocalAxisAdd);
    //}

    float targetZAngle = 180f; // 目標のZ角度
    float rotationSpeed = 300f; // 回転速度 (度/秒)



    [SerializeField] bool isUp = true;
    public void SetUp()
    {
        isUp = true;
        isDown = false;
    }
    public void SetDown()
    {
        isUp = false;
        isDown = true;
    }

    [SerializeField] bool isDown = false;
    void Update()
    {
        if(isDown)
        {
            RotBack();
        }
        else if(isUp)
        {
            Rot();
        }

    }

    void Rot()
    {
        // 1. 目標となる回転（Quaternion）を作成
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);

        // 2. 現在の回転から目標の回転へ、速度に合わせて徐々に回転させる
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    void RotBack()
    {
        // 1. 目標となる回転（Quaternion）を作成
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetZAngle);

        // 2. 現在の回転から目標の回転へ、速度に合わせて徐々に回転させる
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
