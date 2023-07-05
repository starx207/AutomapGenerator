namespace AutomapGenerator.FunctionalTests.Models;
public abstract class AuditEntity {
    public DateTime CreateDate { get; set; }
    public DateTime ChangeDate { get; set; }
    public User? CreateUser { get; set; }
    public User? ChangeUser { get; set; }
}
