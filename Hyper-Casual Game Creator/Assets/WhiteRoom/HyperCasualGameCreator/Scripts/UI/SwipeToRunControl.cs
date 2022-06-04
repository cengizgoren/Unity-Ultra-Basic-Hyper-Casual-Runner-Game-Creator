using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeToRunControl : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public bool isClick;

    private void Update()
    {
        if (!isClick) return;
        if (!(Mathf.Abs(InputManager.singleton.TouchInput.x) > .1f)) return;
        GameManager.singleton.isGameStart = true;
        transform.parent.gameObject.SetActive(false);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClick = false;
    }
}
