using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBlazor.Data.Models
{
    [Table("Weather")]
    public class WeatherDbo
    {
        [Key]
        public long Id { get; set; }

        public float Temperature { get; set; }

        public float Pressure { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; set; }
    }
}
