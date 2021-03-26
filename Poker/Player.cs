using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    class Player
    {
        public Player()
        {
        }

        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();

        // Return the rank
        public int Rank
        {
            get
            {
                return CalculateRank();
            }
        }

        // Return the rank name based on the rank number
        public string GetRankName()
        {
            string result;
            string calResult = Convert.ToString(Rank);

            // Get the rank number from the rank
            int rank = int.Parse(calResult.Substring(0, calResult.Length - 2));

            switch(rank)
            {
                case 10:
                    result = "High Card";
                    break;
                case 9:
                    result = "One Pair";
                    break;
                case 8:
                    result = "Two Pair";
                    break;
                case 7:
                    result = "Three Of A Kind";
                    break;
                case 6:
                    result = "Straight";
                    break;
                case 5:
                    result = "Flush";
                    break;
                case 4:
                    result = "Full House";
                    break;
                case 3:
                    result = "Four Of A Kind";
                    break;
                case 2:
                    result = "Straight Flush";
                    break;
                case 1:
                    result = "Royal Flush";
                    break;
                default:
                    result = "An error has occurred.";
                    break;
            }
            return result;
        }

        /**
            Calculate the rank based on the suits and values
            Return int: the number that consists of the rank and the total value of the cards
         */
        private int CalculateRank()
        {
            int rank = 0, counter = 0;
            var cards = Cards;
            int[] suits = cards.Select(elem => (int)elem.Suit).ToArray();
            int[] values = cards.Select(elem => (int)elem.Value).ToArray();

            // Use set to get unique collection of suits and values
            HashSet<int> suitSet = new HashSet<int>(suits);
            HashSet<int> valueSet = new HashSet<int>(values);

            // Determine the rank based on the number of suits and values
            switch (valueSet.Count())
            {
                // If there are 5 different values
                case 5:
                    // If they are in the same suit
                    if (suitSet.Count() == 1)
                    {
                        // If they are in sequence
                        if (isInSequence(values))
                        {
                            //If they contain 10JQKA (total value is 60), set rank as 1 = Royal Flush
                            if (values.Sum() == 60) rank = GenerateRankValue(1, values);

                            // Else set rank as 2 = Straight Flush
                            else rank = GenerateRankValue(2, values);
                        }

                        // If they are not in sequence, set rank as 5 = Flush
                        else rank = GenerateRankValue(5, values);
                    }

                    // If they are not in the same suit
                    else
                    {
                        // If they are in sequence, set rank as 6 = Straight
                        if (isInSequence(values)) rank = GenerateRankValue(6, values);

                        // If they are not in sequence, set rank as 10 = High Card
                        else rank = GenerateRankValue(10, values);
                    }
                    break;

                // If there are 4 different values
                case 4:
                    // Set rank as 9 = One Pair
                    rank = GenerateRankValue(9, values);
                    break;

                // If there are 3 different values, it can be Two Pair or Three Of A Kind
                case 3:
                    // Count each value
                    foreach (int card in valueSet)
                    {
                        counter = values.Count(elem => elem == card);
                        if (counter == 3) break;
                    }

                    // If a value is counted 3 times, set rank as 7 = Three Of A Kind, else set rank as 8 = Two Pair
                    rank = counter == 3 ? GenerateRankValue(7, values) : GenerateRankValue(8, values);
                    break;

                // If there are 2 different values
                case 2:
                    // Count the first value of the set
                    counter = values.Count(card => card == valueSet.First());

                    // If it has 4 or 1, set rank as 3 = Four Of A Kind, else set rank as 4 = Full House
                    rank = counter == 4 || counter == 1 ? GenerateRankValue(3, values) : GenerateRankValue(4, values);
                    break;
                default:
                    rank = 0;
                    break;
            }
            return rank;
        }

        // This return an int that include the rank and the total value of the cards for comparison
        private int GenerateRankValue(int rank, int[] values)
        {
            string str = $"{rank}{values.Sum()}";
            return int.Parse(str);
        }

        // Check if the array is in sequence
        private static bool isInSequence(int[] values)
        {
            bool isInSequence = true;
            int[] num = values;
            Array.Sort(num);

            for (int i = 0; i < num.Length - 1; i++)
            {
                if (num[i] != num[i + 1] - 1)
                {
                    isInSequence = false;
                    break;
                }
            }

            return isInSequence;
        }

        // Return a string that contain the name of the cards
        public string ListOfCards
        {
            get
            {
                string str = "";
                foreach (Card c in Cards)
                {
                    str += $"{c}, ";
                }
                return str.Substring(0, str.Length - 2);
            }
            
        }
    }
}
