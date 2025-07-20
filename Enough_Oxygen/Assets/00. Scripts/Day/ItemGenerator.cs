using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject IngredientGroup;
    public GameObject DayGroup;

    public GameObject SaucePrefab;
    public GameObject FishPrefab;
    public GameObject SeaweedPrefab;
    public GameObject Pot1Prefab;
    public GameObject Pot2Prefab;

    public Vector3 SaucePos;
    public Vector3 FishPos;
    public Vector3 SeaweedPos;
    public Vector3 Pot1Pos;
    public Vector3 Pot2Pos;

    public void GenerateItem(string name)
    {
        Debug.Log("generate");
        GameObject prefab = null;
        Vector3 pos = new Vector3(0, 0, 0);
        if (name == "Sauce")
        {
            prefab = SaucePrefab;
            pos = SaucePos;
        }
        else if (name == "Fish")
        {
            prefab = FishPrefab;
            pos = FishPos;
        }
        else if(name == "Seaweed")
        {
            prefab = SeaweedPrefab;
            pos = SeaweedPos;
        }
        else if(name == "Pot1")
        {
            prefab = Pot1Prefab;
            pos = Pot1Pos;
        }
        else if (name == "Pot2")
        {
            prefab = Pot2Prefab;
            pos = Pot2Pos;
        }

        GameObject newPrefab = Instantiate(prefab);
        newPrefab.transform.position = pos;
        newPrefab.name = name;
        if (name != "Pot1" && name != "Pot2")
            newPrefab.transform.parent = IngredientGroup.transform;
        else
            newPrefab.transform.parent = DayGroup.transform;
    }
}
