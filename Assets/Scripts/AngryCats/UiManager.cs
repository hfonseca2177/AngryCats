using TMPro;
using UnityEngine;

namespace AngryCats
{
    /**
     * Ui Manager to update the score 
     */
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private void OnEnable()
        {
            GameManager.OnScoreChange += UpdateScore;
        }

        private void OnDisable()
        {
            GameManager.OnScoreChange -= UpdateScore;
        }

        private void UpdateScore(int score)
        {   
            scoreText.SetText(score+"");
        }
        
    }

}