using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireConverter : MonoBehaviour
{
    public Pulse pulseNotCorrupt;
    private void Start()
    {
        Invoke(nameof(Convert), 2.0f);
    }
    private void Convert()
    {
        Wire[] wires = this.GetComponentsInChildren<Wire>();

        for(int i = 0; i < wires.Length; i++)
        {
            wires[i].pulsePrefab = pulseNotCorrupt;     
        }
    }
}
