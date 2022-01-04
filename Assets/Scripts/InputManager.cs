using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
public class InputManager : MonoBehaviour
{
    public SteamVR_Action_Boolean plantAction;

        public Hand hand;

        public Animator scoreboardAnimator;
        private bool scoreboardUp = false;

        void Update() {
            if (Input.GetKeyDown("p"))
            {
                Debug.Log("p key was pressed");
                Scoreboard.Popup();
            }    
        }

        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (plantAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned");
                return;
            }

            plantAction.AddOnChangeListener(OnPlantActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (plantAction != null)
                plantAction.RemoveOnChangeListener(OnPlantActionChange, hand.handType);
        }

        private void OnPlantActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            Debug.Log("Scoreboard Action Triggered");

            if (newValue)
            {
                scoreboardUp = !scoreboardUp;
                Scoreboard.Popup();
            }
        }
}
}
