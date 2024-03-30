using UnityEngine;

public class PortalAnimationEventHandler : MonoBehaviour
{
    public ActivatePortal portalController;

    // This method is called from the animation event
    public void HandleAnimationComplete()
    {
        Debug.Log("Animation complete");
        portalController.OnAnimationComplete();
    }
}