using UnityEditor;
using UnityEngine;

namespace RPGPrototype.Helpers.Gizmos
{
    public class CircleAroundMeleeArcs : MonoBehaviour
    {
        public float swingSize = .5f;
        public bool drawArcs = true;

        void OnDrawGizmosSelected()
        {
            var player = Player.PlayerController.Instance;
            
            Combat.MeleeArc[] objs = (Combat.MeleeArc[])FindObjectsOfType(typeof(Combat.MeleeArc));
            foreach (Combat.MeleeArc arc in objs)
            {
                if (!drawArcs) return;

                // draw the circle
                Handles.color = Color.green;
                Handles.DrawWireDisc((Vector2)arc.transform.position + player.mouseDirection, Vector3.back, swingSize);
            }
        }
    }

}
