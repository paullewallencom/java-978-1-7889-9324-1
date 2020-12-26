using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts
{

    public class AdditionalPersonData
    {
        public int AdditionalPersonDataId { get; set; }
        public string Data { get; set; }
    }
}
