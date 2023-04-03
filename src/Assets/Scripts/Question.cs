[System.Serializable]

public class Question
{
    public string question;
    public string[] answers;
    public int? correctAnswer = null;

    public void SetCorrectAnswer(int correct)
    {
        correctAnswer = correct;
    }
}
