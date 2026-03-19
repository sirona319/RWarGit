using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textUI;

    void Start()
    {
        textUI.DOFade(0, 0);

    }

    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }


    public void DamegeView(int damage)
    {
        textUI.text = "-" + damage.ToString();

        textUI.DOFade(1, 0f);
        textUI.DOFade(0, 1f);

    }
}
