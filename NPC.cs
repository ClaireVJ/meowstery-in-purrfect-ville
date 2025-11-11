using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCInfoSO npcInfoSO;

    public NPCInfoSO GetNPCInfoSO()
    {
        return npcInfoSO;
    }
}
