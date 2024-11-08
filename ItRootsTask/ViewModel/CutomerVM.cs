using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
  public class CustomerVM
{
    public int Id { get; set; }
    [Display(Name = "Full Name")]
    public string FullName { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Email { get; set; }
    public string Password { get; set; }
    [Compare("Password")]
    [Display(Name = "Confirmed Password")]

    public string ConfirmedPassword { get; set; }
    [Display(Name = "Phone Number")]

    public string PhoneNumber { get; set; }
}
}
