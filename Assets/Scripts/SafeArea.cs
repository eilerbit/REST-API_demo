using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private Canvas myCanvas;
    private RectTransform panelSafeArea;
        

    private void Awake()
    {
        myCanvas = transform.parent.GetComponent<Canvas>();
        panelSafeArea = GetComponent<RectTransform>();
        
        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= myCanvas.pixelRect.width;
        anchorMin.y /= myCanvas.pixelRect.height;

        anchorMax.x /= myCanvas.pixelRect.width;
        anchorMax.y /= myCanvas.pixelRect.height;


        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;
    }

}
