using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	public Question[] questions;
	private Question currentQuestion;
	
	private int maxScore = 30;
	private int currentScore = 0;
	private int offset = 3;
	private int randomStart = 5;
	private int questionNumber;
	private int answeringQuestion = 0;
	
	[SerializeField] private TextMeshProUGUI questionTxt;
	[SerializeField] private float timeBetweenQuestions = 1f;

	void Start()
	{
		questionNumber = questions.Length;
		SetCurrentQuestion(answeringQuestion);
		/*
		while(score < 10) 
		{
			if (unansweredQuestions == null || unansweredQuestions.Count == 0) {
				unansweredQuestions = questions.ToList<Question> ();
			}


			SetcurrentQuest();
			Debug.Log(currentQuest.fact);
			score = score + 1;
		}*/

		

	}

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
			ChangeQuestion();
        }
    }

    void ChangeQuestion()
	{
		if(currentScore < maxScore)
		{
			bool b1 = ( (currentScore % offset) == 2);
			bool b2 = ( currentScore >= randomStart );
			Debug.Log(currentScore + "|" + answeringQuestion + "|" + b1 + "|" + b2 + "|" + ( b1 && b2 ));

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
	}


	/*	void SetcurrentQuest()
	   {
		   if(unansweredQuestions.Count < 1) {
			   Debug.Log("Question < 1");
		   }
		   int randomQuestionsIndex = Random.Range(0,unansweredQuestions.Count);
		   currentQuest = unansweredQuestions[randomQuestionsIndex];

		   QuestionTxt.text = currentQuest.fact;


	   }
   */
	IEnumerator TransitionToNextQuestion()
	{
		yield return new WaitForSeconds(timeBetweenQuestions);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}

/*
	public void UserSelectTrue()
	{
		if(currentQuest.isTrue)
		{
			Debug.Log("Correct");
		} else
		{
			Debug.Log("Wrong ");
		}
		StartCoroutine(TransitionToNextQuestion());
	}

	public void UserSelectFalse()
	{
		if(!currentQuest.isTrue)
		{
			Debug.Log("Correct");
		} else
		{
			Debug.Log("Wrong ");
		}
		StartCoroutine(TransitionToNextQuestion());
	}*/

}
