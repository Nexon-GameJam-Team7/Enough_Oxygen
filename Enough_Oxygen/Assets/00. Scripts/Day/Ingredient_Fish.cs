using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient_Fish : ObjectInteraction
{
    public int usingStep = 0;  // 1: ������ �÷���, 2: ������
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

    public override void init()
    {
        base.init();
        usingStep = 0;
        isUsing = false;
    }

    public void Interact(GameObject target)
    {
        if (usingStep == 0)
        {
            isUsing = true;
            usingStep++;
            // ���� ������ �ø���
            OnDrop(target);
            target.GetComponent<Interactor_CuttingBoard>().myFish = this.gameObject;
        } else if (usingStep == 1)
        {
            usingStep++;
            // ���� ������
            target.GetComponent<Interactor_CuttingBoard>().Chopping();
            gameObject.GetComponent<SpriteRenderer>().sprite = Sprite_groundFish;
            initialPos = gameObject.transform.position;
        } else if (usingStep == 2)
        {
            // ���� �ֱ�
            if (target.name == "Pot1" || target.name == "Pot2")
                target.GetComponent<Interactor_Pot>().Interaction(this.gameObject);
        }
    }
}
