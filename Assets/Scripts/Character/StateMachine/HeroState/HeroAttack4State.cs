using UnityEngine;

public class HeroAttack4State : MonoBehaviour
{
	public HeroAttackMove AttackMove;

	private PlayerControl player;
	private CharacterAttackChecker checker;

    void OnEnable()
    {
        player.LastAttack = true;

		checker.Check();
	}

	void OnDisable()
	{
		player.LastAttack = false;
	}

	void FixedUpdate()
	{
		AttackMove.MoveUpdate();
	}

	public void OnAttack4Start()
	{
		AttackMove.MoveStart();
	}

	public void OnAttack4Stop()
	{
		AttackMove.MoveStop();
	}
	
	void Awake()
	{
        player = transform.parent.parent.GetComponent<PlayerControl>();
        checker = player.transform.Find("Sensors/NormalAttack").GetComponent<CharacterAttackChecker>();
	}
}