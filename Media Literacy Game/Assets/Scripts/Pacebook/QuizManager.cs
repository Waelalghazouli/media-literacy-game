﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pacebook
{
    class QuizManager
    {
        List<QuestionModel> questions;
        public int amountOfQuestions { get; set; }
        public int currentQuestion { get; set; }
        public int currentAnswer { get; set; }
        public int score { get; set; }
        bool correctAnswers { get; set; }
        public string currentFeedback { get; set; }
        HardCodedQuestions testQuestions;


        public QuizManager(List<QuestionModel> questions)
        {
            this.questions = questions;
            testQuestions = new HardCodedQuestions();
            startQuiz();
        }

        private void startQuiz()
        {
            amountOfQuestions = questions.Count();
            currentQuestion = 0;
            currentAnswer = 0;

        }

        public bool answerQuestion(bool answer)
        {
            // pick the current question up
            var CurrentQuestion = questions[currentQuestion];
            currentFeedback = CurrentQuestion.feedback;
            correctAnswers = CurrentQuestion.rightAnswer;

            // check if the answer is correct and return it
            if (checkCurrentAnswer(answer, CurrentQuestion))
            {
                score = score + CurrentQuestion.score;
                currentQuestion++;
                currentAnswer++;
                return true;
            }
            currentQuestion++;
            return false;
        }

        private bool checkCurrentAnswer(bool answer, QuestionModel currentQuestion)
        {
            if (answer.Equals(currentQuestion.rightAnswer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public QuestionModel nextQuestion()
        {
            if (currentQuestion < questions.Count())
            {
                // get the next quetion
                var question = questions[currentQuestion];
                return question;
            }
            else
            {
                return null;
            }
        }

    }
}
