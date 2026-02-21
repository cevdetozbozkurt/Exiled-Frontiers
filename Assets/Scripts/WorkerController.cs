using UnityEngine;
using UnityEngine.AI;

public class WorkerController : MonoBehaviour
{
    public bool isSelected = false;
    private NavMeshAgent agent;

    private Material mat;
    private Color originColor;

    [Header("Toplama Ayarlari")]
    public float interactionDistance = 0.5f;
    public float defaultWorkTime = 2.0f;
    public float penaltyWorkTime = 6.0f;
    public int   defaultYield = 10;
    public int   penaltyYield = 5;

    private Transform targetResource;
    private bool isWorking = false;
    private float workTimer = 0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
        {
            mat = renderer.material;
            originColor = mat.color;
        }      
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        if(mat != null)
        {
            mat.color = isSelected ? Color.yellow : originColor;
        }
        
    }

    public void MoveTo(Vector3 destination)
    {
        targetResource = null;
        isWorking = false;
        if(agent != null)
        {
            agent.stoppingDistance = 0.5f; 
            agent.SetDestination(destination);
        }
    }

    public void SetGatherTarget(Transform resource)
    {
        targetResource = resource;
        isWorking = false;
        if(agent != null && targetResource != null)
        {
            agent.stoppingDistance = interactionDistance - 0.2f;
            agent.SetDestination(targetResource.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(targetResource != null)
        {
            Vector3 myPos = transform.position;
            myPos.y = 0f;

            Vector3 targetPos = targetResource.position;
            targetPos.y = 0f;

            float distance = Vector3.Distance(myPos, targetPos);

            if(distance <= interactionDistance)
            {
                if(!isWorking)
                {
                    agent.ResetPath();
                    isWorking = true;
                    workTimer = 0f;
                }
                ProcessResource();
            }
            else if(isWorking)
            {
                isWorking = false;
                agent.SetDestination(targetResource.position);
            }
        }
    }

    private void ProcessResource()
    {
        if(targetResource == null)
        {
            isWorking = false;
            return;
        }
        string resourceTag = targetResource.tag;
        bool isMatchingJob = CheckIfMatchingJob(resourceTag);
        
        Debug.Log("process basladi...");

        float requiredTime = isMatchingJob ? defaultWorkTime : penaltyWorkTime;
        workTimer += Time.deltaTime;

        if(workTimer >= requiredTime)
        {
            int yieldAmount = isMatchingJob ? defaultYield : penaltyYield;
            Debug.Log($"{gameObject.name} (Tag: {gameObject.tag}), {resourceTag} nesnesini isledi. Kazanilan: {yieldAmount}");

            GameObject destroyedResource = targetResource.gameObject;
            targetResource = null;
            isWorking = false;
            ResourceManager.Instance.AddResource(resourceTag, yieldAmount);
            Destroy(destroyedResource);

            if (isMatchingJob)
            {
                FindNextResourceOnScreen(resourceTag, destroyedResource);
            }
            else
            {
                Debug.Log($"{gameObject.name} uyumlu bir meslege sahip olmadigi icin yeni kaynak aramiyor, bekliyor.");
            }
        }
    }

    private bool CheckIfMatchingJob(string resTag)
    {
        string myTag = gameObject.tag;

        if (myTag == "Lumberjack" && resTag == "Tree") return true;
        if (myTag == "Miner" && (resTag == "Rock" || resTag == "Iron")) return true;
        if (myTag == "Gatherer" && resTag == "Fruit") return true;
        
        return false;
    }

    private void FindNextResourceOnScreen(string targetTag, GameObject ignoredObj)
    {
        GameObject[] allResources = GameObject.FindGameObjectsWithTag(targetTag);
        Transform bestTarget = null;
        float closestDist = Mathf.Infinity;
        Camera cam = Camera.main;

        foreach(GameObject res in allResources)
        {
            if(res == ignoredObj) continue;

            Vector3 viewportPos = cam.WorldToViewportPoint(res.transform.position);
            bool isVisible = viewportPos.x >= 0 && viewportPos.x <= 1 &&
                             viewportPos.y >= 0 && viewportPos.y <= 1 &&
                             viewportPos.z > 0;

            if(isVisible)
            {
                float dist = Vector3.Distance(transform.position, res.transform.position);
                if(dist < closestDist)
                {
                    closestDist = dist;
                    bestTarget = res.transform;
                }
            }
        }

        if(bestTarget != null)
        {
            Debug.Log($"Ayni turden yeni kaynak bulundu: {bestTarget.name}. Otomatik gidiliyor.");
            SetGatherTarget(bestTarget);
        }
        else
        {
            Debug.Log($"Ekranda baska {targetTag} bulunamadi. Isci beklemeye gecti.");
        }
    }
}
