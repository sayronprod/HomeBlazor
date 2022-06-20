namespace HomeBlazor.Models
{
    /// <summary>
    /// Data for chart
    /// </summary>
    /// <typeparam name="T">Vertical</typeparam>
    /// <typeparam name="Y">Horizontal</typeparam>
    public class ChartData<T, Y>
    {
        public List<T> Vertical { get; set; }
        public List<Y> Horizontal { get; set; }
    }
}
