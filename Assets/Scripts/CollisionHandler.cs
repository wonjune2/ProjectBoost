using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1.0f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip succesSound;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            isTransitioning = !isTransitioning;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning) return;

        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                isTransitioning = true;
                break;
            default:
                StartCrashSequence();
                isTransitioning = true;
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        deathParticles.Play();
        GetComponent<Movement>().enabled = false;
        
        Invoke("ReloadLevel", delay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(succesSound);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        
        Invoke("LoadNextLevel", delay);
    }

    void ReloadLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    void LoadNextLevel()
    {
        int nextScebeIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextScebeIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextScebeIndex = 0;
        }
        SceneManager.LoadScene(nextScebeIndex);
    }
} 
