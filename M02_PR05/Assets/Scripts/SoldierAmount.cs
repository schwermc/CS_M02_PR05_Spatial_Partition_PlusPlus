using UnityEngine;
using TMPro;
using SpatialPartitionPattern;
using UnityEngine.SceneManagement;

public class SoldierAmount : MonoBehaviour
{
    public SoldierAmountData soldierData;
    private TextMeshProUGUI text;
    private GameController gameController;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        gameController = GameObject.Find("_GameController").GetComponent<GameController>();
        soldierData.amount = gameController.soldierAmount();
        text.text = soldierData.amount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = soldierData.amount.ToString();
    }

    public void refreshScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void add()
    {
        soldierData.AddAmount();
    }

    public void sub()
    {
        soldierData.SubAmount();
    }
}
