using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 150;
    [SerializeField] private int currentBalance;
    [SerializeField] private TextMeshProUGUI balanceDisplay;

    public int CurrentBalance { get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int deposit)
    {
        currentBalance += Mathf.Abs(deposit);
        UpdateDisplay();
    }

    public void Withdraw(int withdrawl)
    {
        currentBalance -= Mathf.Abs(withdrawl);
        UpdateDisplay();

        if (currentBalance < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    private void UpdateDisplay()
    {
        balanceDisplay.text = "Gold: " + currentBalance;
    }
}
