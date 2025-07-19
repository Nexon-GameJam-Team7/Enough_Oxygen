using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] private ObjectInteraction[] objectInteractions;

    public void Init()
    {
        for (int i = 0; i < objectInteractions.Length; i++)
        {
            objectInteractions[i].init();
            objectInteractions[i].GoBack();
        }

        CustomerMovement customer = FindObjectOfType<CustomerMovement>();
        if (customer != null) Destroy(customer.gameObject);

        Interactor_CuttingBoard ic = FindObjectOfType<Interactor_CuttingBoard>();
        if (ic != null) ic.Chopping();

        PlayerCooking playerCook = FindObjectOfType<PlayerCooking>();
        if (playerCook != null) playerCook.Init();


        CustomerGenerator cg = FindObjectOfType<CustomerGenerator>();
        if (cg != null)
        {
            cg.gameObject.SetActive(false);
            cg.gameObject.SetActive(true);
        }
    }
}
