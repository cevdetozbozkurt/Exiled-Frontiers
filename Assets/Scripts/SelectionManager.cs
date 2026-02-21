using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public List<WorkerController> selectedWorkers = new List<WorkerController>();

    public LayerMask selectableLayer;
    public LayerMask groundLayer;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        
        inputActions.PlayerControls.Enable();

        inputActions.PlayerControls.Select.performed += OnSelectPerformed;

        inputActions.PlayerControls.Command.performed += OnCommandPerformed;
        inputActions.PlayerControls.GatherAll.performed += OnGatherAllPerformed;
    }

    private void OnDisable()
    {
        inputActions.PlayerControls.Select.performed -= OnSelectPerformed;
        inputActions.PlayerControls.Command.performed -= OnCommandPerformed;
        inputActions.PlayerControls.Disable();

        inputActions.PlayerControls.GatherAll.performed -= OnGatherAllPerformed;
        inputActions.PlayerControls.Disable();
    }

    private void OnGatherAllPerformed(InputAction.CallbackContext context)
    {
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
        {
            Vector3 centerPos = hit.point;

            
            WorkerController[] allWorkers = FindObjectsByType<WorkerController>(FindObjectsSortMode.None);

            if (allWorkers.Length == 0) return;

            
            for (int i = 0; i < allWorkers.Length; i++)
            {
                if (allWorkers[i] != null)
                {
                    Vector3 targetPos = centerPos;

                    if (allWorkers.Length > 1)
                    {
                        float angle = i * (Mathf.PI * 1f / allWorkers.Length);
                        float radius = 1.0f; 
                        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                        targetPos += offset;
                    }

                    allWorkers[i].MoveTo(targetPos);
                }
            }
            Debug.Log("All workers gathered to screen center!");
        }
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {

        Vector2 mousePos = inputActions.PlayerControls.MousePosition.ReadValue<Vector2>();
        bool isShiftPressed = inputActions.PlayerControls.MultiSelectModifier.IsPressed();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            WorkerController clickedWorker = hit.collider.GetComponentInParent<WorkerController>();

            if(clickedWorker != null)
            {

                if (!isShiftPressed)
                {
                    ClearSelection();
                }

                if (!selectedWorkers.Contains(clickedWorker))
                {
                    selectedWorkers.Add(clickedWorker);
                    clickedWorker.SetSelected(true);
                }
            }
            else
            {
                if (!isShiftPressed)
                {
                    ClearSelection();
                }
            }
        }
        else
        {
            if(!isShiftPressed)
            {
                ClearSelection();
            }
        }
    }

    private void OnCommandPerformed(InputAction.CallbackContext context)
    {
        if (selectedWorkers.Count == 0) return;

        Vector2 moursePos = inputActions.PlayerControls.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(moursePos);

        if(Physics.Raycast(ray, out RaycastHit hit, selectableLayer))
        {
            string hitTag = hit.collider.tag;
            Debug.Log($"Sag tiklandi! Vurulan Obje: {hit.collider.gameObject.name}, Tag: {hitTag}");

            if (hitTag == "Tree" || hitTag == "Rock" || hitTag == "Iron" || hitTag == "Fruit")
            {
                foreach(var worker in selectedWorkers)
                {
                    if (worker != null) worker.SetGatherTarget(hit.collider.transform);
                }
            }
            else
            {
                foreach(var worker in selectedWorkers)
                {
                    if (worker != null) worker.MoveTo(hit.point);
                }
            }
        }

    }

    private void ClearSelection()
    {
        foreach (var worker in selectedWorkers)
        {
            if(worker != null)
            {
                worker.SetSelected(false);
            }
        }
        selectedWorkers.Clear();
    }
}
