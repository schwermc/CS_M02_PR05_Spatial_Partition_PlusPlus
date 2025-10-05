using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpatialPartitionPattern
{
    public class GameController : MonoBehaviour
    {
        public GameObject friendlyObj;
        public GameObject enemyObj;

        // Change materials to detect which enemy is the closest
        public Material enemyMaterial;
        public Material closestEnemyMaterial;

        // To get a cleaner workspace, parent all soldiers to these empty gameobjects
        public Transform enemyParent;
        public Transform friendlyParent;

        // Store all soldiers in these lists
        List<Soldier> enemySoldiers = new List<Soldier>();
        List<Soldier> friendlySoldiers = new List<Soldier>();

        // Save the closest enemies to easier change back its material
        List<Soldier> closestEnemies = new List<Soldier>();

        // Grid data
        float mapWidth = 50f;
        int cellSize = 10;

        // Number of soldiers on each team
        public int numberOfSoldiers = 100;
        public bool spatialPartition = false;

        // UI timer
        public TextMeshProUGUI UItext;

        // The Spatial Partition grid
        Grid grid;

        void Start()
        {
            grid = new Grid((int)mapWidth, cellSize);

            // Add random enemies and friendly and store them in a list
            for (int i = 0; i < numberOfSoldiers; i++)
            {
                Vector3 randomPos = new Vector3(Random.Range(0f, mapWidth), 0.5f, Random.Range(0f, mapWidth));
                GameObject newEnemy = Instantiate(enemyObj, randomPos, Quaternion.identity) as GameObject;
                enemySoldiers.Add(new Enemy(newEnemy, mapWidth, grid));
                newEnemy.transform.parent = enemyParent;

                randomPos = new Vector3(Random.Range(0f, mapWidth), 0.5f, Random.Range(0f, mapWidth));
                GameObject newFriendly = Instantiate(friendlyObj, randomPos, Quaternion.identity) as GameObject;
                friendlySoldiers.Add(new Friendly(newFriendly, mapWidth));
                newFriendly.transform.parent = friendlyParent;
            }
        }

        void Update()
        {
            float startTime = Time.realtimeSinceStartup;

            // Move the enemies
            for (int i = 0; i < enemySoldiers.Count; i++)
            {
                enemySoldiers[i].Move();
            }

            // Reset material of the closest enemies
            for (int i = 0; i < closestEnemies.Count; i++)
            {
                closestEnemies[i].soldierMeshRenderer.material = enemyMaterial;
            }

            // Reset the list with closest enemies
            closestEnemies.Clear();
            Soldier closestEnemy;
            // For each friendly, find the closest enemy and change its color and chase it
            for (int i = 0; i < friendlySoldiers.Count; i++)
            {
                if (!spatialPartition)
                    closestEnemy = FindClosestEnemySlow(friendlySoldiers[i]);
                else
                    closestEnemy = grid.FindClosestEnemy(friendlySoldiers[i]);

                // If we found an enemy
                if (closestEnemy != null)
                {
                    closestEnemy.soldierMeshRenderer.material = closestEnemyMaterial;
                    closestEnemies.Add(closestEnemy);
                    friendlySoldiers[i].Move(closestEnemy);
                }
            }
            float elapsedTime = (Time.realtimeSinceStartup - startTime) * 1000f;
            UItext.text = elapsedTime.ToString("#.000");
            Debug.Log(elapsedTime);
        }

        // Find the closest enemy - slow version
        Soldier FindClosestEnemySlow(Soldier soldier)
        {
            Soldier closestEnemy = null;
            float bestDistSqr = Mathf.Infinity;

            // Loop thorugh all enemies
            for (int i = 0; i < enemySoldiers.Count; i++)
            {
                // The distance sqr between the soldier and this enemy
                float distSqr = (soldier.soldierTrans.position - enemySoldiers[i].soldierTrans.position).sqrMagnitude;

                // If this distance is better than the previous best distance, then we have found an enemy that's closer
                if (distSqr < bestDistSqr)
                {
                    bestDistSqr = distSqr;
                    closestEnemy = enemySoldiers[i];
                }
            }
            return closestEnemy;
        }

        public void switchSpatialPartition()
        {
            if (spatialPartition)
                spatialPartition = false;
            else
                spatialPartition = true;
        }
    }
}