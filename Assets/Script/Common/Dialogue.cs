using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private float heightOffset; // Default height above character
    [Range(0f, 1f)]
    [SerializeField] private float backgroundOpacity = 0.8f; // Background opacity

    private Canvas canvas;
    private Camera mainCamera;
    private bool isDisplaying;
    private int timerId = -1; // Track our timer

    void LateUpdate()
    {
        // Keep upright regardless of parent
        transform.rotation = Quaternion.identity;

        // Optional: Always face camera
        transform.forward = Camera.main.transform.forward;
    }

    private void Awake()
    {
        // Set up Canvas
        canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.sortingOrder = 10; // Render above other objects

            // Scale canvas for world space
            RectTransform rect = canvas.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(2f, 0.5f); // Width and height in world units
            canvas.transform.localScale = Vector3.one * 0.01f; // Small scale
        }

        // Ensure UI elements are set up
        if (dialogueText == null || backgroundImage == null)
        {
            SetupUIElements();
        }

        // Get main camera
        mainCamera = Camera.main;

        // Set initial background opacity
        UpdateBackgroundOpacity();

        // Hide dialogue initially
        HideDialogue();
    }

    private float displayDuration(string text)
    {
        const float rate = 3f;
        if (string.IsNullOrEmpty(text))
        {
            return 0f;
        }
        int wordCount = (text.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)).Length;

        return (wordCount/rate) + 1;
    }

    private float displayLineSetOff(string text){
        int wordCount = (text.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)).Length;
        float setOff = wordCount / 4;
        return setOff > 1.0f ? setOff : 1;
    }

    private void SetupUIElements()
    {
        // Create background Image if not assigned
        if (backgroundImage == null)
        {
            GameObject bgObj = new GameObject("Background");
            bgObj.transform.SetParent(canvas.transform, false);
            backgroundImage = bgObj.AddComponent<Image>();
            backgroundImage.color = new Color(0, 0, 0, backgroundOpacity);
            RectTransform bgRect = bgObj.GetComponent<RectTransform>();
            bgRect.sizeDelta = new Vector2(200, 50); // Pixel size
        }

        // Create TextMeshProUGUI if not assigned
        if (dialogueText == null)
        {
            GameObject textObj = new GameObject("DialogueText");
            textObj.transform.SetParent(canvas.transform, false);
            dialogueText = textObj.AddComponent<TextMeshProUGUI>();
            dialogueText.alignment = TextAlignmentOptions.Center;
            dialogueText.fontSize = 24;
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.sizeDelta = new Vector2(180, 40);
        }
    }

    private void Update()
    {
        // Update position to stay above parent character
        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            Vector3 targetPosition = parentTransform.position + Vector3.up * this.heightOffset;
            transform.position = targetPosition;

            // Face camera
            transform.rotation = mainCamera.transform.rotation;
        }
    }

    public void ShowDialogue(string text, float duration = -1)
    {
        // Clear any existing timer
        if (timerId != -1)
        {
            TimerUtility.ClearTimeout(timerId);
            timerId = -1;
        }

        // Use default duration if none specified
        if (duration < 0)
        {
            duration = displayDuration(text);
        }

        // Update heightOffset
        this.heightOffset = displayLineSetOff(text);

        dialogueText.text = text;
        isDisplaying = true;

        dialogueText.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);

        // Set timer to hide dialogue
        if (duration > 0)
        {
            timerId = TimerUtility.SetTimeout(duration, () => 
            {
                HideDialogue();
                timerId = -1;
            });
        }
    }

    public void HideDialogue()
    {
        // Clear timer if hiding manually
        if (timerId != -1)
        {
            TimerUtility.ClearTimeout(timerId);
            timerId = -1;
        }

        isDisplaying = false;
        dialogueText.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
    }

    private void UpdateBackgroundOpacity()
    {
        if (backgroundImage != null)
        {
            Color bgColor = backgroundImage.color;
            bgColor.a = backgroundOpacity;
            backgroundImage.color = bgColor;
        }
    }

    public void SetBackgroundOpacity(float opacity)
    {
        backgroundOpacity = Mathf.Clamp01(opacity);
        UpdateBackgroundOpacity();
    }

    private void OnDestroy()
    {
        // Clean up any active timers when destroyed
        if (timerId != -1)
        {
            TimerUtility.ClearTimeout(timerId);
        }
    }
}