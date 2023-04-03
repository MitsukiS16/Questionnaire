using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	public Question[] questions;
	private int questionNumber;
	private Question currentQuestion;
	private int? correctAnswer = null;
	private int maxScore;
	private int offset = 3;
	private int randomStart = 5;
	private int currentScore = 0;
	private int answeredQuestion = 0; // last question in questions array we have displayed

	[SerializeField] private TextMeshProUGUI questionTxt;
	[SerializeField] private TextMeshProUGUI scoreTxt;

	[SerializeField] private Button[] optionButtons;
	[SerializeField] private float timeBetweenQuestions = 1f;



	private void Start()
	{
		questionNumber = questions.Length - 1;
		//maxScore = (questionNumber - (questionNumber / 2 + 1)) * 3; //36 // TODO: fix this formula
		maxScore = 36;
		SetCurrentQuestion(currentScore);
		SetOptionListener();

	}

    void SetOptionListener()
    {
		for (int i = 0; i < optionButtons.Length; i++)
		{
            optionButtons[i].onClick.AddListener(() =>
            {
				SelectAnswer(0);
            });
        }
	}

	void ChangeQuestion()
	{
		if (currentScore < (maxScore - 1))
		{
			bool b1 = ((currentScore % offset) == 2);
			bool b2 = (currentScore >= randomStart);

			currentScore++;

			if ((b1 && b2))
			{
				int index = Random.Range(0, answeredQuestion);
				SetCurrentQuestion(index);
			}
			else
			{
				answeredQuestion++;
				SetCurrentQuestion(answeredQuestion);
			}
		}
	}

	void SetCurrentQuestion(int index)
	{
		// set current question properties
		currentQuestion = questions[index];
		questionTxt.text = currentQuestion.question;
		correctAnswer = currentQuestion.correctAnswer;
		// select options and update buttons
		SetOptionTexts();
	}

	void SetOptionTexts()
	{
		List<int> wrongAnswers = new List<int>();
		for (int i = 0; i < currentQuestion.answers.Length; i++)
		{
			if (i != currentQuestion.correctAnswer)
			{
				wrongAnswers.Add(i);
			}
		}

		int correctBtn = Random.Range(0, 4);
		for (int i = 0; i < optionButtons.Length; i++)
		{
			TextMeshProUGUI btnText = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
			if (i == correctBtn)
			{
				if (currentQuestion.correctAnswer != null)
				{
					btnText.text = currentQuestion.answers[(int)currentQuestion.correctAnswer];
				}
                else
				{
					int chosenAnswer = Random.Range(0, wrongAnswers.Count);
					btnText.text = currentQuestion.answers[wrongAnswers[chosenAnswer]];
					wrongAnswers.RemoveAt(chosenAnswer);
				}
			}
			else
			{
				int chosenAnswer = Random.Range(0, wrongAnswers.Count);
				btnText.text = currentQuestion.answers[wrongAnswers[chosenAnswer]];
				wrongAnswers.RemoveAt(chosenAnswer);
			}
		}
	}

	void SelectAnswer(int button)
    {
		Button btn = optionButtons[button];

		string btntext = btn.GetComponentInChildren<TextMeshProUGUI>().text;
		
		if(currentQuestion.correctAnswer == null)
        {
			for(int i=0; i<currentQuestion.answers.Length; i++)
            {
				if(currentQuestion.answers[i] == btntext)
                {
					currentQuestion.correctAnswer = i;
					break;
                }
            }
        }
		else
        {
			if (btntext != currentQuestion.answers[(int)currentQuestion.correctAnswer])
			{
				Debug.Log("you lose");
			}
		}
		ChangeQuestion();

	}

	/*
	void CheckAnswer(int index)
	{
		if (currentQuestion.CorrectAnswer == null)
		{
			currentQuestion.CorrectAnswer = index;
		}

		if (currentQuestion.CorrectAnswer == index)
		{
			Debug.Log("Correct Answer!");
			currentScore++;
			//UpdateScore();
		}
		else
		{
			Debug.Log("Wrong Answer!");
		}
	}


    void UpdateScore()
    {
        scoreTxt.text = "Score: " + currentScore;
    }


	*/
}
