using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class TableGameManager : Singleton<TableGameManager>
{
    public GameObject ScreenEnd;
    [SerializeField]
    private List<GameObject> cardPlayers;
    [SerializeField]
    private List<GameObject> cardEnemy;

    [SerializeField]
    private CardSet cardSet;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Player enemy;

    private int amountTurnToEnd = 3;
    private int currentTurn = 1;
    private TurnStep currentStep;
    private CardEffectConditionManager conditionManager;
    [SerializeField] private TypePlayer preferredTurnPlayer;

    public Player Player { get => player; set => player = value; }
    public Player Enemy { get => enemy; set => enemy = value; }
    public int AmountTurnToEnd { get => amountTurnToEnd; set => amountTurnToEnd = value; }
    public TurnStep CurrentStep { get => currentStep; set => currentStep = value; }
    public TypePlayer PreferredTurnPlayer { get => preferredTurnPlayer; set => preferredTurnPlayer = value; }

    void Start()
    {
        conditionManager = new CardEffectConditionManager();
        AssignCardsToDeck();
        StartCoroutine(ExecuteSelectCardPhase());
    }

    public IEnumerator NextStep()
    {
        CheckForEndOfGame();

        if(!ScreenEnd.activeSelf)
        {
            IncrementCurrentStep();

            yield return StartCoroutine(ExecuteCurrentStepLogic());

            if (IsAtEndOfTurn())
            {
                IncrementTurnCount();
            }
        }
    }

    public void PlayerMoveCardFromHandToGame(Card card)
    {
        player.MoveCardFromHandToGame(card);
    }

    public void PlayerMoveCardFromGameToHand(Card card)
    {
        player.MoveCardFromGameToHand(card);
    }

    public void PlayerMoveCardFromHandToPlayed()
    {
        player.MoveCardsFromInGameToPlayed();
    }

    public void AssignDeckCardToHand()
    {
        player.MoveCardsFromDeckToHand();
        enemy.MoveCardsFromDeckToHand();
    }

    public void AssignHandToGameObject()
    {
        AssignHandToGameObject(cardPlayers, player.CardsInHand);
        AssignHandToGameObject(cardEnemy, enemy.CardsInHand);
    }

    IEnumerator ExecuteCurrentStepLogic()
    {
        switch (CurrentStep)
        {

            case TurnStep.SelectCardPhase:
                yield return StartCoroutine(ExecuteSelectCardPhase());
                break;

            case TurnStep.ProcessCardTablePhase:
                yield return StartCoroutine(ExecuteProcessCardTablePhase());
                break;

            default:
                Debug.LogWarning($"Unhandled turn step: {CurrentStep}");
                break;
        }
    }

    IEnumerator ExecuteSelectCardPhase()
    {
        SoundManager.instance.PlaySoundShuffleCard();
        TablePlayerManager.instance.DefaultAllCardLocalPlayer();
        TableArenaManager.instance.DefaultEnemyCardLocalArenaBehaviour();
        TableArenaManager.instance.DefaultPlayerCardLocalArenaBehaviour();
        PlayerMoveCardFromHandToPlayed();
        EnemyMoveCardFromHandToPlayed();

        // PlayAnimations()

        AssignRandomPlayerBegin();
        AssignDeckCardToHand();
        AssignHandToGameObject();
        yield return new WaitForSeconds(0.0f);
    }

    IEnumerator ExecuteProcessCardTablePhase()
    {
        AssignEnemyCardsDeckToHand();
        TableArenaManager.instance.AddEnemyCardLocalArenaBehaviour();

        SoundManager.instance.PlaySoundCheckWin();

        yield return new WaitForSeconds(0.2f);

        SoundManager.instance.PlaySoundPublicLaugh();

        yield return new WaitForSeconds(0.5f);

        AssignPointsToCurrentPointTurn(Player);
        AssignPointsToCurrentPointTurn(Enemy);

        if (preferredTurnPlayer.Equals(TypePlayer.Player))
        {
            Card card = Player.CardsInHand.First();
            foreach (var effect in card.cardAbility.cardAbilityEffects)
            {
                conditionManager.DoCondition(effect.cardEffectCondition, card.cardAbility, Player, Enemy);
            }

            card = Enemy.CardsInHand.First();
            foreach (var effect in card.cardAbility.cardAbilityEffects)
            {
                conditionManager.DoCondition(effect.cardEffectCondition, card.cardAbility, Enemy, Player);
            }

            card = Player.CardsInHand.Last();
            foreach (var effect in card.cardAbility.cardAbilityEffects)
            {
                conditionManager.DoCondition(effect.cardEffectCondition, card.cardAbility, Player, Enemy);
            }

            card = Enemy.CardsInHand.Last();
            foreach (var effect in card.cardAbility.cardAbilityEffects)
            {
                conditionManager.DoCondition(effect.cardEffectCondition, card.cardAbility, Enemy, Player);
            }
        }
        else
        {
            Card card = Enemy.CardsInHand.First();
            foreach (var effect in card.cardAbility.cardAbilityEffects)
            {
                conditionManager.DoCondition(effect.cardEffectCondition, card.cardAbility, Enemy, Player);
            }

            card = Player.CardsInHand.First();
            foreach (var effect in card.cardAbility.cardAbilityEffects)
            {
                conditionManager.DoCondition(effect.cardEffectCondition, card.cardAbility, Player, Enemy);
            }

            card = Enemy.CardsInHand.Last();
            foreach (var effect in card.cardAbility.cardAbilityEffects)
            {
                conditionManager.DoCondition(effect.cardEffectCondition, card.cardAbility, Enemy, Player);
            }
            card = Player.CardsInHand.Last();
            foreach (var effect in card.cardAbility.cardAbilityEffects)
            {
                conditionManager.DoCondition(effect.cardEffectCondition, card.cardAbility, Player, Enemy);
            }
        }

        Player.UpdateCurrentLaughterPointer();
        Enemy.UpdateCurrentLaughterPointer();

        UIPointsOpponent.instance.SetPoints(Enemy.TotalLaughterPoints);
        UIPointsPlayer.instance.SetPoints(Player.TotalLaughterPoints);
        SoundManager.instance.PlaySoundAddPoints();

        yield return new WaitForSeconds(0.0f);
    }

    void IncrementCurrentStep()
    {
        int nextStepIndex = CalculateNextStepIndex();
        CurrentStep = (TurnStep)nextStepIndex;
    }

    int CalculateNextStepIndex()
    {
        return ((int)CurrentStep + 1) % Enum.GetValues(typeof(TurnStep)).Length;
    }

    bool IsAtEndOfTurn()
    {
        int totalSteps = Enum.GetValues(typeof(TurnStep)).Length;
        int lastStepIndex = totalSteps - 1;
        return (int)CurrentStep == lastStepIndex;
    }

    void IncrementTurnCount()
    {
        currentTurn++;
        UITurnInfo.instance.SetTurn(currentTurn);
    }

    void CheckForEndOfGame()
    {
        if (currentTurn == amountTurnToEnd + 1)
        {
            DisplayGameEndMessage();
        }
    }

    void DisplayGameEndMessage()
    {
        ScreenEnd.SetActive(true);
    }

    void RestartGame()
    {
        // Lógica para reiniciar o jogo, se necessário
    }

    void AssignCardsToDeck()
    {
        player.CardsInDeck = cardSet.GetRandomCards(8);
        enemy.CardsInDeck = cardSet.GetRandomCards(8);
    }

    void AssignHandToGameObject(List<GameObject> cardObjects, List<Card> cards)
    {
        for (int i = 0; i < cardObjects.Count; i++)
        {
            CardBehaviour cardBehaviour = cardObjects[i].GetComponent<CardBehaviour>();
            UICardNameBehaviour uICardNameBehaviour = cardObjects[i].GetComponent<UICardNameBehaviour>();
            cardBehaviour.Card = cards[i];
            uICardNameBehaviour.setCardName(cardBehaviour.Card.cardName);
        }
    }

    void AssignRandomPlayerBegin()
    {
        PreferredTurnPlayer = Random.value < 0.5f ? TypePlayer.Player : TypePlayer.Opponent;
        UIRoundInfo.instance.SetInfo(PreferredTurnPlayer.ToString());
    }

    void AssignPointsToCurrentPointTurn(Player player)
    {
        player.AssignPointsCardToCurrentPointTurn();
    }

    //I.A Enemy Card
    void AssignEnemyCardsDeckToHand()
    {
        enemy.MoveCardsFromDeckToHandIA(2);
    }

    void EnemyMoveCardFromHandToPlayed()
    {
        enemy.MoveCardsFromInGameToPlayed();
    }
}

