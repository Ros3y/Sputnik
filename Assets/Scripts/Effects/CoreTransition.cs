using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class CoreTransition : MonoBehaviour
{
    
    public System.Action transitioned;
    public Color minColor;
    public Color maxColor;
    public Renderer[] renderers;
    public ParticleSystem[] particleSystems;
    public Light spotLight;
    private AudioSource _audioSource;
    public AudioClip coreTransitionSound;
    public bool hasTransitioned { get; private set; }

    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = coreTransitionSound;
        hasTransitioned = false;
    }
    private void TweenRenderer(Renderer renderer)
    {
        renderer.material.TweenColor(Color.cyan, 1.0f).SetDelay(4.0f);
            Tweening.To(
                getter: () => renderer.material.GetColor("_EmissionColor"),
                setter: color => renderer.material.SetColor("_EmissionColor", color),
                endValue: Color.cyan,
                duration: 1.0f).SetDelay(1.5f);
    }


    private void setParticleColors()
    {
        Gradient gradient  = new Gradient();
        gradient.colorKeys = new GradientColorKey[2] {new GradientColorKey(minColor, 0.0f), new GradientColorKey(maxColor, 1.0f)};
        for(int i = 0; i < particleSystems.Length; i++)
        {
            ParticleSystem.MainModule main = this.particleSystems[i].main;
            main.startColor = new ParticleSystem.MinMaxGradient(gradient);
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grenade")
        {
            CubeProjection[] cubes = GetComponentsInChildren<CubeProjection>();
            WireGroup[] wires = FindObjectsOfType<WireGroup>();
            
            this.transitioned.Invoke();

            for(int i = 0; i < cubes.Length; i ++)
            {
                cubes[i].enabled = true;
            }
            setParticleColors();

            for(int i = 0; i < renderers.Length; i++)
            {
                TweenRenderer(renderers[i]); 
            }
            spotLight.TweenColor(Color.cyan, 1.0f).SetDelay(2.0f);        
            this.hasTransitioned = true;
        }
        _audioSource.PlayOneShot(this.coreTransitionSound);
    }

   
}
