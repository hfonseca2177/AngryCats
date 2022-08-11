using System.Collections;
using UnityEngine;

namespace AngryCats
{
    /**
     * Crate Behavior
     */
    public class Crate : MonoBehaviour
    {

        [SerializeField]private ParticleSystem crateExplosion;
        [SerializeField] private AudioClip crateExplosionSound;

        private AudioSource _audioSource;
    
        // Start is called before the first frame update
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        /**
         * Play crate explosion effects and destroy game object 
         */
        public void DestroyCrate()
        {
            Instantiate(crateExplosion, transform.position, Quaternion.identity);
            _audioSource.PlayOneShot(crateExplosionSound);
            StartCoroutine(DestroyObjectWait());
        }
    
        /**
         * Wait few seconds before destroy the object in order to the audio has entire duration played
         */
        private IEnumerator DestroyObjectWait()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
    }
}
