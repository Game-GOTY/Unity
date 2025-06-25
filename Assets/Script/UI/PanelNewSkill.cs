using UnityEngine;
using UnityEngine.UI;

public class PanelNewSkill:MonoBehaviour
{
    [SerializeField] Image iconSkill;
    [SerializeField] Text nameSkill;
    [SerializeField] Text descriptionSkill;
    public void SetInforSkill(Sprite icon, string name, string des)
    {
        iconSkill.sprite = icon;
        nameSkill.text = name;
        descriptionSkill.text = des;
    }
}
