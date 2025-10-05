using UnityEngine;

namespace SpatialPartitionPattern
{
    public class Enemy : Soldier
    {
        Vector3 currentTarget;
        Vector3 oldPos;
        float mapWidth;
        Grid grid;

        public Enemy(GameObject soldierObj, float mapWidth, Grid grid)
        {
            this.soldierTrans = soldierObj.transform;
            this.soldierMeshRenderer = soldierObj.GetComponent<MeshRenderer>();
            this.mapWidth = mapWidth;
            this.grid = grid;

            //Add this unit to the grid
            grid.Add(this);
            oldPos = soldierTrans.position;
            this.walkSpeed = 5f;

            //Give it a random coordinate to move towards
            GetNewTarget();
        }

        public override void Move()
        {            
            //Move towards the target
            soldierTrans.Translate(Vector3.forward * Time.deltaTime * walkSpeed);

            //See if the the cube has moved to another cell
            grid.Move(this, oldPos);

            //Save the old position
            oldPos = soldierTrans.position;

            //If the soldier has reached the target, find a new target
            if ((soldierTrans.position - currentTarget).magnitude < 1f)
            {
                GetNewTarget();
            }
        }


        //Give the enemy a new target to move towards and rotate towards that target
        void GetNewTarget()
        {
            currentTarget = new Vector3(Random.Range(0f, mapWidth), 0.5f, Random.Range(0f, mapWidth));

            //Rotate towards the target
            soldierTrans.rotation = Quaternion.LookRotation(currentTarget - soldierTrans.position);
        }
    }
}