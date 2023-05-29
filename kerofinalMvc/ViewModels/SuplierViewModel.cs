using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class SuplierViewModel
    {
       public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [DataType(DataType.Password)]
        [Compare("PassWord")]
        public string ConfirmPassWord { get; set; }
        //b4c06fb9-bc0c-4542-a44a-b3f05f5f8900
        //
    }
}
