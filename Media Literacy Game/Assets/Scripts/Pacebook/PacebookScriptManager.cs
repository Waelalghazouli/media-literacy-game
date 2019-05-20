﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Pacebook
{
    public class PacebookScriptManager : MonoBehaviour
    {
        // Question Canvas
        public Button rightButton;
        public Button fakeButton;
        public Image newsImage;
        public Text questionText;

        // Feedback Canvas
        public Text feedbackText;
        public Button nextBtn;

        // Welcome Canvase
        public Button startButtom;

        // Canvases
        public Canvas questionCanvas;
        public Canvas feedbackCanvas;
        public Canvas welcomeCanvas;

        HardCodedQuestions questions;

        QuizManager quizManager;

        QuestionModel[] questionList;

        // Use this for initialization
        void Start()
        {
            getQuestions();

            // At the start of the scene
            questionCanvas.gameObject.SetActive(false);
            feedbackCanvas.gameObject.SetActive(false);
            welcomeCanvas.gameObject.SetActive(true);


            quizManager = new QuizManager(questionList);
            loadNextQuestion();
        }

        public void startButtonClick()
        {
            loadNextQuestion();

            welcomeCanvas.gameObject.SetActive(false);
            feedbackCanvas.gameObject.SetActive(false);
            questionCanvas.gameObject.SetActive(true);
        }

        public void rightButtonClick()
        {
            bool answeredCorrectly = false;


            answeredCorrectly = quizManager.answerQuestion(true);
            questionCanvas.gameObject.SetActive(false);

            if (answeredCorrectly)
            {
                feedbackText.text = "Your answer is good, " + quizManager.currentFeedback;
            }
            else
            {
                feedbackText.text = "Your answer is not good, " + quizManager.currentFeedback;
            }
            feedbackCanvas.gameObject.SetActive(true);
        }

        public void fakeButtonClick()
        {
            bool answeredCorrectly = false;

            answeredCorrectly = quizManager.answerQuestion(false);
            questionCanvas.gameObject.SetActive(false);

            if (answeredCorrectly)
            {
                feedbackText.text = "Your answer is good, " + quizManager.currentFeedback;
            }
            else
            {
                feedbackText.text = "Your answer is not good, " + quizManager.currentFeedback;
            }
            feedbackCanvas.gameObject.SetActive(true);
        }

        public void nextButtonClick()
        {
            var nextQuestion = quizManager.nextQuestion();

            loadNextQuestion();
            feedbackCanvas.gameObject.SetActive(false);
            questionCanvas.gameObject.SetActive(true);
        }

        private QuestionModel[] getQuestions()
        {
            questions = new HardCodedQuestions();
            questions.addQuestions();
            questionList = questions.GetQuestions();
            return questionList;
        }

        // to handle the buttons
        private void onButtonClick(Buttons buttonId)
        {
            if (quizManager.currentQuestion < quizManager.amountOfQuestions)
            {
                bool answeredCorrectly = false;

                switch (buttonId)
                {
                    case Buttons.StartButton:
                        welcomeCanvas.gameObject.SetActive(false);
                        feedbackCanvas.gameObject.SetActive(false);
                        questionCanvas.gameObject.SetActive(true);
                        break;

                    case Buttons.rightButton:

                        answeredCorrectly = quizManager.answerQuestion(true);
                        questionCanvas.gameObject.SetActive(false);

                        if (answeredCorrectly)
                        {
                            feedbackText.text = "Your answer is good, " + quizManager.currentFeedback;
                        }
                        else
                        {
                            feedbackText.text = "Your answer is not good, " + quizManager.currentFeedback;
                        }
                        feedbackCanvas.gameObject.SetActive(true);
                        break;

                    case Buttons.fakeButton:
                        answeredCorrectly = quizManager.answerQuestion(true);
                        questionCanvas.gameObject.SetActive(false);

                        if (answeredCorrectly)
                        {
                            feedbackText.text = "Your answer is good, " + quizManager.currentFeedback;
                        }
                        else
                        {
                            feedbackText.text = "Your answer is not good, " + quizManager.currentFeedback;
                        }
                        feedbackCanvas.gameObject.SetActive(true);
                        break;
                }
                if (quizManager.currentQuestion < quizManager.amountOfQuestions)
                {
                    loadNextQuestion();
                }
            }



        }

        // For loading the quetion

        public void loadNextQuestion()
        {
            var nextQuestion = quizManager.nextQuestion();
            int numQuestion = nextQuestion.questionId;
            if (nextQuestion != null)
            {
                questionText.GetComponent<Text>().text = nextQuestion.question;
                newsImage.sprite = Resources.Load<Sprite>("PaceBookImages/" + nextQuestion.image); //+ nextQuestion.image
            }
            else
            {
                // Do NOTHING BRO
            }
        }

        enum Buttons
        {
            StartButton = 1,
            ExitButton = 2,
            fakeButton = 3,
            rightButton = 4,
            feedbackButton = 5,
            nextButton = 6
        }
    }
}
