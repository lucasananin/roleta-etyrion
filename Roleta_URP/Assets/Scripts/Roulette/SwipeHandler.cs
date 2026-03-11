using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeHandler : MonoBehaviour
{
    private Vector2 startPos;
    private float startTime;

    public float minSwipeDistance = 50f; // Minimum pixels to count as swipe

    void Update()
    {
        // TOUCH INPUT
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                startPos = touch.position;
                startTime = Time.time;
            }

            if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                DetectSwipe(touch.position);
            }
        }

        // MOUSE INPUT (for testing in editor)
        //if (Input.GetMouseButtonDown(0))
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startPos = Input.mousePosition;
            startTime = Time.time;
        }

        //if (Input.GetMouseButtonUp(0))
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            DetectSwipe(Input.mousePosition);
        }
    }

    void DetectSwipe(Vector2 endPos)
    {
        Vector2 delta = endPos - startPos;

        // Only horizontal swipe
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && Mathf.Abs(delta.x) > minSwipeDistance)
        {
            float swipeLength = Mathf.Abs(delta.x);
            float swipeTime = Time.time - startTime;

            //string direction = delta.x > 0 ? "Right" : "Left";

            //Debug.Log($"Horizontal Swipe {direction}");
            //Debug.Log($"Swipe Length: {swipeLength} pixels");
            //Debug.Log($"Swipe Time: {swipeTime} seconds");

            float swipeSpeed = swipeLength / swipeTime;
            //Debug.Log($"// swipe speed = {(int)swipeSpeed}");

            var _wheel = FindFirstObjectByType<RouletteWheel>();
            _wheel.Spin((int)swipeSpeed / 2, 0f);
        }
    }
}