namespace AutomapGenerator.FunctionalTests.Models;
public class RelatedParcelViewModel {
    public string RelatedParcelNumber { get; set; }
    public string? District { get; set; }

    public RelatedParcelViewModel(string num) => RelatedParcelNumber = num;
}
