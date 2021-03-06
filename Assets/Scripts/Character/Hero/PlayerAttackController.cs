﻿using System.Collections;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public float AttackInterval;

    private PlayerControl player;
    private CharacterCommon characterCommon;
    private CharacterInformation playerInfor;

    private bool monsterInRange;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogWarning("PlayerAttackController OnTriggerEnter2D: " + other.name + ", parent: " + ((other.transform.parent == null) ? "root" : other.transform.parent.name));

        if (other.tag.Equals("Monster"))
        {
            StartCoroutine("CheckAttack", other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.LogWarning("PlayerAttackController OnTriggerExit2D: " + other.name);

        if (other.tag.Equals("Monster"))
        {
            StopCoroutine("CheckAttack");
        }
    }

    IEnumerator CheckAttack(Collider2D other)
    {
        while (true)
        {
            yield return null;

            // in case monster is dead for a while.
            if (other == null)
            {
                break;
            }

            // monster is dead but not destroyed.
            var monsterCommon = other.GetComponent<CharacterCommon>();
            if (monsterCommon.Dead)
            {
                break;
            }

            // player is not firing.
            if (!player.fire)
            {
                continue;
            }

            // not in the same side and not overlap borders.
            var facingRight = (other.transform.position.x > player.transform.position.x);
            var playerBorder = player.GetComponent<Collider2D>().bounds;
            var monsterBorder = monsterCommon.GetComponent<Collider2D>().bounds;
            if (characterCommon.FacingRight != facingRight && !playerBorder.Intersects(monsterBorder))
            {
                continue;
            }

			player.BoomFight = true;
            // right place to hurt monster.
            HurtMonster(other);
            yield return new WaitForSeconds(AttackInterval);
        }

		player.BoomFight = false;
    }

    void HurtMonster(Collider2D other)
    {
        // monster update.
        var monsterCommon = other.GetComponent<CharacterCommon>();
        monsterCommon.Hurt();

        // player ui update.
        var playerInfor = player.GetComponent<CharacterInformation>();
        playerInfor.Show(true);
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        characterCommon = player.GetComponent<CharacterCommon>();
        playerInfor = player.GetComponent<CharacterInformation>();
    }
}
