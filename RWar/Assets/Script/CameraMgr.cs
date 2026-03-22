using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera subCamera1;
    [SerializeField] Camera subCamera2;
    [SerializeField] Camera subCamera3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mainCamera.tag= "MainCamera";
            mainCamera.enabled = true;

            subCamera1.tag = "Untagged";
            subCamera1.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            subCamera1.tag = "MainCamera";
            subCamera1.enabled = true;

            mainCamera.tag = "Untagged";
            mainCamera.enabled = false;

        }
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    mainCamera.tag = "MainCamera";
        //    mainCamera.enabled = true;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    mainCamera.tag = "MainCamera";
        //    mainCamera.enabled = true;
        //}
    }
}
