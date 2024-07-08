using UnityEngine;
using UnityEngine.InputSystem;
public class RebindManager : MonoBehaviour
{
    public InputActionReference MoveRef, JumpRef, InteractRef;
    void Start()
    {

    }

    private void OnEnable()
    {
        MoveRef.action.Disable();
        JumpRef.action.Disable();
        InteractRef.action.Disable();
    }
    void Update()
    {
        MoveRef.action.Enable();
        JumpRef.action.Enable();
        InteractRef.action.Enable();
    }
}
