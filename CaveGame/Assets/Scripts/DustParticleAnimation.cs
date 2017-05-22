using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleAnimation : MonoBehaviour {
    private ParticleSystem ps;
    public ParticleSystemAnimationType animType;
    public int numTilesX, numTilesY;
    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
        var ts = ps.textureSheetAnimation;
        ts.enabled = true;
        ts.animation = animType;
        ts.numTilesX = numTilesX;
        ts.numTilesY = numTilesY;
    }

    public void EnableDust(bool enable)
    {
        ps.enableEmission = enable;
    }
}
