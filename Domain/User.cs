namespace Domain
{
    internal class User
    {
        public ulong ID { get; set; }
        public ulong PhoneNumber { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
    }
}
