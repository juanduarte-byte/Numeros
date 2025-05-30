namespace ParImparAPI.Domain.Entities
{
    public class Numero
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Paridad { get; set; }

        public Numero()
        {
            Paridad = string.Empty;
        }
    }
}
