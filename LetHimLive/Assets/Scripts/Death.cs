using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public float restartDelay = 2f;
    public ParticleSystem deathEffectPrefab;
    private bool hasTriggered = false;
    public AudioSource splash;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            hasTriggered = true;
            StartCoroutine(RestartSequence(other.gameObject));
        }
    }

    private IEnumerator RestartSequence(GameObject player)
    {
        splash.Play();

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        MeshRenderer[] renderers = player.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        if (deathEffectPrefab != null)
        {
            ParticleSystem effectInstance = Instantiate(deathEffectPrefab, player.transform.position, Quaternion.identity);
            effectInstance.Play();
            Destroy(effectInstance.gameObject, effectInstance.main.duration + 0.5f);
        }

        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
