using UnityEngine;

[CreateAssetMenu(fileName = "soldierAmount", menuName = "soldier Amount")]
public class SoldierAmountData : ScriptableObject
{
    [SerializeField] public int amount;

    public void AddAmount()
    {
        if (amount < 150) {
            amount += 5;
        }
    }

    public void SubAmount()
    {
        if (amount > 5) {
            amount -= 5;
        }
    }
}
