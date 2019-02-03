using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Slowdown : MonoBehaviour
    {
        public Transform slowPos;
        public float slowRange;
        public LayerMask slowLayer;

        public float playerSlowdownMultiplier;
        public float enemySlowdownMultiplier;

        public Transform camera;

        public KeyCode slowKey = KeyCode.LeftShift;

        // Update is called once per frame
        void Update()
        {
            // When Slow Key (default Left Shift) is pressed down
            if (Input.GetKeyDown(slowKey))
            {
                // Get all enemies to be slowed, then slow them
                Collider2D[] objectsSlowed = Physics2D.OverlapCircleAll(slowPos.position, slowRange, slowLayer);
                for (int i = 0; i < objectsSlowed.Length; i++)
                {
                    objectsSlowed[i].GetComponent<AIAggression>().setSpeedMultiplier(enemySlowdownMultiplier);
                }

                // Slow yourself if applicable
                gameObject.GetComponent<PlatformerCharacter2D>().setSpeedMultiplier(playerSlowdownMultiplier);

                // Zoom camera in
                camera.GetComponent<CompleteCameraController>().zoom();
            }

            // When Slow Key is let up
            else if (Input.GetKeyUp(slowKey))
            {
                // Get all enemies that were slowed, then revert back to normal
                Collider2D[] objectsSlowed = Physics2D.OverlapCircleAll(slowPos.position, slowRange, slowLayer);
                for (int i = 0; i < objectsSlowed.Length; i++)
                {
                    objectsSlowed[i].GetComponent<AIAggression>().setSpeedMultiplier(1);
                }

                // Revert your speed if applicable
                gameObject.GetComponent<PlatformerCharacter2D>().setSpeedMultiplier(1);

                // Zoom camera back out
                camera.GetComponent<CompleteCameraController>().zoom();
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(slowPos.position, slowRange);
        }
    }
}
