using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor_scr : MonoBehaviour
{
    public static Cursor_scr instance;

    private void Awake()
    {
        if (instance == null) instance = this; else Destroy(gameObject);
    }

    /// <summary>
    /// Меняет цвет курсора и перемещает к выбранному блоку
    /// </summary>
    /// <param name="block">Трансформ к которому перемещается курсор</param>
    /// <param name="blockColor">Новый цвет курсора</param>
    public void UpdateCursorPosAndColor(Transform block, Color blockColor)
    {
        transform.SetParent(block);
        GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        foreach (Image img in gameObject.GetComponentsInChildren<Image>())
        {
            img.color = blockColor - new Color(0.3f, 0.3f, 0.3f, 0);
        }
    }
}
