using UnityEngine;

namespace City
{
    /// <summary>
    /// Responsible for empty tower slots' behaviour
    /// </summary>
    public class EmptyTowerSlot : MonoBehaviour
    {
        private void OnMouseDown()
        {
            // Show building menu
            print($"{name} was clicked");
        }
    }
}