using UnityEngine;

namespace AngryCats
{
    /**
     * Enemy interface for implementation of enemy behaviors
     */
    public interface IEnemy
    {
        void OnPlayerCollision(Collision2D collision);
        void OnCrateCollision(Collision2D collision);
        void OnEnemyCollision(Collision2D collision);
        
    }    
}

