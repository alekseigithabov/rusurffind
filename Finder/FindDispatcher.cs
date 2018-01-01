namespace TabTextFinder.Finder
{
    sealed class FindDispacher
    {
        public delegate void InvokeFindDelegate( string text, FindType type, FindQuery base_query );
        public delegate bool IsWorkingDelegate();

        public InvokeFindDelegate InvokeFind { get; private set; }
        public IsWorkingDelegate IsWorking { get; private set; }

        public FindDispacher( InvokeFindDelegate invoke_find, IsWorkingDelegate is_working )
        {
            InvokeFind = invoke_find;
            IsWorking = is_working;
        }
    }
}