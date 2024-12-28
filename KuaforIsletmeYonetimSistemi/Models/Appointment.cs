using System.ComponentModel.DataAnnotations;

public class Appointment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CustomerName { get; set; } // Müşteri adı

    [Required]
    public string ServiceType { get; set; } // Hizmet türü

    [Required]
    public string EmployeeName { get; set; } // Çalışan adı

    [Required]
    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; } // Randevu tarihi

    [Required]
    [DataType(DataType.Time)]
    public TimeSpan AppointmentTime { get; set; } // Randevu saati

}
