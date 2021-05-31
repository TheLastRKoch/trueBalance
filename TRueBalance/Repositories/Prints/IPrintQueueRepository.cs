using TRueBalance.Data.Entities;

namespace TRueBalance.Repositories.Prints
{
    public interface IPrintQueueRepository
    {
        void PrintWithCopy(Invoice CurrentInvoice);
    }
}
