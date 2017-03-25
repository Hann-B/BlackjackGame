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
            PCardSumLogic(pHandTotal, pCardsOnTable, newDeck);


            //dealer turn over face down card2
            Console.WriteLine($"Dealer shows: ");
            foreach (var dCard in dCardsOnTable)
            {
                Console.WriteLine(dCard);
            }

            //counting cards in dealers hand
            double dHandTotal = DCardSum(dCardsOnTable);
            DCardSumLogic(dHandTotal, newDeck, dCardsOnTable);

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

        static List<Card> DealingCardsToDealer(List<Card> randomDeck)
        {
            var dealerHand = new List<Card>();

            for (int counter = 0; counter < 4; counter++)
            {
                if (counter % 2 == 1)
                {
                    dealerHand.Add(randomDeck[counter]);
                    randomDeck.RemoveAt(counter);
                }
            }
            return dealerHand;
        }
        static List<Card> DealingCardsToPlayer(List<Card>randomDeck)
        {
            var playerHand = new List<Card>();

         for (int counter = 0; counter < 4; counter++)
         {
            if (counter % 2 == 0)
             {
                 playerHand.Add(randomDeck[counter]);
                    randomDeck.RemoveAt(counter);
             }
         }
         return playerHand;
        }

        static void DisplayDealerCards(List<Card>dealerHand)
{
    Console.WriteLine($"Dealer shows: ");
    Console.WriteLine(dealerHand[0]);
}
        static void DisplayPlayerCards(List<Card>playerHand)
        {
            Console.WriteLine($"Your cards are: ");

        foreach (var pCards in playerHand)
        {
            Console.WriteLine(pCards);
        }
        }

        static double PCardSum(List<Card>playerHand)
        {
            List<int> pCardValue = new List<int>();
            
                foreach (var pCard in playerHand)
                {
                    pCardValue.Add(pCard.GetCardValue());
                }
            double pTotal = pCardValue.Sum();
            return pTotal;
        }
        static double DCardSum(List<Card> dealerHand)
        {
            List<int> dCardValue = new List<int>();

            foreach (var dCard in dealerHand)
            {
                dCardValue.Add(dCard.GetCardValue());
            }
            double dTotal = dCardValue.Sum();
            return dTotal;
        }

        static void PCardSumLogic(double pTotal, List<Card> playerHand, List<Card>randomDeck)
        {
            if (pTotal == 21)
            {
                Console.WriteLine($"{pTotal}");
                Console.WriteLine("BLACKJACK!");
                Console.WriteLine("You Win!");
            }
            else if (pTotal<21)
            {
                Console.WriteLine($"{pTotal}, would you like to (hit) or (stand)?");
                string hitMe = Console.ReadLine();
                if (hitMe.Equals("hit"))
                {
                    playerHand.Add(randomDeck[0]);
                    randomDeck.RemoveAt(0);
                    Console.WriteLine($"{playerHand[2]}");
                    double pNewTotal = pTotal + (playerHand[2].GetCardValue()); 
                    Console.WriteLine($"{pNewTotal}");
                }
                else if (hitMe.Equals("stand"))
                {
                    //StandLogic(pTotal, dTotal);
                }
            }
            else if (pTotal>21)
            {
                Console.WriteLine($"{pTotal}");
                Console.WriteLine("BUUUUST");
                Console.WriteLine("Better Luck Next Time");
            }
        }
        static void DCardSumLogic(double dTotal, List<Card> dealerHand, List<Card> randomDeck)
        {
            if (dTotal == 21)
            {
                Console.WriteLine($"{dTotal}");
                Console.WriteLine("BLACKJACK!");
                Console.WriteLine("Would you like to play again?");
            }
            else if (dTotal >= 16)
            {
                Console.WriteLine($"{dTotal}");
                Console.WriteLine("I will stand");
            }
            else if (dTotal < 16)
            {
                Console.WriteLine($"{dTotal}");
                Console.WriteLine("I'll take another card");

                dealerHand.Add(randomDeck[0]);
                randomDeck.RemoveAt(0);
                Console.WriteLine($"{dealerHand[2]}");
                double dNewTotal = dTotal + (dealerHand[2].GetCardValue());
                Console.WriteLine($"{dNewTotal}");
            }
            else if (dTotal > 21)
            {
                Console.WriteLine($"{dTotal}");
                Console.WriteLine("That's a Bust for me, you win this round.");
            }
        }

        static void StandLogic(double pTotal, double dTotal)
        {
            if (pTotal > dTotal)
            {
                Console.WriteLine($"Your {pTotal} beats my {dTotal}.");
                Console.WriteLine("YOU WIN");
            }
            else if (pTotal < dTotal)
            {
                Console.WriteLine($"My {dTotal} beats your {pTotal}.");
                Console.WriteLine("YOU LOSE");
            }
            else if (pTotal == dTotal)
            {
                Console.WriteLine($"Your {pTotal} is equal to my {dTotal}.");
                Console.WriteLine("DRAW");
            }
        }
    }

}