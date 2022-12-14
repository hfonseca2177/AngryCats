using System;
using System.Collections;
using UnityEngine;

namespace AngryCats
{
    /**
     * Basic Enemy implementation
     */
    public class Enemy : MonoBehaviour, IEnemy
    {
        [Header("Monster Death V-SFX")]
        [SerializeField] protected AudioClip deathSound;
        [SerializeField] protected ParticleSystem deathPS;
        [Header("Score value by killing this enemy")]
        [SerializeField] protected int scoreValue = 10;

        public static Action<int> OnDeath;

        private AudioSource _audioSource;

        protected void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        /**
         * Whenever another game object with collider collides with the enemy
         * Checks if it is the meaningful ones, player and crates
         * Collision with another enemy is ignored by default
         */
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(AngryCatsTags.PLAYER))
            {
                OnPlayerCollision(collision);
            }
            //check if a crate collided but from above
            else if (collision.collider.CompareTag(AngryCatsTags.CRATE) && collision.contacts[0].normal.y < -0.5)
            {
                OnCrateCollision(collision);
            }
            else if(collision.collider.CompareTag(AngryCatsTags.ENEMY))
            {
                OnEnemyCollision(collision);
            }
        }

        /**
         * On player collision, play Vsfx and destroy enemy
         */
        public virtual void OnPlayerCollision(Collision2D collision)
        {
            DeathVsFX();
            StartCoroutine(DestroyObjectWait());
            
        }

        /**
         * On Crate collision on Top of creature, play Vsfx and destroy enemy
         */
        public virtual void OnCrateCollision(Collision2D collision)
        {
            Crate crate = collision.gameObject.GetComponent<Crate>();
            crate.DestroyCrate();
           DestroyEnemy();
        }

        public virtual void OnEnemyCollision(Collision2D collision)
        {
            //Nothing happens by default
        }

        public void DestroyEnemy()
        {
            OnDeath.Invoke(GetScoreValue());
            DeathVsFX();
            StartCoroutine(DestroyObjectWait());
        }

        /**
         * Visual and sound Death effects are played
         */
        protected void DeathVsFX()
        {
            Instantiate(deathPS, transform.position, Quaternion.identity);
            _audioSource.PlayOneShot(deathSound, 1); 
        }

        /**
         * Wait few seconds before destroy the object in order to the audio has entire duration played
         */
        protected IEnumerator DestroyObjectWait()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }

        public virtual int GetScoreValue()
        {
            return scoreValue;
        }
    }
}