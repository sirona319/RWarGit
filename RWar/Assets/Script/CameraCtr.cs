using DG.Tweening;
using UnityEngine;

public class CameraCtr : MonoBehaviour
{
    public bool isEventCamera = false;
    //Transform eventCameraTarget;
    Vector3 eventTargetPos = Vector3.zero;
    float cameraDuration = 1f;

    public Transform cameraTarget; // Z -10
    [SerializeField] float cameraSpd = 0.08f;

    [SerializeField] float cameraTargetLen = 13f;

    //[SerializeField] float cameraSize = 5f;
    void Start()
    {
        if (cameraTarget == null)
        {
            //cameraTarget= GameObject.FindGameObjectWithTag(TagName.Player).transform.Find("CameraTarget").transform;
            //var pos = transform.position;
            //pos.z = -10f;
            //transform.position = pos;
            return;
        }


        var t = transform.position;
        t.x = cameraTarget.position.x;
        t.z = cameraTarget.position.z;
        transform.position = t;

        //Camera.main.orthographicSize = cameraSize;

    }

    void Update()
    {
        //CameraEventUpdate();
    }

    private void FixedUpdate()
    {
        var eDir = cameraTarget.transform.position - transform.position;

        var elookAtRotation = Quaternion.LookRotation(eDir, Vector3.up);

        const float INTERPOLANT = 5f;
        transform.rotation = Quaternion.Lerp(transform.rotation, elookAtRotation, Time.deltaTime * INTERPOLANT);

        //if (Vector3.Distance(cameraTarget.position, transform.position) < cameraTargetLen)
        //    return;

        //transform.position = Vector3.Lerp(transform.position, cameraTarget.transform.position, cameraSpd * Time.deltaTime);

        //CameraTarget2DUpdate();

    }

    void CameraTarget2DUpdate()
    {
        //if (isEventCamera) return;

        if (cameraTarget == null) return;

        transform.position = Vector3.Lerp(transform.position, cameraTarget.transform.position, cameraSpd * Time.deltaTime);

    }


    void CameraEventUpdate()
    {
        if (!isEventCamera) return;

        const float cameraTargetLen = 1f;
        if (Vector3.Distance(eventTargetPos, transform.position) < cameraTargetLen)
            return;

        //tween = 
        transform.DOMove(eventTargetPos, cameraDuration).SetEase(Ease.OutSine);

    }

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="targetPos"></param>位置
    ///// <param name="dur"></param>速度
    //public void CameraEventTrigger(Vector3 targetPos, float dur)
    //{
    //    isEventCamera = true;
    //    eventTargetPos = targetPos;
    //    cameraDuration = dur;
    //}

    //public void CameraEventTriggerOff()
    //{
    //    isEventCamera = false;
    //}

    //public void ChangePlayerPos()
    //{
    //    transform.position = cameraTarget.transform.position;
    //}
}
