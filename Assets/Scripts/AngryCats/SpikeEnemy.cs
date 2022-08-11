using UnityEngine;

namespace AngryCats
{
    /**
     * Player must not hit directly this enemy
     */
    public class SpikeEnemy : Enemy
    {
        public override void OnPlayerCollision(Collision2D collision)
        {
            PlayerController playerController = collision.collider.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage();
        }

        public override void OnEnemyCollision(Collision2D collision)
        {
            Enemy component = collision.gameObject.GetComponent<Enemy>();
            component.DestroyEnemy();
        }
    }
}
