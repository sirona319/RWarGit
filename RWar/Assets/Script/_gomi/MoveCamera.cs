using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //[SerializeField] float speed = 3f;

    //[SerializeField]float targetY = 13f;

    //[SerializeField] bool isUp = true;
    //[SerializeField] bool isDown = false;

    //public void SetDown()
    //{
    //    isDown = true;
    //}

    //public void SetUp() 
    //{
    //    isUp = true;
    //}
    //void Update()
    //{
    //    if (!isUp && !isDown) return;

    //    UpMove();

    //    DownMove();
    //}

    //void UpMove()
    //{
    //    if(!isUp) return;

    //    var pos = transform.position;
    //    pos.y += speed * Time.deltaTime;

    //    transform.position = pos;

    //    if(transform.position.y >= targetY)
    //    {
    //        isUp = false;
    //    }
    //}

    //void DownMove()
    //{
    //    if (!isDown) return;

    //    var pos = transform.position;
    //    pos.y -= speed * Time.deltaTime;

    //    transform.position = pos;

    //    if (transform.position.y <= -targetY)
    //    {
    //        isDown = false;
    //    }
    //}

}
//[SerializeField] protected List<Transform> moveTrans;
//[SerializeField] List<float> endLength;
//[SerializeField] float speed = 1f;

//[SerializeField] bool isLoop = false;

//protected int targetNo = 0;

//void Update()
//{

//    transform.position += moveTrans[targetNo].position * speed * Time.deltaTime;

//    float len = Vector3.Distance(transform.position, moveTrans[targetNo].position);
//    if (len < endLength[targetNo])
//    {

//        targetNo++;
//        if (targetNo > moveTrans.Count - 1)
//        {

//            if (isLoop)
//                targetNo = 0;
//            else
//                targetNo--;
//        }

//    }

//}
//}
