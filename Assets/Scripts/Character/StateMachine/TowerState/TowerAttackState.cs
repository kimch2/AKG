﻿using UnityEngine;

public class TowerAttackState : MonoBehaviour
{
    public PowerEffectController PowerEffectController;
    public GameObject AttackPrefab;

    public float ShootBeginTime;
    public float ShootInterval;

    private PlayerControl player;
    private CharacterCommon monsterCommon;

    private Transform attackLocation;
    private AttackEffectController attackEffectController;
    private Animator powerAnimator;

    /// <summary>
    /// Attack right place occurs.
    /// </summary>
    /// <remarks>Refers to attack animation right place.</remarks>
    public void OnShoot(GameObject go)
    {
        GenerateEffect();

        attackEffectController.FacingRight = monsterCommon.FacingRight;
        attackEffectController.Play();
    }

    private void GenerateEffect()
    {
        var attackObject = Instantiate(AttackPrefab, attackLocation.position, attackLocation.rotation) as GameObject;
        attackEffectController = attackObject.GetComponent<AttackEffectController>();
        attackEffectController.Owner = gameObject;

        //attackObject.transform.parent = attackLocation;
        attackObject.transform.localScale =
            new Vector3(
                monsterCommon.FacingRight ? attackObject.transform.localScale.x : -attackObject.transform.localScale.x,
                attackObject.transform.localScale.y, attackObject.transform.localScale.z);
    }

    void PrepareToShoot()
    {
		powerAnimator.SetTrigger("Power");
    }

    void OnEnable()
    {
		InvokeRepeating("PrepareToShoot", ShootBeginTime, ShootInterval);
    }

    void OnDisable()
    {
        CancelInvoke("PrepareToShoot");
    }

	void FixedUpdate()
	{	
		if (monsterCommon.FacingRight && player.transform.position.x < monsterCommon.transform.position.x)
		{
			monsterCommon.Flip();
		}
		
		if (!monsterCommon.FacingRight && player.transform.position.x > monsterCommon.transform.position.x)
		{
			monsterCommon.Flip();
		}
	}

    void Awake()
    {
        var root = transform.parent.parent;

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        monsterCommon = root.GetComponent<CharacterCommon>();
        attackLocation = root.Find("Effect/Attack");

        PowerEffectController.OnShoot += OnShoot;
        powerAnimator = PowerEffectController.GetComponent<Animator>();
    }
}