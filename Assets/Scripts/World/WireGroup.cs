using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireGroup : MonoBehaviour
{
    public Pulse pulseNotCorrupt;
    public float EmitInterval;
    public float pulseTravelTime;
    private Wire[] _wires;
    
    
    private void Awake()
    {
        _wires = this.GetComponentsInChildren<Wire>();
    }
    private void OnValidate()
    {
        _wires = this.GetComponentsInChildren<Wire>();
        
        for(int i = 0; i < _wires.Length; i++)
        {
            _wires[i].pulseEmitInterval = this.EmitInterval;
            _wires[i].pulseTravelTime = this.pulseTravelTime;
        }   
    }
    public void Delay()
    {
        Invoke(nameof(Convert), this.EmitInterval);
    }
    public void Convert()
    {
        for(int i = 0; i < _wires.Length; i++)
        {
            _wires[i].pulsePrefab = pulseNotCorrupt;     
        }
    }
}
