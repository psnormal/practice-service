﻿using System.ComponentModel.DataAnnotations;

namespace practice_service.Models
{
    public class PracticeProfile
    {
        [Required]
        [Key]
        public Guid PracticeProfileId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Characteristic { get; set; }
        [Required]
        public string PracticeDiary { get; set; }
        [Required]
        public Guid PracticePeriodId { get; set; }
    }
}
