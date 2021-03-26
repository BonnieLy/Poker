using System;
using System.Collections.Generic;

namespace Poker
{
    class Deck : Card
    {
        public Stack<Card> Cards { get; set; } = new Stack<Card>();

        public Deck()
        {
            Cards = InitialiseDeck();
        }

        public Card Draw()
        {
            return Cards.Pop();
        }

        // Initialise the deck with 13 values and 4 suits
        private Stack<Card> InitialiseDeck()
        {
            Stack<Card> Deck = new Stack<Card>();
            foreach (SUIT s in Enum.GetValues(typeof(SUIT)))
            {
                foreach (VALUE v in Enum.GetValues(typeof(VALUE)))
                {
                    Card card = new Card { Suit = s, Value = v};
                    Deck.Push(card);
                }
            }

            // Shuffle the deck
            Deck = ShuffleDeck(Deck);

            return Deck;
        }

        // Shuffle the cards in the deck with Random()
        private Stack<Card> ShuffleDeck(Stack<Card> deck)
        {
            var deckArr = deck.ToArray();
            Random random = new Random();

            for (int i = 0; i < deck.Count - 1; i++)
            {
                int next = random.Next(NUMBER_OF_CARD);

                // Swap the current card with a random card
                Card tempCard = deckArr[i];
                deckArr[i] = deckArr[next];
                deckArr[next] = tempCard;
            }

            // Convert the array to the Stack
            deck = new Stack<Card>(deckArr);
            return deck;
        }

        // Check the winner based on the rank and total value
        public Player CheckWinner(List<Player> players)
        {
            Player player = null;
            int rank = 11, total = 0, p_rank, p_total;
            string result;

            foreach (Player p in players)
            {
                // Get player rank and total value
                result = Convert.ToString(p.Rank);
                p_rank = int.Parse(result.Substring(0, result.Length - 2));
                p_total = int.Parse(result.Substring(result.Length - 2, 2));

                // If the player has a better rank (lowest is 10, highest is 1)
                if (p_rank < rank)
                {
                    // Set the player as the highest
                    rank = p_rank;
                    total = p_total;
                    player = p;
                }

                // If the player has the same rank with the highest
                else if (p_rank == rank)
                {
                    // If the player total is higher then the highest
                    if (p_total > total)
                    {
                        // Set the player as the highest
                        total = p_total;
                        player = p;
                    }
                }
            }
            return player;
        }
    }
}
