namespace Poker
{
    class Card
    {
        public const int NUMBER_OF_CARD = 52;
        public enum SUIT
        {
            Spades,
            Clubs,
            Diamonds,
            Hearts
        }

        public enum VALUE
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }

        public SUIT Suit { get; set; }
        public VALUE Value { get; set; }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }
}
