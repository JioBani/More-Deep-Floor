using System;
using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Common.TimerSystem;
using LNK.MoreDeepFloor.Data.Defender.States;
using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.Entity.Defenders;
using LNK.MoreDeepFloor.InGame.TraitSystem;
using UnityEngine;

/*
public delegate void TestEventHandler(ActionManager actionManager);

public class Actions
{
    public TestEventHandler action;

    public Actions(TestEventHandler _action)
    {
        action = _action;
    }
    
}

public class Routine : MonoBehaviour
{
    public ActionManager actionManager;

    public TestEventHandler eventAction;
    
    private static Actions testAction;
    private void Awake()
    {
        testAction = new Actions((_actionManager) =>
        {
            Debug.Log(_actionManager.number);
        });
    }

    private void Start()
    {
        StartCoroutine(ActionRoutine());
    }

    IEnumerator ActionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            testAction.action?.Invoke(actionManager);
        }
    }
    
    
    private static DefenderStateActionInfo Effect_Gladiator = new DefenderStateActionInfo(
        /* #. Id #1# DefenderStateId.Effect_Gladiator,
        /* #. Type #1# DefenderStateType.Immediately,
        /* #. On #1#(Defender caster, Monster target) =>
        {
            int level = caster.GetComponent<TraitController>().job.synergyLevel;
            int value = 1 + level * 2;
            StatusBuff buff = caster.status.AddAttackSpeedBuff(caster.status.attackSpeed.currentValue * value, "GladiatorEffect");
            Debug.Log($"Effect_Gladiator : Lv {level}");
            TimerManager.instance.LateAction(3f, () =>
            {
                caster.status.RemoveAttackSpeedBuff(buff);
                caster.GetComponent<DefenderState>().RemoveState(DefenderStateId.Effect_Gladiator);
            });
        },
        /* Active #1#null,
        /* Active #1#null
    );
}
*/
