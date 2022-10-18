using System.Collections.Generic;
using Shared.RequestFeatures;

namespace Shared.DataTransferObjects.ForShow
{
    public class GameDtoWithPagination
    {
        public IEnumerable<GameDto>? Games { get; set; }

        public MetaData? Data { get; set; }

       
    }
}