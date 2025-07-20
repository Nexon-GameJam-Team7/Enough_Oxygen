using UnityEngine;

public class MainTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Sound.BGMPlay("bgm1");

        GameManager.Data.data.clearCount = 0;
        GameManager.Data.data.money = 0;
        GameManager.Data.data.haveItem = new bool[4];
    }

    public void PlayBookSound()
    {
        GameManager.Sound.SFXPlay("paper");
    }
}
