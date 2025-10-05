using UnityEngine;

namespace SpatialPartitionPattern
{
    public class Friendly : Soldier
    {
        public Friendly(GameObject soldierObj, float mapWidth)
        {
            this.soldierTrans = soldierObj.transform;
            this.walkSpeed = 2f;
        }

        // Move towards the closest enemy - will always move within its grid
        public override void Move(Soldier closestEnemy)
        {
            soldierTrans.rotation = Quaternion.LookRotation(closestEnemy.soldierTrans.position - soldierTrans.position);
            soldierTrans.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
        }
    }
}