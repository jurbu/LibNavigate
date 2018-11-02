namespace LibNavigate.Function
{
    public sealed class Function
    {
        public delegate Result UnaryFunction<Input, Result>(Input input);

        public delegate void UnaryVoidFunction<Input>(Input input);

        public delegate bool UnaryPredicate<Input>(Input input);

        public delegate Result GenerateFunction<Result>();
    }
}
