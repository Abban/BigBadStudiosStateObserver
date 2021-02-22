using System;

namespace GF.Library.StateBroker
{
    public class TransactionException : Exception
    {
        public override string Message => "State transaction attempted while transaction already in progress";
    }
}