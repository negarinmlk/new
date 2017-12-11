using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
   public class AddUsser
    {
        [Key]
        [Display(Name = "نام کاربری")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }


        [Display(Name = "کلمه عبور")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
