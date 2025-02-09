using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eParticleType
{
    Destroy2,
}

public class ParticleMng : MonoBehaviour
{
    public static ParticleMng Instance;






    [Header("Particle Prefab")]
    [SerializeField] private Transform Destroy2Particle;





    private void Awake()
    {
        SELF_INS();
    }

    private void SELF_INS()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void CreateParticle(eParticleType ParticleType, Vector2 Position)
    {
        if (ParticleType == eParticleType.Destroy2)
        {
            Transform New = Instantiate(Destroy2Particle, Position, Quaternion.identity, null);
            ParticleSystem particleSystem = New.GetComponent<ParticleSystem>();
            particleSystem.Play();


        }
    }


}
