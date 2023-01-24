namespace Domain
{
    public static class SessionMutex
    {
        private static Dictionary<ulong?, Mutex> _mutex = new Dictionary<ulong?, Mutex>();
        public static bool containsKey(ulong? key)
        {
            return _mutex.ContainsKey(key);
        }
        public static void addKey(ulong? key)
        {
            _mutex.Add(key, new Mutex());
        }
        public static void wait(ulong? key)
        {
            _mutex.First(d => d.Key == key).Value.WaitOne();
        }
        public static void releaseMutex(ulong? key)
        {
            _mutex.First(d => d.Key == key).Value.ReleaseMutex();
        }
    }
}
