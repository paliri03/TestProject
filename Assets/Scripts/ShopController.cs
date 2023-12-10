using UnityEngine;

public class ShopController : MonoBehaviour
{
    public void BuyChest(int ticketReward)
    {
        TicketsController.ticketsCount += ticketReward;
    }
}
