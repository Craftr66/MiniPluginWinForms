namespace MiniPluginWinForms
{
    public class RuntimeContext
    {
        private static readonly RuntimeContext _instance
            = new RuntimeContext();

        public static RuntimeContext Instance
            => _instance;

        private RuntimeContext()
        {
        }

        /// <summary>
        /// 当前读取到的电压
        /// </summary>
        public double Voltage { get; set; }
    }
}