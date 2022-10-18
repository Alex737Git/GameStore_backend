using Entities.Exceptions.Abstract;

namespace Entities.Exceptions.BadRequestExceptions
{
    public class MaxDateRangeBadRequestException :BadRequestException
    {
        public MaxDateRangeBadRequestException(string message) : base(message)
        {
        }
        
         public MaxDateRangeBadRequestException() : base("" +
                                                         "Max Game date can't be less than Min game date")
        {
        }
        
        
        
    }
}