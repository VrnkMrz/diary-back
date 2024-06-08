using System.Text.Json.Serialization;

namespace diary_back.DTO
{
    public class DiaryEntryDto
    {
        public string Text { get; set; } = null!;
        public int? UserEmotionId { get; set; }
        public int? AiEmotionId { get; set; }

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? Date { get; set; }
    }

}
