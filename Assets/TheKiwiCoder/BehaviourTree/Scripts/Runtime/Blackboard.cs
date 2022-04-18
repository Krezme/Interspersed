using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public class Blackboard {
        public Vector3 moveToPosition; /// the position for the AI to move to
        public bool isFollowingPlayer; /// a check to see if it should follow the player or not
        public bool playerInAttackRange; /// a check to see if the Ai is in attack range
        public float distance; /// the distance between the AI and the player
        public Vector3 directionToTarget;
        public bool isCurrentlyDiveBombing;
        public bool isCurrentlyShooting;
    }
}