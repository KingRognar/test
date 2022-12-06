using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block_scr : MonoBehaviour
{
    [HideInInspector]
    public int x, y;

    public bool isChosen = false;

    private void Update()
    {
        if (!isChosen)
            return;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            if (GameField_scr.instance.MoveChosenBlock(x, y - 1))
                UpdatePosition();
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            if (GameField_scr.instance.MoveChosenBlock(x - 1, y))
                UpdatePosition();
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            if (GameField_scr.instance.MoveChosenBlock(x, y + 1))
                UpdatePosition();
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            if (GameField_scr.instance.MoveChosenBlock(x + 1, y))
                UpdatePosition();
    }

    /// <summary>
    /// Функция, выполняющаяся при нажатии на блок.
    /// В данном случае выбирает этот блок как текущий и обновляет позицию курсора
    /// </summary>
    public void OnButtonClick()
    {
        if (GameField_scr.instance.chosenBlock != null)
            GameField_scr.instance.chosenBlock.isChosen = false;
        GameField_scr.instance.chosenBlock = this;
        isChosen = true;
        Cursor_scr.instance.UpdateCursorPosAndColor(transform, gameObject.GetComponent<Image>().color);
    }
    /// <summary>
    /// Обновляет позицию спрайта блока по значениям <see cref="x">x</see> и <see cref="y">y</see>
    /// </summary>
    void UpdatePosition()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(50 + 100 * x, -50 - 100 * y, 0);
    }
}
