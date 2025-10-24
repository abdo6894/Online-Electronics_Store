using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.CategoryDtos
{
    public class CategoryCreateDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
