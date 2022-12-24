using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine_", menuName = "Dialogue Line")]
public class DialogueLineSO : ScriptableObject
{
    public string speaker;
    public Sprite speakerSprite;
    [TextArea] public string text;
}
