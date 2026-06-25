import { Component, OnInit, inject } from '@angular/core';
import { Quiz, QuizService, QuizSummary } from './services/quize.service';
import { QuizQuestionsComponent } from './quiz-questions/quiz-questions.component';

@Component({
  selector: 'app-root',
  imports: [QuizQuestionsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private readonly quizService = inject(QuizService);

  quizzes: QuizSummary[] = [];
  selectedQuiz?: Quiz;
  isLoading = false;
  isQuizLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadQuizzes();
  }

  loadQuizzes(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.quizService.getQuizzes().subscribe({
      next: quizzes => {
        this.quizzes = quizzes;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Could not load quizzes.';
        this.isLoading = false;
      }
    });
  }

  selectQuiz(id: string): void {
    this.isQuizLoading = true;
    this.errorMessage = '';

    this.quizService.getQuizById(id).subscribe({
      next: quiz => {
        this.selectedQuiz = quiz;
        this.isQuizLoading = false;
      },
      error: () => {
        this.errorMessage = 'Could not load quiz details.';
        this.isQuizLoading = false;
      }
    });
  }
}
