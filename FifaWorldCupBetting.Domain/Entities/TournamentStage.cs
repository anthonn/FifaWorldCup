namespace FifaWorldCupBetting.Domain.Entities;

public class TournamentStage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // GroupStage, Round16, QuarterFinal, SemiFinal, Final
    public int Order { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
}
