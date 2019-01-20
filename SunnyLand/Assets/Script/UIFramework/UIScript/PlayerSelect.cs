using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    private Animator player;
    private Toggle toggle;

	void Start ()
    {
        toggle = GetComponent<Toggle>();
        player = GetComponentInChildren<Animator>();
	}
	

	void Update ()
    {
        player.SetBool("Play", toggle.isOn);
    }
}
