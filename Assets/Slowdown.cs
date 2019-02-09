using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slowdown : MonoBehaviour
{
    public Transform slowPos;
    public float slowRange;
    public LayerMask slowLayer;
    public float slowCooldown = 10;
    public float cooldownRestoreRate;
    public float cooldownReductionRate;
    private bool slow;
    private bool reset;

    // The enemy and player can be slowed by different multipliers
    public float playerSlowdownMultiplier;
    public float enemySlowdownMultiplier;

    // Stuff for bar
    public Image cooldownBar;

    public Transform camera;

    public KeyCode slowKey = KeyCode.LeftShift;

    // Update is called once per frame
    void Start()
    {
        reset = true;
    }

    void Update()
    {
        // While the key is pressed, keep checking for enemies that need to be slowed
        if (Input.GetKey(slowKey) && slowCooldown >= 0 && reset)
        {
            slow = true;

            // Zoom camera in
            camera.GetComponent<CompleteCameraController>().zoomIn = true;

            // Slow yourself if applicable
            gameObject.GetComponent<PlatformerCharacter3D>().setSpeedMultiplier(playerSlowdownMultiplier);

            // Get all enemies to be slowed, then slow them
            Collider[] objectsSlowed = Physics.OverlapSphere(slowPos.position, slowRange, slowLayer);

            for (int i = 0; i < objectsSlowed.Length; i++)
            {
                objectsSlowed[i].GetComponent<AIAggression>().setSpeedMultiplier(enemySlowdownMultiplier);
            }
        }

        // When Slow Key is let up
        else if (Input.GetKeyUp(slowKey))
        {
            // Get all enemies that were slowed, then revert back to normal
            Collider[] objectsSlowed = Physics.OverlapSphere(slowPos.position, slowRange, slowLayer);

            for (int i = 0; i < objectsSlowed.Length; i++)
            {
                objectsSlowed[i].GetComponent<AIAggression>().setSpeedMultiplier(1);
            }

            // Revert your speed if applicable
            gameObject.GetComponent<PlatformerCharacter3D>().setSpeedMultiplier(1);

            // Zoom camera back out
            camera.GetComponent<CompleteCameraController>().zoomIn = false;

            slow = false;
            reset = true;
        }
        if (slowCooldown <= 0)
        {
            // Zoom camera back out
            camera.GetComponent<CompleteCameraController>().zoomIn = false;

            reset = false;
        }

        if (!slow && slowCooldown < 10 && !Input.GetKeyDown(slowKey) && !Input.GetKey(slowKey))
        {
            slowCooldown += Time.deltaTime * cooldownRestoreRate;
        }
        if (slow && slowCooldown > 0)
        {
            slowCooldown -= Time.deltaTime * cooldownReductionRate;
        }

        cooldownBar.fillAmount = slowCooldown / 10f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(slowPos.position, slowRange);
    }
}
