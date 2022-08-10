using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace angrycats
{
    public class PlayerController : MonoBehaviour
    {

        #region Tunning Properties
        [SerializeField] private float launchImpulse = 500;
        #endregion

        #region Variables

        private LineRenderer _lineRender;
        private Vector2 _initialPosition;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private bool _wasLauched;
        private float _timeSittingAround;
        private const float PositiveSceneEdgeLimit = 10;
        private const float NegativeSceneEdgeLimit = -20;
        private const float IddleTimeLimit = 3;

        #endregion

        #region Bultin Methods
        private void Awake()
        {
            _lineRender = GetComponent<LineRenderer>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _initialPosition = transform.position;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_wasLauched && _rigidbody.velocity.magnitude <= 0.1)
            {
                _timeSittingAround += Time.deltaTime;
            }

            if (isOffLimit(transform.position.y) || isOffLimit(transform.position.x)
            || _timeSittingAround > IddleTimeLimit)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }

        private void OnMouseDown()
        {
            _spriteRenderer.color = Color.red;
            _lineRender.enabled = true;
        }

        private void OnMouseEnter()
        {
            _spriteRenderer.color = Color.green;
        }

        private void OnMouseDrag()
        {
            DrawLine();
        }

        private void OnMouseUp()
        {
            _spriteRenderer.color = Color.white;
            LaunchCat();
        }

        #endregion

        #region Custom Methods

        private void DrawLine()
        {
            _lineRender.SetPosition(0, transform.position);
            _lineRender.SetPosition(1, _initialPosition);
        }

        private void SnapToMouseLocation()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }

        private void LaunchCat()
        {
            Vector2 direction = new Vector2(_initialPosition.x - transform.position.x, _initialPosition.y - transform.position.y);
            _rigidbody.AddForce(direction * launchImpulse);
            _rigidbody.gravityScale = 1;
            _wasLauched = true;
            _lineRender.enabled = false;

        }

        private bool isOffLimit(float axisPosition)
        {
            return axisPosition > PositiveSceneEdgeLimit || axisPosition < NegativeSceneEdgeLimit;
        }

        #endregion

    }
}


