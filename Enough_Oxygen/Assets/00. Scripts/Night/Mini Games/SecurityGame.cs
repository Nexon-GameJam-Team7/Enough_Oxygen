// System
using System.Collections.Generic;

// TMPro
using TMPro;

// Unity
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class SecurityGame : MonoBehaviour
{
    [Header("Word list of mini game")]
    [SerializeField] private List<string> wordList;
    private string currentWord;

    [SerializeField] private Transform buttonParent;
    [SerializeField] private Transform barParent;
    [SerializeField] private Transform conjectureParent;
    private UIBase uiBase;

    private int wordListIdx;

    private List<char> letterList;
    private string conjectureString;

    private int wordLength;

    private void Start()
    {
        letterList = new List<char>();
        conjectureString = "";
        wordListIdx = 0;

        uiBase = GetComponent<UIBase>();

        InitScreen(wordListIdx);
    }

    private void InitScreen(int _idx)
    {
        Clear();

        currentWord = wordList[wordListIdx];
        wordLength = wordList[_idx].Length;

        SplitWord(wordList[_idx]);
        ShuffleWord(letterList);
        GenerateButtons(letterList);

        UpdateGrid();
    }

    private void UpdateGrid()
    {
        UpdateGridSize buttonParentGrid = buttonParent.GetComponent<UpdateGridSize>();
        int offset = buttonParentGrid.ComputeOffset();
        Debug.Log(offset);

        buttonParentGrid.UpdateOffset(offset);

        UpdateGridSize barParentGrid = barParent.GetComponent<UpdateGridSize>();
        barParentGrid.UpdateOffset(offset);

        UpdateGridSize conjectureParentGrid = conjectureParent.GetComponent<UpdateGridSize>();
        conjectureParentGrid.UpdateOffset(offset);
    }

    private void Clear()
    {
        letterList.Clear();
        conjectureString = "";

        for (int i = buttonParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(buttonParent.GetChild(i).gameObject);
        }

        for (int i = barParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(barParent.GetChild(i).gameObject);
        }

        for (int i = conjectureParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(conjectureParent.GetChild(i).gameObject);
        }
    }

    private void SplitWord(string _word)
    {
        for (int i = 0; i < _word.Length; i++)
        {
            letterList.Add(_word[i]);
        }
    }

    private void ShuffleWord(List<char> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            int randomIndex = Random.Range(i, _list.Count);
            (_list[i], _list[randomIndex]) = (_list[randomIndex], _list[i]);
        }
    }

    private void GenerateButtons(List<char> _list)
    {
        foreach (char letter in _list)
        {
            // Get Button Object
            GameObject buttonObj = Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI/MiniGame/Security", "Word Button"));
            buttonObj.transform.SetParent(buttonParent);

            // Init TMP
            TextMeshProUGUI tmp = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = letter.ToString();

            // Init Letter Button
            LetterButton letterButton = buttonObj.GetComponent<LetterButton>();
            letterButton.SetLetter(letter);

            // Add Button Event
            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => ConjectureWord(letterButton, button));

            GameObject barObj = Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI/MiniGame/Security", "Word Bar"));
            barObj.transform.SetParent(barParent);

        }
    }

    private void ConjectureWord(LetterButton _letterButton, Button _button)
    {
        char letter = _letterButton.GetLetter();

        conjectureString += letter;
        _button.enabled = false;

        GameObject tmpObject = Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI/MiniGame/Security", "Conjecture TMP"));
        tmpObject.transform.SetParent(conjectureParent);

        TextMeshProUGUI tmp = tmpObject.GetComponent<TextMeshProUGUI>();
        tmp.text = letter.ToString();

        if (conjectureString.Length == wordLength)
        {
            CompareAnswer();
        }
    }

    private void CompareAnswer()
    {
        if (conjectureString == currentWord)
        {
            Debug.Log("Success");

            // Next Stage
            wordListIdx++;

            if (wordListIdx >= wordList.Count) uiBase.Close();
            else InitScreen(wordListIdx);
        }
        else
        {
            Debug.Log("Failed!");
            InitScreen(wordListIdx);
        }
    }
}
