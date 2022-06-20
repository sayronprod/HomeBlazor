namespace HomeBlazor.Models
{
    public class Weather
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }

        public Weather Clone()
        {
            return (Weather)this.MemberwiseClone();
        }
    }
}
