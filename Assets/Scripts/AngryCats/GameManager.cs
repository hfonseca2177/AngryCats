using System;
using UnityEngine;

namespace AngryCats
{
    /**
    * Singleton resposible for computing the score
    */
    public class GameManager : MonoBehaviour
    {
                        
        public static GameManager Instance { get; private set; }

        public static Action<int> OnScoreChange;
        

        private int _score;
        public int Score => _score;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnEnable()
        {
            Enemy.OnDeath += ComputeEnemyDeath;
            Humanoid.OnHumanHug += ComputeHumanHug;
        }

        private void OnDisable()
        {
            Enemy.OnDeath -= ComputeEnemyDeath;
            Humanoid.OnHumanHug -= ComputeHumanHug;
        }

        /**
         * Updates score when hits humanoid
         */
        private void ComputeHumanHug(int score)
        {
            _score += score;
            OnScoreChange.Invoke(_score);
        }

        /**
         * Updates score when killable enemy dies
         */
        private void ComputeEnemyDeath(int score)
        {
            _score += score;
            OnScoreChange.Invoke(_score);
        }

    }
}