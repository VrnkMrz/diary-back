using System;
using System.Collections.Generic;

namespace diary_back.Models;

public partial class Emotion
{
    public int Id { get; set; }

    public string EmotionName { get; set; } = null!;

    //public virtual ICollection<Diaryentry> DiaryentryAiEmotions { get; set; } = new List<Diaryentry>();

    //public virtual ICollection<Diaryentry> DiaryentryUserEmotions { get; set; } = new List<Diaryentry>();
}
