using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapeSettings : MonoBehaviour {

    public float planetRadius = 1;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled = true;
        public bool useFirstLayerAsMask;
        public NoiseSettings noiseSettings;
    }
}
