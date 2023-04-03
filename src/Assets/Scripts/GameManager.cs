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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
			SetOptionTexts();
        }
    }

    void SetOptionListener()
    {
		for (int i = 0; i < optionButtons.Length; i++)
		{
            optionButtons[i].onClick.AddListener(() =>
            {
                ChangeQuestion(); // TODO: change
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

	/*
	public Question[] questions;
	private Question currentQuestion;
	private int? correctAnswer = null; 
	private int maxScore = 34;
	private int currentScore = 0;
	private int offset = 3;
	private int randomStart = 5;
	private int questionNumber;
	private int answeringQuestion = 0;
	
	[SerializeField] private TextMeshProUGUI questionTxt;
	
	//[SerializeField] private TextMeshProUGUI scoreTxt;

	[SerializeField] private Button[] optionButtons;
	[SerializeField] private float timeBetweenQuestions = 1f;



	void Start()
	{
        questionNumber = questions.Length;
        SetCurrentQuestion(answeringQuestion);
        SetOptionButtons();
		//UpdateScore();
	}

	void SetOptionButtons() {
		for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[index].onClick.AddListener(() =>
            {
                CheckAnswer(index);
            });
        }
	}
		
	void Update()
	{
		if (correctAnswer == null)
		{
			for (int i = 0; i < optionButtons.Length; i++)
			{
				if (optionButtons[i].IsClicked)
				{
					CheckAnswer(i);
				}
			}

		}
		ChangeQuestion();
	}



    void ChangeQuestion()
	{
		if(currentScore < maxScore)
		{
			bool b1 = ( (currentScore % offset) == 2);
			bool b2 = ( currentScore >= randomStart );
			//Debug.Log(currentScore + "|" + answeringQuestion + "|" + b1 + "|" + b2 + "|" + ( b1 && b2 ));

			currentScore++;

			if ( ( b1  && b2 ) )
            {
				int index = Random.Range(0, answeringQuestion + 1);
				//Debug.Log(index);
				SetCurrentQuestion(index);
            }
            else
			{
				answeringQuestion++;
				SetCurrentQuestion(answeringQuestion);
			}
		}
	}
    void SetCurrentQuestion(int index)
    {
        currentQuestion = questions[index];
        questionTxt.text = currentQuestion.question;
        correctAnswer = null;
		
		int[] wrongAnswers = new int[9];
        int k = 0;
        for (int i = 0; i < 10; i++)
        {
            if (i != currentQuestion.CorrectAnswer)
            {
                wrongAnswers[k] = i;
                k++;
            }
        }



        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i == currentQuestion.CorrectAnswer)
            {
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.Answer[i];
            }
            else
            {
                int randomIndex = Random.Range(0, 9);
                while (wrongAnswers[randomIndex] == -1)
                {
                    randomIndex = Random.Range(0, 9);
                }
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.Answer[wrongAnswers[randomIndex]];
                wrongAnswers[randomIndex] = -1;
            }
        }


		SetRightAnswer();
    }


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

	void SetRightAnswer()
	{
        if (currentQuestion.CorrectAnswer == null)
        {
            // If the correct answer hasn't been set yet
            for (int i = 0; i < currentQuestion.Answer.Length; i++)
            {
                // Iterate over all the option buttons to check if any of them have been clicked
                if (optionButtons[i].IsClicked)
                {
                    currentQuestion.CorrectAnswer = i;
                    Debug.Log("Correct answer set to " + i);
                    break;
                }
            }
			else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				for (int i = 0; i < currentQuestion.Answer.Length; i++)
				{
					if (RectTransformUtility.RectangleContainsScreenPoint(optionButtons[i].GetComponent<RectTransform>(), Input.GetTouch(0).position, Camera.main))
					{
						currentQuestion.CorrectAnswer = i;
						Debug.Log("Correct answer set to " + i);
						break;
					}
				}
			}
		}
	}



	//	void getRandomQuestion() {

	//	}

	IEnumerator TransitionToNextQuestion()
	{
		yield return new WaitForSeconds(timeBetweenQuestions);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}

	public void clickButton(){
		//Debug.Log(this.g);
	}
	*/
}
