using UnityEngine;
public class UISafeAreaHandler : MonoBehaviour
{
    RectTransform panel;

    void Start()
    {
        panel = GetComponent<RectTransform>();
    }

    void Update()
    {
        Rect area = Screen.safeArea;

        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        if (Application.isEditor && Input.GetButton("Jump"))
        {
            if (Screen.height > Screen.width)
            {
                area = new Rect(0f, 0.038f, 1f, 0.913f);
            }
            else
            {
                area = new Rect(0.049f, 0.051f, 0.902f, 0.949f);
            }
            panel.anchorMin = area.position;
            panel.anchorMax = (area.position + area.size);
            return;
        }

        panel.anchorMin = area.position / screenSize;
        panel.anchorMax = (area.position + area.size) / screenSize;
    }
}