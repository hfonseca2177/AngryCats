using UnityEngine;

namespace AngryCats
{
    /**
     * Enemy that patrol a range in X direction
     */
    public class PatrolEnemy : Enemy
    {

        [Header("range Area that the patrol enemy will be moving")]
        [SerializeField] private float patrolRange = 2;
        [Header("Enemy moving speed")]
        [SerializeField] private float speed = 0.5f;

        #region Variables

        private float _currentXPosition;
        private float _leftPositionLimit;
        private float _rightPositionLimit;
        private float _currentDirection = -1;
        #endregion
        

        private void Start()
        {
        
            //calculates the off limits
            _currentXPosition = transform.position.x;
            _leftPositionLimit =  _currentXPosition -  patrolRange;
            _rightPositionLimit =  _currentXPosition +  patrolRange;
        }

        // Update is called once per frame
        void Update()
        {
            _currentXPosition = transform.position.x;
            Move();    
        }

        //Move enemy in the current patrol direction
        private void Move()
        {
            //if off limits, invert direction
            if (_currentXPosition < _leftPositionLimit || _currentXPosition > _rightPositionLimit)
            {
                _currentDirection *= -1;
                FlipEnemy();
            }

            //Increments the current X position
            var newPosition = transform.position;
            _currentXPosition += _currentDirection * speed * Time.deltaTime;
            newPosition.x = _currentXPosition;
            transform.position = newPosition;
        }

        /**
         * Flips Enemy graphically
         */
        private void FlipEnemy()
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
