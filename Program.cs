using System;
using System.Collections.Generic;

namespace DeckOfCards
{
  class Program
  {
    static void Main(string[] args)
    {
      var newGame = true;

      while (newGame)
      {
        //Welcome the user
        Console.WriteLine("");
        Console.WriteLine("Welcome to War! the card game.");
        Console.WriteLine("");

        //Create a list for the "deck of cards" and "shuffled deck of cards"
        var cardDeck = new List<string>();
        var shuffledCardDeck = new List<string>();
        var playerOneHand = new List<string>();
        var playerTwoHand = new List<string>();

        //Variable used to kick code out of game loop
        var continuePlaying = true;

        //Initialize the "deck of cards" with all 52 possible cards (named like "Ace of Spades")
        cardDeck = generateDeck();

        //Randomize the order of "deck of cards" into "shuffled deck of cards"
        //Using Fisher-Yates Shuffle
        while (cardDeck.Count > 0)
        {
          Random rnd = new Random();

          var numberSelected = rnd.Next(cardDeck.Count);

          var cardSelected = cardDeck[numberSelected];

          shuffledCardDeck.Add(cardSelected);
          cardDeck.Remove(cardSelected);
        }

        //Deal to both players
        while (shuffledCardDeck.Count > 0)
        {
          if (shuffledCardDeck.Count % 2 == 0)
          {
            playerOneHand.Add(shuffledCardDeck[0]);
            shuffledCardDeck.RemoveAt(0);
          }
          else
          {
            playerTwoHand.Add(shuffledCardDeck[0]);
            shuffledCardDeck.RemoveAt(0);
          }
        }

        //Display that both players have been dealt hands
        Console.WriteLine("Player 1 and Player 2 have been dealt 26 cards each.");
        Console.WriteLine("");

        //Begin Game with playerOneHand and playerTwoHand
        while (continuePlaying)
        {
          //Save Player Ones card and remove it from the top of his hand
          var playerOneCard = playerOneHand[0];
          playerOneHand.RemoveAt(0);

          //Save Player Twos card and remove it from the top of his hand
          var playerTwoCard = playerTwoHand[0];
          playerTwoHand.RemoveAt(0);

          //Display the card each person played
          Console.WriteLine($"Player 1 plays a(n) {playerOneCard}");
          Console.WriteLine($"Player 2 plays a(n) {playerTwoCard}");
          Console.WriteLine("");

          //Determine the winner of the hand
          var winner = compareCards(playerOneCard, playerTwoCard);

          //If Player One wins, add both cards to Player Ones hand (at the bottom)
          if (winner == "Player 1")
          {
            Console.WriteLine("Player 1 Wins!");
            Console.WriteLine("");

            playerOneHand.Add(playerOneCard);
            playerOneHand.Add(playerTwoCard);
          }
          //If Player Two wins, add both cards to Player Twos hand (at the bottom)
          else if (winner == "Player 2")
          {
            Console.WriteLine("Player 2 Wins!");
            Console.WriteLine("");

            playerTwoHand.Add(playerOneCard);
            playerTwoHand.Add(playerTwoCard);
          }
          //WAR
          else if (winner == "War")
          {
            //Display that a War scenario was encountered
            Console.WriteLine("War!");
            Console.WriteLine("");

            //Create a list for each players stack of cards to be played
            var playerOneWarCards = new List<string>();
            var playerTwoWarCards = new List<string>();

            //Initialize variable used to denote a winner of the War has been found
            var noWinner = true;

            while (noWinner)
            {
              //Add top 2 cards of Player Ones hand to their list and remove them from their hand
              playerOneWarCards.Add(playerOneHand[0]);
              playerOneHand.RemoveAt(0);
              playerOneWarCards.Add(playerOneHand[0]);
              playerOneHand.RemoveAt(0);

              //Add top 2 cards of Player Twos hand to their list and remove them from their hand
              playerTwoWarCards.Add(playerTwoHand[0]);
              playerTwoHand.RemoveAt(0);
              playerTwoWarCards.Add(playerTwoHand[0]);
              playerTwoHand.RemoveAt(0);

              //Display the card each person played
              Console.WriteLine($"Player 1 has played a(n) {playerOneWarCards[playerOneWarCards.Count - 1]}");
              Console.WriteLine($"Player 2 has played a(n) {playerTwoWarCards[playerTwoWarCards.Count - 1]}");
              Console.WriteLine("");

              //Determine the winner
              winner = compareCards(playerOneWarCards[playerOneWarCards.Count - 1], playerTwoWarCards[playerOneWarCards.Count - 1]);

              //If Player One wins, add all cards in play to Player Ones hand (at the bottom)
              if (winner == "Player 1")
              {
                noWinner = false;

                playerOneHand.Add(playerOneCard);
                playerOneHand.Add(playerTwoCard);

                for (var i = 0; i < playerOneWarCards.Count; i++)
                {
                  playerOneHand.Add(playerOneWarCards[i]);
                }

                for (var j = 0; j < playerTwoWarCards.Count; j++)
                {
                  playerOneHand.Add(playerTwoWarCards[j]);
                }

                Console.WriteLine("Player 1 Wins!");
                Console.WriteLine("");
              }
              //If Player Two wins, add all cards in play to Player Twos hand (at the bottom)
              else if (winner == "Player 2")
              {
                noWinner = false;

                playerTwoHand.Add(playerOneCard);
                playerTwoHand.Add(playerTwoCard);

                for (var i = 0; i < playerOneWarCards.Count; i++)
                {
                  playerTwoHand.Add(playerOneWarCards[i]);
                }

                for (var j = 0; j < playerTwoWarCards.Count; j++)
                {
                  playerTwoHand.Add(playerTwoWarCards[j]);
                }

                Console.WriteLine("Player 2 Wins!");
                Console.WriteLine("");
              }
              //No winner (another War scenario), continue the "War" loop
              else
              {
                Console.WriteLine("War!");
                Console.WriteLine("");
              }
            }
          }

          //Prompt user what they want to do next as long as bot players still have cards
          if (playerOneHand.Count > 0 && playerTwoHand.Count > 0)
          {
            var nextAction = "d";

            while (nextAction == "d")
            {
              Console.WriteLine("What do you want to do next?");
              Console.WriteLine("(P)lay next hand.");
              Console.WriteLine("(D)isplay Player 1 and Player 2 card counts.");
              Console.WriteLine("(Q)uit the game.");

              nextAction = Console.ReadLine().ToLower();

              while (nextAction != "p" && nextAction != "d" && nextAction != "q")
              {
                Console.WriteLine("Please enter a valid response. You can only reply Play(P), Display(D) or Quit(Q).");

                nextAction = Console.ReadLine().ToLower();
              }

              if (nextAction == "q")
              {
                continuePlaying = false;

                newGame = false;
              }
              else if (nextAction == "d")
              {
                Console.WriteLine("");
                Console.WriteLine($"Player 1 has {playerOneHand.Count} cards.");
                Console.WriteLine($"Player 2 has {playerTwoHand.Count} cards.");
                Console.WriteLine("");
              }
            }
          }
          //If Player Two has 0 cards left, Player One wins
          else if (playerTwoHand.Count == 0)
          {
            Console.WriteLine("");
            Console.WriteLine("Player 1 has won the game!");
            Console.WriteLine("");

            continuePlaying = false;

            newGame = askToPlayAgain();
          }
          //If Player One has 0 cards left, Player Two wins
          else if (playerOneHand.Count == 0)
          {
            Console.WriteLine("");
            Console.WriteLine("Player 2 has won the game!");
            Console.WriteLine("");

            continuePlaying = false;

            newGame = askToPlayAgain();
          }
        }
      }

      //Sign off Message
      Console.WriteLine("Thank you for playing! Goodbye!");
    }

