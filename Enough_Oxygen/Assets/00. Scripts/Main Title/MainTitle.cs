using UnityEngine;

public class MainTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Sound.BGMPlay("bgm1");
    }

    public void PlayBookSound()
    {
        GameManager.Sound.SFXPlay("paper");
    }
}
