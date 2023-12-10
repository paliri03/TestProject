using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int cost;
    [SerializeField] private int levelToUnlock;

    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject boughtPanel;

    [SerializeField] private GameObject lockImage;
    [SerializeField] private TextMeshProUGUI lockLevelText;

    private bool isBought
    {
        get { return PlayerPrefs.GetInt("PurchaseID" + id, 0) == 1 ? true : false; }
        set 
        {
            if (value) 
                PlayerPrefs.SetInt("PurchaseID" + id, 1);
            else 
                PlayerPrefs.SetInt("PurchaseID" + id, 0);
        }
    }

    private void OnEnable()
    {
        if (isBought) return;

        TicketsController.onTicketsCountChanged += UpdateButtonState;

        UpdateButtonState(TicketsController.ticketsCount);

        if (lockImage == null) return;
        if (LevelsController.lastCompletedLevel < levelToUnlock)
        {
            lockImage.SetActive(true);
            lockLevelText.text = "LV " + levelToUnlock;
        }
        else
            lockImage.SetActive(false);
    }

    private void Start()
    {
        if (isBought)
        {
            boughtPanel.SetActive(true);
            return;
        }

        costText.text = cost.ToString();
        buyButton.onClick.AddListener(() => Buy());
    }

    private void Buy()
    {
        if (TicketsController.ticketsCount < cost) return;

        TicketsController.ticketsCount -= cost;
        isBought = true;

        buyButton.enabled = false;
        boughtPanel.SetActive(true);
    }

    private void UpdateButtonState(int ticketsCount)
    {
        if (levelToUnlock > LevelsController.lastCompletedLevel || ticketsCount < cost)
            buyButton.interactable = false;
        else
            buyButton.interactable = true;
    }

    private void OnDisable()
    {
        if (isBought) return;

        TicketsController.onTicketsCountChanged -= UpdateButtonState;
    }
}
