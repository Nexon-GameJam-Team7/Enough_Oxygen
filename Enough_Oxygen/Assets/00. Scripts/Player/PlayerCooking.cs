using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCooking : MonoBehaviour
{
    public GameObject holdingObj = null;
    public GameManager gm = null;
    public CustomerGenerator cg = null;
    public bool canReadyMenu = true;
    public GameObject myChoppingBoard;

    // Start is called before the first frame update
    void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        holdingObj = null;
        canReadyMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cg == null) cg = GameObject.Find("CustomerGenerator").GetComponent<CustomerGenerator>();


        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero, 0f);
            if (hits.Length == 1 && holdingObj != null)
            {
                if (!holdingObj.CompareTag("Pot"))
                {
                    holdingObj.GetComponent<ObjectInteraction>().GoBack();
                    if (holdingObj.name == "Fish")
                    {
                        Color newAlpha = holdingObj.GetComponent<SpriteRenderer>().color;
                        newAlpha.a = 1;
                        holdingObj.GetComponent<SpriteRenderer>().color = newAlpha;
                    }
                    holdingObj = null;
                }
                else if (holdingObj.GetComponent<Interactor_Pot>().boiledTime == 0)
                {
                    holdingObj.GetComponent<Interactor_Pot>().GoBack();
                    holdingObj = null;
                }
                hits = new RaycastHit2D[0];
            }

            foreach (var hit in hits)
            {
                GameObject clickedObj = hit.collider.gameObject;

                if (clickedObj == holdingObj || (clickedObj.name == "Tray" && !canReadyMenu))
                    continue;
                Debug.Log("clicked: " + clickedObj.name);

                if (clickedObj.CompareTag("Ingredients") && !clickedObj.GetComponent<ObjectInteraction>().isUsing)
                {
                    // ��� �� ���¿��� �ٸ� ��� ����
                    // ���� ������Ʈ ����
                    if (holdingObj != null)
                    {
                        // �ϼ� �丮 �� ���¿����� ��ݸ� ���� ����
                        if ((holdingObj.name == "Pot1" || holdingObj.name == "Pot2") && holdingObj.GetComponent<Interactor_Pot>().boiledTime != 0)
                            break;
                        holdingObj.GetComponent<ObjectInteraction>().GoBack();
                        if (holdingObj.name == "Fish")
                        {
                            Color newAlpha = holdingObj.GetComponent<SpriteRenderer>().color;
                            newAlpha.a = 1;
                            holdingObj.GetComponent<SpriteRenderer>().color = newAlpha;
                        }
                    }
                    // �ٸ� ������Ʈ ���� ����
                    if (!clickedObj.GetComponent<ObjectInteraction>().isUsing && (clickedObj.gameObject.tag != "Pot" || clickedObj.gameObject.tag != "Temp"))
                    {
                        Color newAlpha = clickedObj.GetComponent<SpriteRenderer>().color;
                        newAlpha.a = 1;
                        clickedObj.GetComponent<SpriteRenderer>().color = newAlpha;

                        holdingObj = clickedObj;
                    }

                    break;
                } else if (clickedObj.CompareTag("CuttingBoard"))
                {
                    if (holdingObj != null)
                    {
                        if ((holdingObj.name == "Seaweed" || holdingObj.name == "Sauce") && !holdingObj.CompareTag("Pot"))
                        {
                            holdingObj.GetComponent<Ingredient_Toppings>().GoBack();
                            holdingObj = null;
                            break;
                        } else if (holdingObj.CompareTag("Pot"))
                        {
                            if (!holdingObj.GetComponent<Interactor_Pot>().finalStep)
                            {
                                holdingObj.GetComponent<Interactor_Pot>().GoBack();
                                holdingObj = null;
                            }
                            break;
                        }
                    }

                    GameObject fish = null;
                    if (holdingObj != null && holdingObj.name == "Fish")
                    {
                        fish = holdingObj;
                        holdingObj = null;
                    }
                    else if (clickedObj.GetComponent<Interactor_CuttingBoard>().myFish != null)
                    {
                        fish = clickedObj.GetComponent<Interactor_CuttingBoard>().myFish;
                        if (fish.GetComponent<Ingredient_Fish>().usingStep == 2)
                        {
                            holdingObj = fish;
                            break;
                        }
                    }
                    if (fish != null)
                        fish.GetComponent<Ingredient_Fish>().Interact(clickedObj);

                    break;
                } else if (clickedObj.CompareTag("Pot"))
                {
                    if (holdingObj == null) {
                        if (cg.curCustomer != null && cg.isCustomerReady && clickedObj.GetComponent<Interactor_Pot>().finalStep)
                        {
                            // ���� �� ��
                            Debug.Log("���� �� ��");
                            GameManager.Data.data.money += cg.curCustomer.GetComponent<CustomerMovement>().GetFood(clickedObj);
                            GameManager.Sound.SFXPlay("coin");
                            Destroy(clickedObj.gameObject);
                            canReadyMenu = true;
                            break;
                        }

                        // Ʈ���̿� �� ������ �ϼ��� ���� ���� �ʰ� ó���ϱ�
                        if (canReadyMenu || !clickedObj.GetComponent<Interactor_Pot>().isUsing)
                        {
                            Color newAlpha = clickedObj.GetComponent<SpriteRenderer>().color;
                            newAlpha.a = 1;
                            clickedObj.GetComponent<SpriteRenderer>().color = newAlpha;

                            holdingObj = clickedObj;

                            GameManager.Sound.SFXPlay("bowlclatter");
                        }

                        if (clickedObj.GetComponent<Interactor_Pot>().isUsing && canReadyMenu)
                        {
                            clickedObj.GetComponent<Interactor_Pot>().StopCoroutine("BurningTimer");
                            Debug.Log(clickedObj.GetComponent<Interactor_Pot>().boiledTime);
                        }
                    } else if (clickedObj.GetComponent<Interactor_Pot>().isUsing)
                    {
                        // ���� ������ �ܰ�: ��� �ֱ⸸ ����
                        if (clickedObj.GetComponent<Interactor_Pot>().finalStep) {
                            if (holdingObj.name == "Seaweed" && !clickedObj.GetComponent<Interactor_Pot>().order[3])
                            {
                                Debug.Log("�̿�?");
                                clickedObj.GetComponent<Interactor_Pot>().Interaction(holdingObj);
                                holdingObj = null;
                                break;
                            } else
                            {
                                break;
                            }
                        }
                        // �������� ���� ���, ��� ������ �ϸ� ����
                        if (holdingObj.name == "Seaweed" || (holdingObj.name == "Fish" && holdingObj.GetComponent<Ingredient_Fish>().usingStep < 2))
                            break;
                        // ���� ��� �ֱ�
                        if ((holdingObj.name == "Sauce" && clickedObj.GetComponent<Interactor_Pot>().orderNum == 1) || (holdingObj.name == "Fish" && clickedObj.GetComponent<Interactor_Pot>().orderNum == 2))
                        {
                            if (holdingObj.name == "Fish")
                                myChoppingBoard.GetComponent<Interactor_CuttingBoard>().myFish = null;
                            clickedObj.GetComponent<Interactor_Pot>().Interaction(holdingObj);
                            holdingObj = null;
                        }
                    }

                    break;
                } else if (clickedObj.CompareTag("Water"))
                {
                    if (holdingObj != null)
                    {
                        if ((holdingObj.name == "Pot1" || holdingObj.name == "Pot2") && !holdingObj.GetComponent<Interactor_Pot>().water)
                        {
                            holdingObj.GetComponent<Interactor_Pot>().Interaction(clickedObj);
                        }
                    }
                } else if (clickedObj.CompareTag("Fire"))
                {
                    if (holdingObj != null)
                    {
                        if ((holdingObj.name == "Pot1" || holdingObj.name == "Pot2") && holdingObj.GetComponent<Interactor_Pot>().water && !holdingObj.GetComponent<Interactor_Pot>().isUsing)
                        {
                            holdingObj.GetComponent<Interactor_Pot>().Interaction(clickedObj);
                            holdingObj = null;
                        }
                    }

                    break;
                } else if (clickedObj.CompareTag("Tray"))
                {
                    if (holdingObj != null)
                    {
                        if ((holdingObj.name == "Pot1" || holdingObj.name == "Pot2") && holdingObj.GetComponent<Interactor_Pot>().boiledTime != 0)
                        {
                            holdingObj.transform.position = clickedObj.transform.GetChild(0).position;
                            holdingObj.GetComponent<Interactor_Pot>().finalStep = true;
                            holdingObj = null;
                            canReadyMenu = false;
                        }
                    }

                    break;
                }
            }
        }

        if (holdingObj != null)
        {
            holdingObj.gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        }
    }
}
