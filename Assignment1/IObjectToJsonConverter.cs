namespace Assignment1
{
    public  interface IObjectToJsonConverter
    {
        public void Convert(object? obj);
        public string? GetJson();
        public void Print();
    }
}
