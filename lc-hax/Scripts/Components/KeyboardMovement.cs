using UnityEngine;
using UnityEngine.InputSystem;

namespace Hax;

public class KeyboardMovement : MonoBehaviour {
    const float baseSpeed = 20;
    float SprintMultiplier { get; set; } = 1;

    void Update() {
        if (Helper.LocalPlayer?.isTypingChat is true) return;

        Vector3 direction = new(
            Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue(),
            Keyboard.current.spaceKey.ReadValue() - Keyboard.current.ctrlKey.ReadValue(),
            Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue()
        );

        this.UpdateSprintMultiplier(Keyboard.current);
        this.Move(direction);
    }

    void UpdateSprintMultiplier(Keyboard keyboard) {
        this.SprintMultiplier = keyboard.shiftKey.IsPressed() ? Mathf.Min(this.SprintMultiplier + (5 * Time.deltaTime), 5) : 1;
    }

    void Move(Vector3 direction) {
        Vector3 translatedDirection =
            (this.transform.right * direction.x) +
            (this.transform.up * direction.y) +
            (this.transform.forward * direction.z);

        this.transform.position += translatedDirection * Time.deltaTime * baseSpeed * this.SprintMultiplier;
    }
}
