using UnityEngine;
using UnityEngine.InputSystem;

public class CameraCursorMove : MonoBehaviour
{
    public Camera playerCamera;

    // Player pos -> Cursor pos Vector will be divided by this number, to calculate a new camera position
    public float fractionOfCursorPlayerVector;

    public Canvas crosshairCanvas;
    public RectTransform crosshairImage;

    public Vector2 newCursorScreenPos;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        // Crosshair UI
        if (crosshairImage && crosshairCanvas)
        {
            var mouseScreenPos = Mouse.current.position.ReadValue();
            newCursorScreenPos = mouseScreenPos;
            newCursorScreenPos = LimitToScreenResolution(newCursorScreenPos);
            var newCursorWorldPos = GetMouseUIPosition(newCursorScreenPos);
            crosshairImage.anchoredPosition = newCursorWorldPos;
        }
    }

    //Move Camera Based on Cursor Position - Microsoft Copilot (Inspired by Enter the Gungeon)
    private void LateUpdate()
    {
        var playerWorldPos = transform.position;

        var playerDepth = playerCamera.WorldToScreenPoint(playerWorldPos).z;
        var mouseWorldPos = playerCamera.ScreenToWorldPoint(
            new Vector3(newCursorScreenPos.x, newCursorScreenPos.y, playerDepth)
        );

        var playerToMouse = mouseWorldPos - playerWorldPos;

        var fraction = 1f / fractionOfCursorPlayerVector;
        var offset = playerToMouse * fraction;

        var newCameraWorldPos = playerWorldPos + offset;
        newCameraWorldPos.z = -1f;

        playerCamera.transform.position = newCameraWorldPos;
    }

    private Vector2 GetMouseUIPosition(Vector2 mouseScreenPos)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            crosshairCanvas.transform as RectTransform,
            mouseScreenPos,
            crosshairCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : crosshairCanvas.worldCamera,
            out localPos
        );
        return localPos;
    }

    private Vector2 LimitToScreenResolution(Vector2 cursorScreenPos)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (cursorScreenPos.x > screenWidth)
            cursorScreenPos.x = screenWidth;
        else if (cursorScreenPos.x < 0)
            cursorScreenPos.x = 0;

        if (cursorScreenPos.y > screenHeight)
            cursorScreenPos.y = screenHeight;
        else if (cursorScreenPos.y < 0)
            cursorScreenPos.y = 0;

        return cursorScreenPos;
    }
}