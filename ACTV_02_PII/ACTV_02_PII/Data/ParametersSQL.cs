namespace ACTV_02_PII.Data
{
    public class ParametersSQL
    {
        public ParametersSQL(string name, object value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}