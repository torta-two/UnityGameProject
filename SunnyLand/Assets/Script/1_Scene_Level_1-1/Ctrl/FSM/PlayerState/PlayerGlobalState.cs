public class PlayerGlobalState : State<PlayerControl>
{
    private static PlayerGlobalState _instance;
    public static PlayerGlobalState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerGlobalState();
            return _instance;
        }
    }

    private PlayerGlobalState()
    {

    }
  
    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        if (!owner.isPlayHurtAnim && owner.isHurt == true)
        {
            owner.HP--;
            owner.isPlayHurtAnim = true;
            owner.isHurt = false;
            owner.StartCoroutine(owner.HurtAnim());
        }

        if(owner.isEnding)
        {

        }
    }

    public override void Exit()
    {

    }
}
