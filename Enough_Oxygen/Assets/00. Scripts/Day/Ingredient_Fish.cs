using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient_Fish : ObjectInteraction
{
    public int usingStep = 0;  // 1: 도마에 올려짐, 2: 다져짐
    public Sprite Sprite_groundFish = null;

    // Start is called before the first frame update
    protected void Start()
    {
        base.init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject target)
    {
        if (usingStep == 0)
        {
            isUsing = true;
            usingStep++;
            // 생선 도마에 올리기
            OnDrop(target);
            target.GetComponent<Interactor_CuttingBoard>().myFish = this.gameObject;
        } else if (usingStep == 1)
        {
            usingStep++;
            // 생선 다지기
            target.GetComponent<Interactor_CuttingBoard>().Chopping();
            gameObject.GetComponent<SpriteRenderer>().sprite = Sprite_groundFish;
            initialPos = gameObject.transform.position;
        } else if (usingStep == 2)
        {
            // 냄비에 넣기
            if (target.name == "Pot")
                target.GetComponent<Interactor_Pot>().Interaction(this.gameObject);
        }
    }
}
