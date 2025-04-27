using UnityEngine;
using UnityEngine.UI;

public class StartButton : Main
{
    [SerializeField] protected GameMenu menu;
    [SerializeField] protected Button button;

    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(OnButtonClick);
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadMenu();
        LoadButton();
    }
    protected virtual void LoadButton()
    {
        if (button != null) return;
        button = transform.GetComponent<Button>();
    }
    protected virtual void LoadMenu()
    {
        if (menu != null) return;
        menu = GameObject.Find("Scroll View").GetComponent<GameMenu>();
    }
    protected virtual void OnButtonClick()
    {
        menu.transform.gameObject.SetActive(false);
    }
}
