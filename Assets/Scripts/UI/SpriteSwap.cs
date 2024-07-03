using UnityEngine;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour
{
    [SerializeField] private SpriteState spriteState;
    [SerializeField] private Button button;
    public void SpriteChange()
    {
        button.spriteState = spriteState;
    }
}
