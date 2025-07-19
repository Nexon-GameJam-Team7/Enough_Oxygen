// Unity
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] checkObject;
    [SerializeField] private Image[] itemImages;

    public void CheckToDoList(int _idx)
    {
        if (_idx < checkObject.Length)
            checkObject[_idx].gameObject.SetActive(true);
    }

    public void DisplayGetItem(int _idx)
    {
        itemImages[_idx].gameObject.SetActive(true);
    }
}
