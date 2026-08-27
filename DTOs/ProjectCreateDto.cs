namespace API_REST.DTOs
{
    public class ProjectCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "En attente"; // Statut par défaut
        public int TeamId { get; set; } // L'ID de l'équipe qui va gérer le projet
    }
}
