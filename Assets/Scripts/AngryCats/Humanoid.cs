using System;
using UnityEngine;

namespace AngryCats
{
    /**
     * Humanoid: When cat hits him it gets some love
     */
    public class Humanoid : MonoBehaviour
    {
        private Collider2D _collider2D;

        private void Start()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        /**
         * On Collider trigger, callbacks Cat and disables the collider
         */
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(AngryCatsTags.PLAYER))
            {
                PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
                playerController.OnHitHumanoid(gameObject);
                _collider2D.enabled = false;
            }
        }
    }
}
