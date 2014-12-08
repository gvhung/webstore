using System.Web.Mvc;

namespace Entities
{
    public class BaseEntity
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}
