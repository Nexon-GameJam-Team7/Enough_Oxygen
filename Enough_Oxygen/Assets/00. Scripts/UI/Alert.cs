// Unity
using UnityEngine;
using UnityEngine.UI;

// System
using System.Collections;

// TMPro
using TMPro;

[DisallowMultipleComponent]
public class Alert : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private float maintenanceDuration = 2f;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI tmp;

    public ChangeCloth CutScenePlayer;

    private void Start()
    {
        StartCoroutine(CloseCoroutine());
    }

    public void OpenAlert(string _string)
    {
        tmp.text = _string;
        StartCoroutine(OpenCoroutine());
    }

    IEnumerator CloseCoroutine()
    {
        yield return new WaitForSeconds(maintenanceDuration);

        float elapsedTime = 0f;
        Color startColor = image.color;

        while (elapsedTime < duration)
        {
            yield return null;
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;
            float alpha = Mathf.Lerp(1f, 0f, t);  // 알파값 1 → 0으로 선형 보간
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, alpha);
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0f);

        Player player = FindObjectOfType<Player>();
        if (player != null) player.UnInteraction();

        Destroy(gameObject);
    }

    IEnumerator OpenCoroutine()
    {
        float elapsedTime = 0f;
        Color startColor = image.color;

        while (elapsedTime < duration)
        {
            yield return null;
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;
            float alpha = Mathf.Lerp(0f, 1f, t);
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, alpha);
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1f);
    }
}
