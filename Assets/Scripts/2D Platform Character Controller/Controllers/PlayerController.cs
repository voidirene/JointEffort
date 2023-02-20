using UnityEngine;

namespace Shinjingi
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        //edited to work with the unity input manager, in order to support a second player
        [SerializeField] private string moveAxisName = "Horizontal";
        [SerializeField] private string jumpAxisName = "Jump";

        public override bool RetrieveJumpInput()
        {
            return Input.GetButtonDown(jumpAxisName);
        }

        public override float RetrieveMoveInput()
        {
            return Input.GetAxisRaw(moveAxisName);
        }
    }
}
