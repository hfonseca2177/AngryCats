using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace angrycats
{
    public class PlayerController : MonoBehaviour
    {

        #region Tunning Properties
        [Header("Force multiplier to be applied to the launch")]
        [SerializeField] private float launchImpulse = 500;
        [Header("Idle time after launch before reset the level")]
        [SerializeField] private float idleTimeLimit = 3.0f;
        [Header("Max Range in X and Y coords to pull the Cat based on Initial position")]
        [SerializeField] private float maxRange = 8.0f;
        #endregion

        #region Variables

        //Cached components
        private LineRenderer _lineRender;
        private Vector2 _initialPosition;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        //Flag to navigate the idle state after launch
        private bool _wasLaunched;
        private float _idleTime;
        //Cache the ranges based on initial position and configured maxRage
        private float _xRangeLimit;
        private float _yTopRangeLimit;
        private float _yDownRangeLimit;
        
        

        #endregion

        #region Bultin Methods
        private void Awake()
        {
            //cache components
            _lineRender = GetComponent<LineRenderer>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //Cache local variables
            _initialPosition = transform.position;
            _xRangeLimit = _initialPosition.x - maxRange;
            _yTopRangeLimit = _initialPosition.y + maxRange;
            _yDownRangeLimit = _initialPosition.y - maxRange;
        }

        // Update is called once per frame
        void Update()
        {
            //If basically not moving (vector magnitude close to 0), starts counting idle time
            if (_wasLaunched && _rigidbody.velocity.magnitude <= 0.1)
            {
                _idleTime += Time.deltaTime;
            }

            //if extrapolates idle time limit, reload scene
            if (_idleTime > idleTimeLimit)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }

        private void OnMouseDown()
        {
            if (_wasLaunched) return;
            //Enables line if not launched yet
            _spriteRenderer.color = Color.red;
            _lineRender.enabled = true;
        }

        private void OnMouseEnter()
        {
            if (_wasLaunched) return;
            _spriteRenderer.color = Color.green;
        }

        private void OnMouseDrag()
        {   
            SnapToMouseLocation();
            DrawLine();
        }

        private void OnMouseUp()
        {
            _spriteRenderer.color = Color.white;
            LaunchCat();
        }

        #endregion


        #region Custom Methods

        /**
         * Set initial and end position for line renderer
         */
        private void DrawLine()
        {
            _lineRender.SetPosition(0, transform.position);
            _lineRender.SetPosition(1, _initialPosition);
        }

        /**
         * Snap Cat to mouse position while mouse dragging
         */
        private void SnapToMouseLocation()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = ClampMovementRange(mousePosition);
        }

        /**
         * Applies force and launches the cat 
         */
        private void LaunchCat()
        {
            //Gets the direction by getting the difference between the initial position and the current one
            Vector2 direction = new Vector2(_initialPosition.x - transform.position.x, _initialPosition.y - transform.position.y);
            _rigidbody.AddForce(direction * launchImpulse);
            //Enables the gravity scale to applies physics properly
            _rigidbody.gravityScale = 1;
            //_rigidbody.isKinematic = false;
            //Flags up to mark that it was launched
            _wasLaunched = true;
            //hide the line
            _lineRender.enabled = false;

        }

        /**
         * Controls the range that player can pull the cat
         */
        private Vector3 ClampMovementRange(Vector3 currentPosition)
        {   
            float fixedX = Mathf.Clamp(currentPosition.x,_xRangeLimit, _initialPosition.x);
            float fixedY = Mathf.Clamp(currentPosition.y, _yDownRangeLimit, _yTopRangeLimit);
            //Ignore Z - 2D context only
            return new Vector3(fixedX, fixedY, 0);
        }


        #endregion

    }
}


