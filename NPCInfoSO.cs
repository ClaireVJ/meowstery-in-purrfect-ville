using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/New NPC")]
public class NPCInfoSO : ScriptableObject
{
    public string npcName;
    public Sprite npcSprite;
    public bool hasTask;
    public bool hasDialogue;
}
