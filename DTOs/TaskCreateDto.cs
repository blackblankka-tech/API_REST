namespace API_REST.DTOs
{
    public class TaskCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "À faire"; // En attente, En cours, Terminé
        public string Priority { get; set; } = "Moyenne"; // Basse, Moyenne, Haute
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; } // Le projet auquel appartient la tâche
        public int UserId { get; set; }
    }
}
