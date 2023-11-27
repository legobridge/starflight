using UnityEngine;
using UnityEngine.UIElements;

public class BulletBehavior : MonoBehaviour

{
    public GameObject Explosion1Prefab;
    //public AudioClip ExplosionClip;
    //public AudioSource SoundSource;
    private void OnTriggerEnter(Collider other)
    {
        // TODO: sounds and effects
       
        GameObject explosion = Instantiate(Explosion1Prefab, transform.position, Quaternion.identity, transform.parent);
        //SoundSource.enabled = true;
        //SoundSource.PlayOneShot(ExplosionClip, 2f);
        Destroy(explosion, 5f);
        Destroy(gameObject);
    }
}
