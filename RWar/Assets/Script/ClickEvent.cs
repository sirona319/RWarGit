using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (textMeshPro == null)
            var textMeshPro = this.GetComponent<TextMeshProUGUI>();


        var clickEvent = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        // var enterEvent = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        // //var exitEvent = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };

        clickEvent.callback.AddListener((eventData) => { Debug.Log("test"); });
        // enterEvent.callback.AddListener((eventData) => { PointerEnter(); });
        // exitEvent.callback.AddListener((eventData) => { PointerExit();});

        var trigger = GetComponent<EventTrigger>();
        trigger.triggers.Add(clickEvent);
    }
}
