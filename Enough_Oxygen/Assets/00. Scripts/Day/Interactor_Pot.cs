using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor_Pot : ObjectInteraction
{
    private ItemGenerator itemGenerator;
    private GameObject[] ingredientPos = new GameObject[3];
    private bool[] hasIngredients = { false, false, false };   // 양념장, 생선, 미역 고명 순서

    public Sprite Sprite_Pot = null;
    public Sprite Sprite_waterPot = null;
    public Sprite Sprite_burnedWaterPot = null;
    public Sprite Sprite_endPot = null;
    public bool water = false;
    private bool onSink = false;
    public bool finalStep = false;

    // 평가 요소
    public int boiledTime = 0;
    public bool[] order = { false, false, false, false };   // 1: 물, 2: 양념장, 3: 다진 생선, 4: 고명
    public int orderNum = 0;


    // Start is called before the first frame update
    protected void Start()
    {
        itemGenerator = GameObject.Find("ItemGenerator").GetComponent<ItemGenerator>();
        isUsing = false;
        base.init();
        for (int i = 0; i < 3; i++)
        {
            ingredientPos[i] = transform.GetChild(i).gameObject;
        }
    }

    public void Interaction(GameObject target)
    {
        if (target.gameObject.CompareTag("Ingredients"))
        {
            for (int i = 0; i < 3; i++)
            {
                if (!hasIngredients[i] && target.gameObject.name + "Pos" == ingredientPos[i].gameObject.name)
                {
                    target.gameObject.transform.SetParent(ingredientPos[i].gameObject.transform);
                    target.gameObject.transform.position = ingredientPos[i].gameObject.transform.position;
                    itemGenerator.GenerateItem(target.name);
                    target.gameObject.tag = "Temp";
                    target.gameObject.GetComponent<ObjectInteraction>().isUsing = true;
                    hasIngredients[i] = true;
                    if (target.name != "Seaweed")
                        order[orderNum++] = true;
                    else
                        order[3] = true;
                }
            }
            // 각각 원래 자리에 프리팹 생성
        } else if (target.gameObject.CompareTag("Water"))
        {
            if (!water)
            {
                StartCoroutine("PuttingWaterTimer");
            }
        }
        else if (target.gameObject.CompareTag("Fire"))
        {
            // 화구에 냄비 고정
            gameObject.transform.position = new Vector3(target.gameObject.transform.position.x, target.gameObject.transform.position.y + 0.5f, target.gameObject.transform.position.z);
            isUsing = true;
            itemGenerator.GenerateItem(this.gameObject.name);
            // 타이머 시작
            StartCoroutine("BurningTimer");
        }
    }

    public int Rating()
    {
        int cost = 50;

        bool checkRecipe = true;
        for (int i = 0; i < 3; i++)
            if (!order[i])
                checkRecipe = false;
        Debug.Log("레시피: " + checkRecipe);

        int boiledDegree = 0;   // 0: 맹물, 1: 적절, 2: 탐
        if (boiledTime >= 8)
            boiledDegree = 2;
        else if (boiledTime >= 4)
            boiledDegree = 1;

        if (boiledDegree == 1 && checkRecipe && order[3])
            cost = 500;
        else if ((boiledDegree == 1 && checkRecipe && !order[3]) || (boiledDegree == 2 && checkRecipe && order[3]) || (boiledDegree == 0 && checkRecipe && order[3]))
            cost = 300;

        return cost;
    }

    IEnumerator BurningTimer()
    {
        while (boiledTime < 8)
        {
            yield return new WaitForSeconds(1f);
            boiledTime++;
        }
        
        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite_burnedWaterPot;
    }

    IEnumerator PuttingWaterTimer()
    {
        int puttingTime = 0;
        while (onSink && puttingTime < 2)
        {
            yield return new WaitForSeconds(1f);
            puttingTime++;
        }
        if (onSink && orderNum == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Sprite_waterPot;
            water = true;
            order[orderNum++] = true;
        }
    }

    public override void GoBack()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite_Pot;
        Color newAlpha = gameObject.GetComponent<SpriteRenderer>().color;
        newAlpha.a = 0;
        gameObject.GetComponent<SpriteRenderer>().color = newAlpha;
        water = false;
        isUsing = false;
        order = new bool[] { false, false, false, false };
        orderNum = 0;
        base.GoBack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Sink")
            onSink = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Sink")
            onSink = false;
    }
}
