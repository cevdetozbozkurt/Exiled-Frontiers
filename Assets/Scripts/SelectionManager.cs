using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public List<WorkerController> selectedWorkers = new List<WorkerController>();

    public LayerMask selectableLayer;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        
        inputActions.PlayerControls.Enable();

        inputActions.PlayerControls.Select.performed += OnSelectPerformed;

        inputActions.PlayerControls.Command.performed += OnCommandPerformed;
    }

    private void OnDisable()
    {
        inputActions.PlayerControls.Select.performed -= OnSelectPerformed;
        inputActions.PlayerControls.Command.performed -= OnCommandPerformed;
        inputActions.PlayerControls.Disable();
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

        if(Physics.Raycast(ray, out RaycastHit hit))
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
