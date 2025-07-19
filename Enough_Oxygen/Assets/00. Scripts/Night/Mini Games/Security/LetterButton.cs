using UnityEngine;

[DisallowMultipleComponent]
public class LetterButton : MonoBehaviour
{
    private char letter;

    public void SetLetter(char _letter)
    {
        letter = _letter;
    }

    public char GetLetter()
    {
        return letter;
    }
}
