namespace ConnectionLibrary.Objects.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class LastModifiedAttribute(string date) : Attribute
    {
        public string Date { get; } = date;
    }
}
