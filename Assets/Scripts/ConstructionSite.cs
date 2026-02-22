using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    private GameObject finalPrefab;
    private float buildTime;
    private float currentBuildTimer = 0f;
    private bool isBeingBuilt = false;

    // INJECTION METHOD: Data comes from BuildingManager, no need for Inspector assignment here
    public void Initialize(GameObject realPrefab, float time)
    {
        this.finalPrefab = realPrefab;
        this.buildTime = time;
    }

    public void StartBuilding()
    {
        isBeingBuilt = true;
        Debug.Log("Construction started...");
    }

    void Update()
    {
        if (!isBeingBuilt) return;

        currentBuildTimer += Time.deltaTime;

        // Visual feedback could be added here (e.g., scaling or changing transparency)

        if (currentBuildTimer >= buildTime)
        {
            FinishConstruction();
        }
    }

    private void FinishConstruction()
    {
        if (finalPrefab != null)
        {
            Instantiate(finalPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
