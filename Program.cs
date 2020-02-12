using System;
using System.Collections.Generic;

namespace DeckOfCards
{
  class Program
  {
    static void Main(string[] args)
    {
      //Create a list for the "deck of cards" and "shuffled deck of cards"
      var cardDeck = new List<string>();
      var shuffledCardDeck = new List<string>();
      var playerOneHand = new List<string>();
      var playerTwoHand = new List<string>();

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

      //Loop through to display (up to) all cards in the deck
      var drawNextCard = true;

      while (drawNextCard)
      {
        //Display top card of "shuffled deck of cards"
        if (shuffledCardDeck.Count > 0)
        {
          Console.WriteLine($"{shuffledCardDeck[0]} is the top card of the deck.");

          //Remove top card from deck and add it to player hand
          playerOneHand.Add(shuffledCardDeck[0]);
          shuffledCardDeck.RemoveAt(0);

          //Ask if the user wants to see the next card, or quit program
          Console.WriteLine("Would you like to see the (NEXT) card in the deck, or (QUIT) the program?");

          var nextAction = Console.ReadLine().ToLower();

          //Verify user input
          while (nextAction != "next" && nextAction != "quit")
          {
            Console.WriteLine("Please enter a valid selection. Enter (NEXT) or (QUIT) to continue.");

            nextAction = Console.ReadLine().ToLower();
          }

          //Exit while loop if user picks quit
          if (nextAction == "quit")
          {
            drawNextCard = false;
          }
        }
        //Else if no cards in the deck
        else
        {
          Console.WriteLine("There are no more cards left in the deck. Program will end. Thank you.");

          drawNextCard = false;
        }
      }

      //Sign off Message
      Console.WriteLine("Have a great day! Goodbye!");
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
  }
}
