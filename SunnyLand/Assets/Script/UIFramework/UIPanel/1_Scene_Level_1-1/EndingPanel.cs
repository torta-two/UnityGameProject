public class EndingPanel : BasePanel
{
    private void Update()
    {
        transform.position = FindObjectOfType<PlayerControl>().transform.position;
    }
}
