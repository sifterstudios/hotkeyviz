using UnityEngine;

namespace Sifter.UI.Button
{
    public class ButtonBorder : MonoBehaviour
    {
        void Start()
        {
            gameObject.SetActive(false);
        }

        void OnMouseEnter()
        {
            gameObject.SetActive(true);
        }

        void OnMouseExit()
        {
            gameObject.SetActive(false);
        }
    }
}