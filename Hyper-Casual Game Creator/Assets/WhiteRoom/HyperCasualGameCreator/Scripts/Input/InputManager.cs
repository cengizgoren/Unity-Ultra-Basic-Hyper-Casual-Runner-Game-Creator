using UnityEngine;

public class InputManager : Manager
{
    public static InputManager singleton;
    
    public Vector2 TouchInput;
    public float TouchInputMagnitude = 1f;

    private Vector2 firstTouchPos;

    private void Awake()
    {
        singleton = this;
    }

    private void Update()
    {
        if (Input.touchCount < 1) return;
        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            firstTouchPos = touch.position;
        }
        
        else if (touch.phase == TouchPhase.Moved)
        {
            TouchInput = (firstTouchPos - touch.position) * -.1f;
            TouchInput = Vector2.ClampMagnitude(TouchInput, TouchInputMagnitude);
        }
        else if (touch.phase == TouchPhase.Stationary)
        {
            TouchInput = Vector2.zero;
            firstTouchPos = touch.position;
        }

        else if (touch.phase == TouchPhase.Ended)
        {
            TouchInput = Vector2.zero;
            firstTouchPos = touch.position;
        }
    }
}
