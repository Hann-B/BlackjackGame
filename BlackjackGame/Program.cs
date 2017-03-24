using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Welcome Display
            welcome();
           
            //making of cards
            List<Card> theDeck = orderedDeck();
            
            //Shuffle the cards
            List<Card> newDeck = ShuffleDeck(theDeck);

            //Dealing cards 
            List<Card> pCardsOnTable = DealingCardsToPlayer(newDeck);
            List<Card> dCardsOnTable = DealingCardsToDealer(newDeck);

            //Displaying cards in hand
            DisplayPlayerCards(pCardsOnTable);
            DisplayDealerCards(dCardsOnTable);

            //suit rank for identical card value scenario

            //Counting cards in players hand
            double pHandTotal = PCardSum(pCardsOnTable);
            PCardSumLogic(pHandTotal);

            //counting cards in dealers hand
            //!facedown card 2

            //second round; hit/stand or bust/blackjack

            //hit logic

            //stand logic
        }
        private static void welcome()
        {
            Console.WriteLine("Welcome! The name of the game is Blackjack. I will be your dealer, Let's get started.");
        }

        static List<Card> orderedDeck()
        {
            List<Card> deck = new List<Card>();
            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                foreach (Suit s in Enum.GetValues(typeof(Suit)))
                {
                    deck.Add(new Card(s, r));
                }
            }
            return deck;
            }

        static List<Card> ShuffleDeck(List<Card> deck)
        {
            var randomDeck = deck.OrderBy(x => Guid.NewGuid()).ToList();
            return randomDeck;
        }

        static List<Card> DealingCardsToPlayer(List<Card>randomDeck)
        {
            var playerHand = new List<Card>();

         for (int counter = 0; counter < 4; counter++)
         {
            if (counter % 2 == 0)
             {
                 playerHand.Add(randomDeck[counter]);
             }
         }
         return playerHand;
        }
        static List<Card> DealingCardsToDealer(List<Card> randomDeck)
        {
            var dealerHand = new List<Card>();

            for (int counter = 0; counter < 4; counter++)
            {
                if (counter % 2 == 1)
                {
                    dealerHand.Add(randomDeck[counter]);
                }
            }
            return dealerHand;
        }

        static void DisplayPlayerCards(List<Card>playerHand)
        {
            Console.WriteLine($"Your cards are: ");

        foreach (var pCards in playerHand)
        {
            Console.WriteLine(pCards);
        }
        }

        static void DisplayDealerCards(List<Card>dealerHand)
{
    Console.WriteLine($"Dealers cards are: ");
    foreach (var dCards in dealerHand)
    {
        Console.WriteLine(dCards);
    }
}

        static double PCardSum(List<Card>playerHand)
        {
            List<int> cardValue = new List<int>();
            
                foreach (var pCard in playerHand)
                {
                    cardValue.Add(pCard.GetCardValue());
                }
            double total = cardValue.Sum();
            return total;
        }

        static void PCardSumLogic(double total)
        {
            if (total == 21)
            {
                Console.WriteLine($"{total}");
                Console.WriteLine("BLACKJACK!");
                Console.WriteLine("You Win!");
            }
            else if (total < 21)
            {
                Console.WriteLine($"{total}, would you like to hit of stand?");
            }
            else
            {
                Console.WriteLine($"{total}");
                Console.WriteLine("BUUUUST");
                Console.WriteLine("Better Luck Next Time");
            }
        }
    }
}