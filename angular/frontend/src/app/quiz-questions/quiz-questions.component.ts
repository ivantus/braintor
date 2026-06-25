import { Component, Input } from '@angular/core';
import { Quiz } from '../services/quize.service';

@Component({
  selector: 'app-quiz-questions',
  imports: [],
  templateUrl: './quiz-questions.component.html',
  styleUrl: './quiz-questions.component.css'
})
export class QuizQuestionsComponent {
  @Input({ required: true }) quiz!: Quiz;
}
