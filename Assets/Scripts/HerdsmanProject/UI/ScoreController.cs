using System;
using UnityEngine;

namespace HerdsmanProject.UI
{
    public class ScoreController : MonoBehaviour
    {
        private int score = 0;

        public int Score
        {
            get { return score; }
            set
            {
                int difference = value - score;
                score = value;
                OnScoreChanged?.Invoke(difference);
            }
        }

        public event Action<int> OnScoreChanged;
    }
}