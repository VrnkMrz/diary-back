using System;
using System.Collections.Generic;

namespace diary_back.Models;

public partial class Diaryentry
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int? UserEmotionId { get; set; }

    public int? AiEmotionId { get; set; }

    public int User { get; set; }

    public DateTime? Date { get; set; }

    public virtual Emotion? AiEmotion { get; set; }

    public virtual Emotion? UserEmotion { get; set; }

    public virtual User UserNavigation { get; set; }
}
