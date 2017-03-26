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

            //Counting cards in players hand
            double pHandTotal = PCardSum(pCardsOnTable);
            double dHandTotal = DCardSum(dCardsOnTable);
            double dNewTotal = dHitLogic(dCardsOnTable, newDeck, dHandTotal);

            //Displaying cards in hand
            DisplayPlayerCards(pCardsOnTable, pHandTotal);
            DisplayDealerCards(dCardsOnTable);

            //player card counting logic
            PCardSumLogic(dNewTotal, DealingCardsToDealer(newDeck),dCardsOnTable, dHandTotal, pHandTotal, pCardsOnTable, newDeck);
            
            //suit rank for identical card value scenario
            //suits have number values but I do not see the input for it

            //hit logic
            double playerHandTotal2 = pHitLogic(pCardsOnTable,newDeck, pHandTotal);
            PCardSumLogic(dNewTotal, DealingCardsToDealer(newDeck), dCardsOnTable, playerHandTotal2, pHandTotal, pCardsOnTable, newDeck);
            
            DCardSumLogic(dHandTotal, newDeck, dCardsOnTable);
            
        }
        private static void welcome()
        {
            Console.WriteLine("Welcome! The name of the game is Blackjack. " +
                "I will be your dealer.");
            Console.WriteLine("First, What is your name?");
            string name = Console.ReadLine();
            Console.WriteLine("Hello {0}, Let's Get Started.", name);
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

        static void DisplayDealerCards(List<Card>dealerHand)
{
    Console.WriteLine($"Dealer shows: ");
    Console.WriteLine(dealerHand[0]);
            Console.WriteLine("");
}
        static void DisplayPlayerCards(List<Card>playerHand, double pTotal)
        {
            Console.WriteLine($"Your cards are: ");

        foreach (var pCards in playerHand)
        {
            Console.WriteLine(pCards);
        }
            Console.WriteLine($"{pTotal} is your total.");
            Console.WriteLine("");
        }

        static void DisplayDealerCards2(List<Card>dCardsOnTable, double dTotal)
        {
            Console.WriteLine("");
            Console.WriteLine("Dealer Shows:");
           
            Console.WriteLine(dCardsOnTable[0]);
            Console.WriteLine(dCardsOnTable[1]);

            //Console.WriteLine($"{dTotal} is my total");
        }

        static void PCardSumLogic(double dNewTotal, List<Card>dealerHand, List<Card>dCardsOnTable, double dTotal, double pTotal, List<Card> playerHand, List<Card>randomDeck)
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
                    pHitLogic(playerHand, randomDeck, pTotal);
                }
                else if (hitMe.Equals("stand"))
                {
                    DisplayDealerCards2(dCardsOnTable, dTotal);
                    DCardSumLogic(dTotal, dealerHand, randomDeck);
                    DCardSumLogic2(dNewTotal, dealerHand, randomDeck);
                    StandLogic(pTotal, dTotal);
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
                Console.WriteLine($"{dTotal} is my total.");
                Console.WriteLine("I'll take another card");
                dHitLogic(dealerHand, randomDeck, dTotal);
            }
            else if (dTotal > 21)
            {
                Console.WriteLine($"{dTotal}");
                Console.WriteLine("That's a Bust for me, you win this round.");
            }
        }
        
        static double pHitLogic(List<Card>playerHand, List<Card>randomDeck,double pTotal)
        {
                    playerHand.Add(randomDeck[0]);
                    randomDeck.RemoveAt(0);
                    Console.WriteLine($"{playerHand[2]}");
                    double pNewTotal = pTotal + (playerHand[2].GetCardValue());
                    Console.WriteLine($"{pNewTotal} is your new total");
            return pNewTotal;
        }
        static void PCardSumLogic2(double pNewTotal, List<Card> playerHand, List<Card> randomDeck)
        {
            if (pNewTotal == 21)
            {
                Console.WriteLine($"{pNewTotal}");
                Console.WriteLine("BLACKJACK!");
                Console.WriteLine("You Win!");
            }
            else if (pNewTotal < 21)
            {
                Console.WriteLine($"{pNewTotal}, would you like to (hit) or (stand)?");
                string hitMe = Console.ReadLine();
                if (hitMe.Equals("hit"))
                {
                    pHitLogic(playerHand, randomDeck, pNewTotal);
                }
                else if (hitMe.Equals("stand"))
                {
                    //StandLogic(pTotal, dTotal);
                }
            }
            else if (pNewTotal > 21)
            {
                Console.WriteLine($"{pNewTotal}");
                Console.WriteLine("BUUUUST");
                Console.WriteLine("Better Luck Next Time");
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

        static double dHitLogic(List<Card>dealerHand, List<Card>randomDeck, double dTotal)
        {
        dealerHand.Add(randomDeck[0]);
                randomDeck.RemoveAt(0);
                Console.WriteLine($"{dealerHand[2]}");
                double dNewTotal = dTotal + (dealerHand[2].GetCardValue());
        Console.WriteLine($"{dNewTotal} is my new total");
            return dNewTotal;
        }
        static void DCardSumLogic2(double dNewTotal, List<Card> dealerHand, List<Card> randomDeck)
        {
            if (dNewTotal == 21)
            {
                Console.WriteLine($"{dNewTotal}");
                Console.WriteLine("BLACKJACK!");
                Console.WriteLine("Would you like to play again?");
            }
            else if (dNewTotal >= 16)
            {
                Console.WriteLine($"{dNewTotal}");
                Console.WriteLine("I will stand");
            }
            else if (dNewTotal < 16)
            {
                Console.WriteLine($"{dNewTotal} is my total.");
                Console.WriteLine("I'll take another card");
                dHitLogic(dealerHand, randomDeck, dNewTotal);
            }
            else if (dNewTotal > 21)
            {
                Console.WriteLine($"{dNewTotal}");
                Console.WriteLine("That's a Bust for me, you win this round.");
            }
        }
    }

}