namespace Broker.Models
{
    public class FanStatus
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsRunning { get; set; } = false;

        public double Speed { get; set; }
    }
}
