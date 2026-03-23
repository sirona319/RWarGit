using UnityEngine;

public class TitleStart : MonoBehaviour
{

    [SerializeField] string BottuonNameOne;
    [SerializeField] string BottuonNameTwo;
    [SerializeField] string BottuonNameThree;


    //[SerializeField] EnumSceneName.SceneNameType sceneNameOne;

    bool isSelect = false;

    //float fadeTime = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSelect = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeScene>().fadeWaitTime -= Time.deltaTime;
        if (GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeScene>().fadeWaitTime > 0) return;

        const int LEFT = 0;
        if (!Input.GetMouseButtonDown(LEFT)) return;
        

        var selectObj = MyLib.DebugRayViewCameraPosZ();

        if(selectObj==null) return;

        if (selectObj.gameObject.name == BottuonNameOne)
        {
            //遷移
            SelectStart();
        }

        if (selectObj.gameObject.name == BottuonNameTwo)
        {
            //遷移
            SelectStart2();
        }

        if (selectObj.gameObject.name == BottuonNameThree)
        {
            //遷移
            SelectStart3();
        }

    }

    //[SerializeField] FadeScene fadeScene;
    public void SelectStart()
    {
        if(isSelect) return;

        const float sceneChangeTime = 2f;

        GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeScene>().
            SceneFade(BottuonNameOne.ToString(), 0f, sceneChangeTime);

        var se = GameObject.FindGameObjectWithTag("SoundM").GetComponent<SoundManager>().se.gameObject;
        var volume = .3f;
        MyLib.MyPlayOneSound("SE/決定ボタンを押す12", volume, se);

        isSelect = true;
    }

    public void SelectStart2()
    {
        if (isSelect) return;

        const float sceneChangeTime = 2f;

        GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeScene>().
            SceneFade(BottuonNameTwo.ToString(), 0f, sceneChangeTime);

        var se = GameObject.FindGameObjectWithTag("SoundM").GetComponent<SoundManager>().se.gameObject;
        var volume = .3f;
        MyLib.MyPlayOneSound("SE/決定ボタンを押す12", volume, se);

        isSelect = true;
    }

    public void SelectStart3()
    {
        if (isSelect) return;

        const float sceneChangeTime = 2f;

        GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeScene>().
        SceneFade(BottuonNameThree.ToString(), 0f, sceneChangeTime);

        var se = GameObject.FindGameObjectWithTag("SoundM").GetComponent<SoundManager>().se.gameObject;
        var volume = .3f;
        MyLib.MyPlayOneSound("SE/決定ボタンを押す12", volume, se);

        isSelect = true;
    }
}
