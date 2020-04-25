using System;
using System.Collections;
using System.Collections.Generic;
using Fight;
using UnityEngine;

[RequireComponent(typeof(FightVisualEffector))]
public class FightController : MonoBehaviour
{
    [SerializeField] private FightElement playerElement;
    [SerializeField] private FightElement enemyElement;
        
    [SerializeField] private SkillHolder skillHolder;
    [SerializeField] private FightLogger fightLogger;

    [SerializeField] private Animation sceneAnimation;
    
    private FightVisualEffector visualEffector;

    private Dictionary<FightState, Action> fightStateMachine = new Dictionary<FightState, Action>();
    private Dictionary<FightState, Action> fightComplete = new Dictionary<FightState, Action>();

    private Attacker playerAttacker;
    private EnemyAttacker enemyAttacker;
    private Skill lastSkill;
    
    private FightState currentState;
    private FightState nextState
    {
        get
        {
            if (currentState == FightState.EnemyEffect)
            {
                return FightState.PlayerTurn;
            }

            return currentState + 1;
        }
    }

    private DelayCallService call =>
        ServiceLocator.Get<DelayCallService>();
    
    private void Start()
    {
        sceneAnimation.Play("BossFightIntro");
        
        fightStateMachine.Add(FightState.FightStarted, OnGameStarted);
        fightStateMachine.Add(FightState.PlayerTurn, PlayerTurn);
        fightStateMachine.Add(FightState.PlayerEffect, PlayerEffect);
        fightStateMachine.Add(FightState.EnemyTurn, EnemyTurn);
        fightStateMachine.Add(FightState.EnemyEffect, EnemyEffect);

        fightComplete.Add(FightState.Win, Win);
        fightComplete.Add(FightState.Lose, Lose);
        fightComplete.Add(FightState.Draw, Draw);
        
        Init();
        InvokeCurrentState();
    }

    private void Init()
    {
        SetupFighters();
        
        skillHolder.Init(playerAttacker);
        skillHolder.OnUseSkill += UseSkill;
    }

    private void SetupFighters()
    {
        playerAttacker = FighterContainer.Player();
        enemyAttacker = FighterContainer.Enemy();
        
        playerElement.UpdateView(playerAttacker);
        enemyElement.UpdateView(enemyAttacker);
    }
    
    private void OnGameStarted()
    {
        skillHolder.Hide();
        fightLogger.Show();

        fightLogger.LogText($"{enemyAttacker.Name} \n would like a battle!");
        
        call.AddTick(3, MoveNextState);
    }

    private void PlayerTurn()
    {
        skillHolder.Show();
        fightLogger.Hide();
    }

    private void EnemyTurn()
    {
        EnemyUseSkill();
    }

    private void EnemyUseSkill()
    {
        UseSkill(enemyAttacker.ChoseMove());
    }

    private void PlayerEffect()
    {
        skillHolder.Hide();
        fightLogger.Show();
        
        call.AddTick(2.5f, MoveNextState);
    }

    private void EnemyEffect()
    {
        call.AddTick(2.5f, MoveNextState);
    }

    private void LogData(Attacker attacker)
    {
        if (lastSkill.SkillType == SkillType.Attack)
        {
            fightLogger.LogText($"{attacker.Name} use {lastSkill.Name} \n {InverseAttacker(attacker).Name} got {lastSkill.PositivEffect} dmg.");
        }
        else
        {
            fightLogger.LogText($"{attacker.Name} use {lastSkill.Name} \n {attacker.Name} restored {lastSkill.PositivEffect} hp.");
        }
    }

    private Attacker InverseAttacker(Attacker attacker)
    {
        if (attacker == playerAttacker)
        {
            return enemyAttacker;
        }

        return playerAttacker;
    }

    private void Lose()
    {
        
    }

    private void Win()
    {
        
    }

    private void Draw()
    {
        
    }

    public void MoveNextState()
    {
        currentState = nextState;

        InvokeCurrentState();
    }

    public void UseSkill(Skill skill)
    {
        skill.Used();

        lastSkill = skill;
        
        if (currentState == FightState.PlayerTurn)
        {
            ApplySkill(playerAttacker, enemyAttacker, skill);
            
            LogData(playerAttacker);

            if (skill.SkillType == SkillType.Attack)
            {
                enemyElement.PlayAnim(skill.SkillType);
            }
            else
            {
                playerElement.PlayAnim(skill.SkillType);
            }
        }

        if (currentState == FightState.EnemyTurn)
        {
            ApplySkill(enemyAttacker, playerAttacker, skill);   
            
            LogData(enemyAttacker);
            
            if (skill.SkillType == SkillType.Attack)
            {
                playerElement.PlayAnim(skill.SkillType);
            }
            else
            {
                enemyElement.PlayAnim(skill.SkillType);
            }
        }

        playerElement.UpdateView(playerAttacker);
        enemyElement.UpdateView(enemyAttacker);
        
        MoveNextState();
    }

    public void ApplySkill(Attacker attacker, Attacker enemy, Skill skill)
    {
        if (skill.SkillType == SkillType.Attack)
        {
            enemy.ApplyHp(skill);
        }
        else
        {
            attacker.ApplyHp(skill);
        }

    }

    private void InvokeCurrentState()
    {
        Debug.LogWarning($"State : {currentState}");

        if (EndCheck())
        {
            if (playerAttacker.Hp <= 0)
            {
                fightComplete[FightState.Lose].Invoke();
            }
            else if(enemyAttacker.Hp <=0)
            {
                fightComplete[FightState.Win].Invoke();
            }
            else
            {
                fightComplete[FightState.Draw].Invoke();
            }

            fightLogger.Hide();
            skillHolder.Hide();
            sceneAnimation.Stop();
            
            sceneAnimation["BossFightAutro"].speed = -1;
            sceneAnimation["BossFightAutro"].time = sceneAnimation["BossFightAutro"].length;
            sceneAnimation.Play("BossFightAutro");
            
            return;
        }

        fightStateMachine[currentState].Invoke();
    }

    private bool EndCheck()
    {
        if (enemyAttacker.CurrentHp <= 0)
        {
            return true;
        }
        else if (playerAttacker.CurrentHp <= 0)
        {
            return true;
        }

        return false;
    }

    public void DisableGame()
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
            
            return;
        }

        Destroy(gameObject);
    }
}