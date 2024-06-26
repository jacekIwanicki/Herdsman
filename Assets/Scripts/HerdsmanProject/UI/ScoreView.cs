using TMPro;
using UnityEngine;

namespace HerdsmanProject.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text scoreValue;
        [SerializeField]
        private ScoreController scoreController;

        private void OnEnable()
        {
            UpdateScoreText();
            scoreController.OnScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            scoreController.OnScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int scoreDifference)
        {
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreValue.SetText(scoreController.Score.ToString());
        }
    }
}