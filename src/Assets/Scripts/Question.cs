[System.Serializable]

public class Question
{
    public string question;
    public string[] answers;
    public int? correctAnswer = 1;

    public void SetCorrectAnswer(int correct)
    {
        correctAnswer = correct;
    }
}
