using UnityEngine;

public class MapMove : MonoBehaviour
{
    private Transform map_0;
    private Transform map_1;

	void Start ()
    {
        map_0 = transform.Find("Bg (0)");
        map_1 = transform.Find("Bg (1)");
    }

    private void Update ()
    {
        Vector2 temp = transform.position;
        temp.x -= 5f * Time.deltaTime;
        transform.position = temp;

		if(transform.position.x <= -18)
        {
            map_0.localPosition = Vector2.zero;
            map_1.localPosition = new Vector2(18, 0);
            transform.position = Vector2.zero;
        }
	}
}
