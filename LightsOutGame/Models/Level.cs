namespace LightsOutGame.Models
{
    public class Level
    {
        public string Name { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public int[] On { get; set; }
    }

}
