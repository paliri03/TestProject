using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _ticketsText;

    public int DayNum 
    {
        get
        { 
            return int.Parse(_dayText.text.Remove(0, 3));
        }
        set
        { 
            _dayText.text = "DAY" + value;
        }
    } 

    public int TicketsCount
    {
        get
        {
            return int.Parse(_ticketsText.text.Remove(0, 1));
        }
        set
        {
            _ticketsText.text = "X" + value.ToString();
        }
    }

    public void SetCollected()
    {
        GetComponent<CanvasGroup>().alpha = 0.5f;
        GetComponent<Button>().interactable = false;
    }

    public void SetLocked()
    {
        GetComponent<Button>().interactable = false;
    }
}
