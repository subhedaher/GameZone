namespace GameZone.Models
{
    public class Device
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;
        public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();
    }
}
