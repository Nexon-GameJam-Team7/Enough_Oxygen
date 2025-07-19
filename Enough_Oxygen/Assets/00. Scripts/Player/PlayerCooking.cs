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

    [SerializeField]
    private TMP_Text text = null;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("GameManager"))
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        else
            Debug.Log("No GM");
        text.text = "" + gm.money;

        if (GameObject.Find("CustomerGenerator"))
            cg = GameObject.Find("CustomerGenerator").GetComponent<CustomerGenerator>();
        else
            Debug.Log("No CG");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero, 0f);
            if (hits.Length == 1 && holdingObj != null)
            {
                if (!holdingObj.CompareTag("Pot"))
                {
                    holdingObj.GetComponent<ObjectInteraction>().GoBack();
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
                    // 재료 든 상태에서 다른 재료 선택
                    // 기존 오브젝트 놓기
                    if (holdingObj != null)
                    {
                        // 완성 요리 든 상태에서는 쟁반만 선택 가능
                        if ((holdingObj.name == "Pot1" || holdingObj.name == "Pot2") && holdingObj.GetComponent<Interactor_Pot>().boiledTime != 0)
                            break;
                        holdingObj.GetComponent<ObjectInteraction>().GoBack();
                    }
                    // 다른 오브젝트 새로 집기
                    if (!clickedObj.GetComponent<ObjectInteraction>().isUsing && (clickedObj.gameObject.tag != "Pot" || clickedObj.gameObject.tag != "Temp"))
                        holdingObj = clickedObj;

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
                            // 제출 및 평가
                            Debug.Log("제출 및 평가");
                            gm.money += cg.curCustomer.GetComponent<CustomerMovement>().GetFood(clickedObj);
                            text.text = "" + gm.money;
                            Destroy(clickedObj.gameObject);
                            canReadyMenu = true;
                            break;
                        }

                        // 트레이에 뭐 있으면 완성된 냄비 집지 않게 처리하기
                        if (canReadyMenu || !clickedObj.GetComponent<Interactor_Pot>().isUsing)
                        {
                            Color newAlpha = clickedObj.GetComponent<SpriteRenderer>().color;
                            newAlpha.a = 1;
                            clickedObj.GetComponent<SpriteRenderer>().color = newAlpha;

                            holdingObj = clickedObj;
                        }

                        if (clickedObj.GetComponent<Interactor_Pot>().isUsing && canReadyMenu)
                        {
                            clickedObj.GetComponent<Interactor_Pot>().StopCoroutine("BurningTimer");
                            Debug.Log(clickedObj.GetComponent<Interactor_Pot>().boiledTime);
                        }
                    } else if (clickedObj.GetComponent<Interactor_Pot>().isUsing)
                    {
                        // 조리 마지막 단계: 고명 넣기만 가능
                        if (clickedObj.GetComponent<Interactor_Pot>().finalStep) {
                            if (holdingObj.name == "Seaweed" && !clickedObj.GetComponent<Interactor_Pot>().order[3])
                            {
                                Debug.Log("미역?");
                                clickedObj.GetComponent<Interactor_Pot>().Interaction(holdingObj);
                                holdingObj = null;
                                break;
                            } else
                            {
                                break;
                            }
                        }
                        // 손질되지 않은 재료, 고명 넣으려 하면 무시
                        if (holdingObj.name == "Seaweed" || (holdingObj.name == "Fish" && holdingObj.GetComponent<Ingredient_Fish>().usingStep < 2))
                            break;
                        // 냄비에 재료 넣기
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
                            holdingObj.GetComponent<SpriteRenderer>().sprite = holdingObj.GetComponent<Interactor_Pot>().Sprite_endPot;
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
