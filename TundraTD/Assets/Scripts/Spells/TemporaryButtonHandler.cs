using UnityEngine;

namespace Spells
{
    public class TemporaryButtonHandler : MonoBehaviour
    {
        public void OnClick()
        {
            BasicElement[] elements = new BasicElement[5];
            System.Random random = new System.Random();
            for (int i = 0; i < 5; i++)
            {
                BasicElement element = i < 3 ? BasicElement.Fire : (BasicElement)Mathf.NextPowerOfTwo(random.Next(1 << 4));
                elements[i] = element;
                Debug.Log($"Position {i}, Element: {element}");
            }
            var spell = Grimoire.TurnElementsIntoSpell(elements);
            spell.ExecuteSpell();
        }
    }
}