    static List<string> generateDeck()
    {
      //Initialize List for storing combinations of cards
      var listOfCards = new List<string>();

      //Initialize 2 Arrays for static lists of of Suits and Ranks
      var cardSuits = new string[] { "Spades", "Clovers", "Diamonds", "Hearts" };
      var cardRanks = new string[] {"Ace", "Two", "Three", "Four", "Five", "Six", "Seven",
                                    "Eight", "Nine", "Ten", "Jack", "Queen", "King"};

      //Generate deck of cards
      for (var i = 0; i < cardSuits.Length; i++)
      {
        for (var j = 0; j < cardRanks.Length; j++)
        {
          listOfCards.Add($"{cardRanks[j]} of {cardSuits[i]}");
        }
      }

      //Return Deck of Cards
      return listOfCards;
    }

    static Boolean askToPlayAgain()
    {
      //Ask the user if they would like to re-shuffled deck and re-deal for new game
      Console.WriteLine("Would you like to re-shuffle and re-deal for a new game? Yes(Y) or No(N).");

      //Read user input
      var playAgain = Console.ReadLine().ToLower();

      //Validate for "y" and "n" only
      while (playAgain != "y" && playAgain != "n")
      {
        Console.WriteLine("Please enter a valid response. You can only reply Yes(Y) or No(N).");

        playAgain = Console.ReadLine().ToLower();
      }

      //Play again
      if (playAgain == "y")
      {
        return true;
      }
      //Exit program
      else if (playAgain == "n")
      {
        return false;
      }
      //Catch all
      else
      {
        return false;
      }
    }

    static string compareCards(string playerOneInput, string playerTwoInput)
    {
      //Compare the two cards to determine if Player One wins, Player Two wins or there is a draw
      var playerOneRank = playerOneInput.Substring(0, playerOneInput.IndexOf(" "));
      var playerTwoRank = playerTwoInput.Substring(0, playerTwoInput.IndexOf(" "));

      //Initialize variables to assign numerical value to the rank of their card
      var playerOneResult = 0;
      var playerTwoResult = 0;

      //Define static array of card ranks (use index of array to return int value of card)
      var cardRanking = new string[] {"Two", "Three", "Four", "Five", "Six", "Seven",
                                      "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace"};

      //Loop through the cardRanking array to find int value for each player
      for (var i = 0; i < cardRanking.Length; i++)
      {
        if (playerOneRank == cardRanking[i])
        {
          playerOneResult = i;
        }

        if (playerTwoRank == cardRanking[i])
        {
          playerTwoResult = i;
        }
      }

      //If Player Ones number is higher than Player Twos, Player One had a higher value card
      if (playerOneResult > playerTwoResult)
      {
        return "Player 1";
      }
      //If Player Twos number is higher than Player Ones, Player Two had a higher value card
      else if (playerTwoResult > playerOneResult)
      {
        return "Player 2";
      }
      //Both players have the same card rank so a War is started
      else
      {
        return "War";
      }
    }
  }
}
