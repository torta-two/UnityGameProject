using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPanel : BasePanel
{
    private void Update()
    {
        transform.position = FindObjectOfType<PlayerControl>().transform.position;
    }
}
