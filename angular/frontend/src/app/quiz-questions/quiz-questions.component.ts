import { Component, Input } from '@angular/core';
import { Quiz, QuizAnswer } from '../services/quize.service';

@Component({
  selector: 'app-quiz-questions',
  imports: [],
  templateUrl: './quiz-questions.component.html',
  styleUrl: './quiz-questions.component.css',
})
export class QuizQuestionsComponent {
  @Input({ required: true }) quiz!: Quiz;

  currentQuestionIndex: number = 0;
  correctAnswers: number = 0;

  selectAnswer(answer: QuizAnswer): void {
    if (answer.isCorrect) {
      this.correctAnswers++;
    }

    this.currentQuestionIndex++;
  }
}
