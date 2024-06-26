using Movement;
using UnityEngine;

namespace HerdsmanProject.Common
{
    public class PlayerController : MonoBehaviour
    {
        private IMovementController movementController;

        private void Awake()
        {
            movementController = GetComponent<IMovementController>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                movementController.SetTargetPosition(targetPosition);
            }
        }
    }
}