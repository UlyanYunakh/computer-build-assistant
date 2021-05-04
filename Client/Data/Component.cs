using System.ComponentModel.DataAnnotations;

namespace Client.Data
{
    public class Component
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Worth { get; set; }
        public string Description { get; set; }
    }

    public class CPU : Component
    {
        
    }
    public class RAM : Component
    {

    }
    public class StorageDevice : Component
    {

    }
    public class PowerSupply : Component
    {

    }
    public class Motherboard : Component
    {

    }
    public class Shell : Component
    {

    }
    public class CoolingSystem : Component
    {

    }
}