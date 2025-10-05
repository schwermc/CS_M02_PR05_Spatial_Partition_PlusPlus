using UnityEngine;

namespace SpatialPartitionPattern
{
    public class Soldier
    {
        public MeshRenderer soldierMeshRenderer;
        public Transform soldierTrans;
        protected float walkSpeed;

        // Has to do with the grid, so we can avoid storing all soldiers in an array
        // Instead we are going to use a linked list where all soldiers in the cell 
        // Are linked to each other
        public Soldier previousSoldier;
        public Soldier nextSoldier;

        public virtual void Move() { }

        public virtual void Move(Soldier soldier) { }
    }
}