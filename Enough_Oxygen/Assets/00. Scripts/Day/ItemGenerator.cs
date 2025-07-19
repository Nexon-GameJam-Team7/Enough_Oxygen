using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject IngredientGroup;

    public GameObject SaucePrefab;
    public GameObject FishPrefab;
    public GameObject SeaweedPrefab;
    public GameObject PotPrefab;

    public Vector3 SaucePos;
    public Vector3 FishPos;
    public Vector3 SeaweedPos;
    public Vector3 PotPos;

    public void GenerateItem(string name)
    {
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
        else if(name == "Pot")
        {
            prefab = PotPrefab;
            pos = PotPos;
        }

        GameObject newPrefab = Instantiate(prefab);
        newPrefab.transform.position = pos;
        newPrefab.name = newPrefab.name.Substring(0, newPrefab.name.Length - 7);
        if (name != "Pot")
            newPrefab.transform.parent = IngredientGroup.transform;
    }
}
