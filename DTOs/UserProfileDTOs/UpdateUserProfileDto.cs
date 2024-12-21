namespace E_Commerce_API.DTOs.UserProfileDTOs
{
    public class UpdateUserProfileDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
