using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameField_scr : MonoBehaviour
{
    public static GameField_scr instance;

    public List<GameObject> prefabs = new List<GameObject>();
    public GameObject winScreen;

    int[,] field;
    [HideInInspector]
    public Block_scr chosenBlock;

    private void Awake()
    {
        if (instance == null) instance = this; else Destroy(gameObject);
    }
    void Start()
    {
        InitField(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            InitField(true);
        if (Input.GetKeyDown(KeyCode.F2))
            InitField(false);
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    /// <summary>
    /// �������������� ���� � ������ ������� ����� ������� ����
    /// </summary>
    /// <param name="original">������������� ������������� �������� ���� ��� ����-����������</param>
    public void InitField(bool original)
    {
        
        winScreen.SetActive(false);
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
        if (original)
            field = new int[5, 5] { 
            { 2, 3, 1, 2, 3 }, 
            { 0, -1, 0, -1, 0 }, 
            { 1, 1, 3, 2, 3 }, 
            { 0, -1, 0, -1, 0 }, 
            { 2, 2, 3, 1, 1 } };
        else
        {
            restart: 
            field = new int[5, 5];
            int[] blocksLeft = new int[] { 5, 5, 5 }; 
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (i % 2 == 0)
                    {
                        int blockNum = WeightedPick(blocksLeft[0], blocksLeft[1], blocksLeft[2]);
                        blocksLeft[blockNum - 1]--;
                        field[i, j] = blockNum;
                    } 
                    else
                        if (j % 2 == 1)
                            field[i, j] = -1;
            if (CheckValidity())
                goto restart;
        }
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
            {
                if (field[i, j] == -1)
                    continue;
                GameObject curPref = Instantiate(prefabs[field[i, j]], transform);
                curPref.GetComponent<RectTransform>().anchoredPosition = new Vector3(50 + 100 * i, - 50 - 100 * j, 0);
                if (field[i, j] == 0)
                    continue;
                Block_scr curBlockScr = curPref.GetComponent<Block_scr>();
                curBlockScr.x = i;
                curBlockScr.y = j;
            }
    }
    /// <summary>
    /// ���������� ��������� � ������ ������ ���� � ������� <see cref="field">field</see>[<paramref name="xTo"/>, <paramref name="yTo"/>]
    /// </summary>
    /// <param name="xTo">������ ������ ������� Field</param>
    /// <param name="yTo">������ ������ ������� Field</param>
    /// <returns>���� �� ������ ������ - ���������� True, ����� False</returns>
    public bool MoveChosenBlock(int xTo, int yTo)
    {
        if ((xTo < 0) || (xTo > 4) || (yTo < 0) || (yTo > 4) || field[xTo, yTo] != -1)
            return false;
        field[xTo, yTo] = field[chosenBlock.x, chosenBlock.y];
        field[chosenBlock.x, chosenBlock.y] = -1;
        chosenBlock.x = xTo;
        chosenBlock.y = yTo;
        if (CheckValidity())
            winScreen.SetActive(true);
        return true;
    }
    /// <summary>
    /// �������� �������� ������� �����
    /// </summary>
    /// <returns>���� ���� ����� ����� - ���������� True, ����� False</returns>
    bool CheckValidity()
    {
        for (int i = 0; i < 5; i++)
            if (field[0, i] != 1 || field[2, i] != 2 || field[4, i] != 3)
                return false;
        return true;
    }
    /// <summary>
    /// ���������� ����� ����� �������
    /// </summary>
    /// <param name="yellow">���������� ����� ������ ��� ������</param>
    /// <param name="orange">���������� ��������� ������ ��� ������</param>
    /// <param name="red">���������� ������� ������ ��� ������</param>
    /// <returns>���������� int ����� �������</returns>
    int WeightedPick(int yellow, int orange, int red)
    {
        int sum = yellow + orange + red;
        if (sum == 0)
            return -1;
        int pick = Random.Range(0, sum) + 1;
        if (pick <= yellow)
            return 1;
        if (pick <= (yellow + orange))
            return 2;
        return 3;
    }
}
