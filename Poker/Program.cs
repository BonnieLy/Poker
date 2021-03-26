using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        private const int NUMBER_OF_PLAYER = 4;
        private const int NUMBER_OF_CARD_PER_PLAYER = 5;

        static void Main(string[] args)
        {
            Task game1 = Task.Factory.StartNew(() => PlayGameThread());
            Task game2 = Task.Factory.StartNew(() => PlayGameThread());
            Task game3 = Task.Factory.StartNew(() => PlayGameThread());

            Task.WaitAll(game1, game2, game3);
            Console.WriteLine("All games completed.");

            // PlayGame();
        }

        static void PlayGameThread()
        {
            // Initialise the deck with shuffled cards
            Deck deck = new Deck();

            // Create a list of players with cards
            List<Player> players = InitialisePlayer(deck);

            // Display the winner
            DisplayWinner(players, deck);
        }

        static void PlayGame()
        {
            // Initialise the deck with shuffled cards
            Deck deck = new Deck();

            // Create a list of players with cards
            List<Player> players = InitialisePlayer(deck);

            // Display their ranks
            DisplayRank(players);

            // Display the winner
            DisplayWinner(players, deck);
        }

        // Initialise the player list based on the number of players
        static List<Player> InitialisePlayer(Deck deck)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < NUMBER_OF_PLAYER; i++)
            {
                Player player = new Player($"{i + 1}");
                players.Add(player);
            }

            // Deal card to the players
            players = DealCardToPlayers(players, deck);
            return players;
        }

        // Deal the card to each player based on the number of cards for each player
        static List<Player> DealCardToPlayers(List<Player> players, Deck deck)
        {
            var playerList = players;
            for (int time = 0; time < NUMBER_OF_CARD_PER_PLAYER; time++)
            {
                foreach (Player p in playerList)
                {
                    p.Cards.Add(deck.Draw());
                }
            }
            return playerList;
        }

        // Show the cards and the rank of each player
        static void DisplayRank(List<Player> players)
        {
            foreach (Player p in players)
            {
                foreach (Card c in p.Cards)
                {
                    Console.WriteLine(c.ToString());
                }
                Console.WriteLine($"\nPlayer {p.Name} rank is: {p.GetRankName()}\n\n");
            }
        }

        // Check and display the winner
        static void DisplayWinner(List<Player> players, Deck deck)
        {
            Player winner = deck.CheckWinner(players);
            Console.WriteLine($"\nWinner is Player {winner.Name}\nCard: {winner.ListOfCards}\nRank:{winner.GetRankName()}\n");
        }
    }
}
