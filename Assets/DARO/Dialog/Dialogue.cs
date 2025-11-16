using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DIalogue")]
public class Dialogue : ScriptableObject
{
    public List<DialogueLine> Lines;
}