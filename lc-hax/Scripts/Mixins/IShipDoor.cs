using UnityEngine;
using Hax;

public interface IShipDoor { }

public static class IShipDoorMixin {
    static InteractTrigger? GetAnimationInteractTrigger(this GameObject gameObject, string animation) =>
        gameObject
            .GetComponentsInChildren<AnimatedObjectTrigger>()
            .First(trigger => trigger.animationString == animation)?
            .GetComponentInParent<InteractTrigger>();

    public static void SetShipDoorState(this IShipDoor _, bool closed) =>
        Helper.FindObject<HangarShipDoor>()?
              .gameObject.GetAnimationInteractTrigger(closed ? "CloseDoor" : "OpenDoor")?
              .onInteract.Invoke(Helper.LocalPlayer);
}
