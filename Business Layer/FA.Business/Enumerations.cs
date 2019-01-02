using System.ComponentModel;

namespace FA.Business
{
    public class Enumerations
    {
        public enum Status
        {
            [Description("Not Successful")]
            Unsuccessful = 0,

            [Description("Successful")]
            Successful = 1,

            [Description("Not Authorized")]
            Unauthorized = 2
        }
    }
}
