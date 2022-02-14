namespace Dentis.Models
{
    public class Queue
    {
        public int QueueId { get; set; }
        public DateTime InitDate { get; set; }
        public DateTime EndDate { get; set; }
        public int QueueStatusId { get; set; }
        public int ClinicConsultingId { get; set; }
        public string? ClinicConsultingName { get; set; }
        public string? QueueStatusName { get; set; }
        public int PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? PatientGender { get; set; }
        public int PatientAge { get; set; }
        public int AppointmentReasonId { get; set; }
        public string? AppointmentReasonName { get; set; }
    }

    public class QueueStatusViewModel
    {
        public int QueueEstatusId { get; set; }
        public string? QueueEstatusName { get; set; }
    }
}
