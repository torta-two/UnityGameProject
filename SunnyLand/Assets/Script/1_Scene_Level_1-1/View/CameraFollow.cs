using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public float maxX = 32f;
    public float minX = 0f;

    private Transform player;
    private AudioManager audioManager;
    public AudioSource audioSource;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>().transform;
        audioManager = GameObject.FindWithTag("Ctrl").GetComponent<AudioManager>();
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
        audioManager.Play(audioManager.BGM, audioSource);        
    }

    private void FixedUpdate()
    {
        Vector3 temp = transform.position;

        if (player.position.x <= maxX && player.position.x >= minX)
        {
            //temp.x = Mathf.Lerp(temp.x, player.position.x, Mathf.SmoothStep(0, cameraSpeed,Time.deltaTime * cameraSpeed));
            temp.x = player.position.x;
        }
        
        temp.y = Mathf.Lerp(temp.y, player.position.y, Time.deltaTime * cameraSpeed);
        transform.position = temp;
    }
}
